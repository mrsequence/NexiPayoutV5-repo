using NexiPayout.Clients;
using NexiPayout.Framework.Interfaces;
using NexiPayout.Framework.Logging;
using NexiPayout.Helpers;
using NexiPayout.Models.Payouts;
using NexiPayout.Utilities;
using Serilog.Events;
using System;
using System.Collections.Generic;

namespace NexiPayout.ClientManagers
{
    public class PayoutManager
    {
        private RestSharpClient _payoutClient;
        private ClientHelper<PayoutResponse> payoutResponseClientHelper;
        private ClientHelper<PayoutDetailResponse> payoutDetailResponseClientHelper;
        //private readonly ILogger _logger;

        public PayoutManager()
        {
        }
        public RestSharpClient PayoutClient { get { return _payoutClient; } set { _payoutClient = value; } }
        public PayoutResponse GetAllPayouts(string endpoint)
        {
            //_logger.Write(LogEventLevel.Information, "Retrieving all payouts");
            try
            {
                string baseUrl = ConfigurationsUtility.GetSetting("baseUrl");
                _payoutClient = new RestSharpClient(baseUrl, endpoint);

                Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Accept", "application/Json" }
            };
                _payoutClient.CreateGetRequest(headers);
                _payoutClient.ExecuteRequest();
                payoutResponseClientHelper = new ClientHelper<PayoutResponse>();
                return payoutResponseClientHelper.DeserializeContent(_payoutClient.Response.Content);
            }
            catch (Exception ex)
            {
                //_logger.Write(LogEventLevel.Error, "Erro while retrueve endpoint: " + endpoint);
                //_logger.Debug(ex, "Encountered error when getting all Payments " + ex.Message);
                throw;
            }
          
        }

        public PayoutDetailResponse GetPayoutDetailById(string id)
        {
            string baseUrl = ConfigurationsUtility.GetSetting("baseUrl");
            string endpoint = "report/v1/payouts";
            endpoint = ApiUtility.ConstructEndPointParams( endpoint,new Dictionary<string, string>() { { "id", id } });
            _payoutClient = new RestSharpClient(baseUrl, endpoint);
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Accept", "application/Json" }
            };
            _payoutClient.CreateGetRequest(headers);
            _payoutClient.ExecuteRequest();
            payoutDetailResponseClientHelper = new ClientHelper<PayoutDetailResponse>();
            return payoutDetailResponseClientHelper.DeserializeContent(_payoutClient.Response.Content);
        }
    }
}
