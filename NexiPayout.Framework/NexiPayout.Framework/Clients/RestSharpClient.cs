using NexiPayout.CustomTypes.Enums;
using NexiPayout.Framework.Interfaces;
using RestSharp;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace NexiPayout.Clients
{
    public class RestSharpClient
    {
        private RestClient _client;
        private IRestResponse _response;
        private RestRequest _request;
        private string _baseUrl;
        private string _fullUrl;
        private readonly ILogger _logger;

        public IRestResponse Response { get { return _response; } set { _response = value; } }
        public RestRequest Request { get { return _request; } set { _request = value; } }
        public RestClient Client { get { return _client; } set { _client = value; } }
        public string BaseUrl { get { return _baseUrl; } set { _baseUrl = value; } }
        public string FullUrl { get { return _fullUrl; } set { _fullUrl = value; } }

        public RestSharpClient(string baseUrl, string endpoint, ILogger logger)
        {
            this._logger = logger;
            _baseUrl = baseUrl;
            string url = Path.Combine(baseUrl, endpoint);   
            _fullUrl = url;
            UriBuilder uriBuilder = new UriBuilder(url);
            _client = new RestClient();
            _request = new RestRequest(uriBuilder.Uri);
        }

        public HttpStatusCode GetStatusCode()
        {
            try
            {
                _logger.Write(LogEventLevel.Information, "[" + this.GetType().Name + "] Retrieving response statuscode");

                return _response.StatusCode;
            }
            catch (Exception ex)
            {

                _logger.Write(LogEventLevel.Error, "Error while retrieving status code");
                _logger.Debug(ex, "Encountered error when getting status code " + ex.Message);
                throw;
            }
           
        }


        public void ExecuteRequest()
        {
            try
            {
                _logger.Write(LogEventLevel.Information, "[" + this.GetType().Name + "] Executing request");
                _response = _client.Execute(_request);

            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, "Error while excuting request " + _request);
                _logger.Debug(ex, "Encountered error when executing request " + ex.Message);
                throw;
            }
        }


        public void CreateGetRequest(Dictionary<string, string> headers)
        {
            SetupRequestMethod(MethodType.GET);
            AddRequestHeaders(headers);
        }

        public void CreateDeleteRequest(Dictionary<string, string> headers)
        {
            SetupRequestMethod(MethodType.DELETE);
            AddRequestHeaders(headers);
        }

        public void CreatePostRequest(Dictionary<string, string> headers, string body)
        {
            SetupRequestMethod(MethodType.POST);
            AddRequestHeaders(headers);
            AddPostOrPutBody(body);
        }

        public void CreatePutRequest(Dictionary<string, string> headers, string body)
        {
            SetupRequestMethod(MethodType.PUT);
            AddRequestHeaders(headers);
            AddPostOrPutBody(body);
        }

        public void SetupRequestMethod(MethodType method)
        {
            switch (method)
            {
                case MethodType.GET:
                    _request.Method = Method.GET;
                    break;
                case MethodType.POST:
                    _request.Method = Method.POST;
                    break;
                case MethodType.PUT:
                    _request.Method = Method.PUT;
                    break;
                case MethodType.DELETE:
                    _request.Method = Method.DELETE;
                    break;
            }
        }

        public void AddRequestHeaders(Dictionary<string, string> headers)
        {
            foreach(string header in headers.Keys){
                _request.AddHeader(header, headers[header]);
            }
        }

        public void AddPostOrPutBody(string body)
        {
            _request.AddBody(body);
        }

    }
}
