using Newtonsoft.Json;
using NexiPayout.Clients;
using NexiPayout.Framework.Interfaces;
using NexiPayout.Framework.Models.Config;
using NexiPayout.Framework.Utilities;
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
        private readonly ILogger _logger;
        public readonly PayoutEnv _env;
        public PayoutManager(ILogger logger)
        {
            this._logger = logger;
            _env = JsonConvert.DeserializeObject<PayoutEnv>(FileUtility.GetFileContent(
              FileUtility.GetFullFilePath("payoutenv.json")));

        }
        public RestSharpClient PayoutClient { get { return _payoutClient; } set { _payoutClient = value; } }
        public PayoutResponse GetAllPayouts(string endpoint)
        {
            _logger.Write(LogEventLevel.Information, "Retrieving all payouts");
            try
            {
                _payoutClient = new RestSharpClient(_env.BaseUrl, endpoint, _logger);
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
                _logger.Write(LogEventLevel.Error, "Erro while retrieving Payouts with endpoint: " + endpoint);
                _logger.Debug(ex, "Encountered error when getting all Payments " + ex.Message);
                throw;
            }
          
        }

        public PayoutDetailResponse GetPayoutDetailById(string id)
        {
                string endpoint = "report/v1/payouts";
            try
            {
                _logger.Write(LogEventLevel.Information, "Retrieving payoutDetails By Id");

                endpoint = ApiUtility.ConstructEndPointParams(endpoint, new Dictionary<string, string>() { { "id", id } });
                _payoutClient = new RestSharpClient(_env.BaseUrl, endpoint, _logger);
                Dictionary<string, string> headers = new Dictionary<string, string>()
                {
                    { "Accept", "application/Json" }
                };
                _payoutClient.CreateGetRequest(headers);
                _payoutClient.ExecuteRequest();
                payoutDetailResponseClientHelper = new ClientHelper<PayoutDetailResponse>();
                return payoutDetailResponseClientHelper.DeserializeContent(_payoutClient.Response.Content);
            }
            catch (Exception ex)
            {

                _logger.Write(LogEventLevel.Error, "Erro while retrieving PayoutDetails with endpoint: " + endpoint + " and id: " + id);
                _logger.Debug(ex, "Encountered error when getting all Payments " + ex.Message);
                throw;
            }
        }
    }
}
