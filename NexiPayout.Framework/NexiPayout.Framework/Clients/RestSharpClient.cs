using NexiPayout.CustomTypes.Enums;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NexiPayout.Clients
{
    public class RestSharpClient
    {
        private RestClient _client;
        private IRestResponse _response;
        private RestRequest _request;
        private string _baseUrl;
        private string _fullUrl;

        public IRestResponse Response { get { return _response; } set { _response = value; } }
        public RestRequest Request { get { return _request; } set { _request = value; } }
        public RestClient Client { get { return _client; } set { _client = value; } }
        public string BaseUrl { get { return _baseUrl; } set { _baseUrl = value; } }
        public string FullUrl { get { return _fullUrl; } set { _fullUrl = value; } }

        public RestSharpClient(string baseUrl, string endpoint)
        {
            _baseUrl = baseUrl;
            string url = Path.Combine(baseUrl, endpoint);   
            _fullUrl = url;
            UriBuilder uriBuilder = new UriBuilder(url);
            _client = new RestClient();
            _request = new RestRequest(uriBuilder.Uri);
        }

        public HttpStatusCode GetStatusCode()
        {
            return _response.StatusCode;
        }


        public void ExecuteRequest()
        {
            try
            {
                _response = _client.Execute(_request);

            }
            catch (Exception)
            {

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
