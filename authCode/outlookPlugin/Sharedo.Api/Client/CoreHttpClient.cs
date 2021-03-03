using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sharedo.Api.Client
{
    public class CoreHttpClient : ICoreHttpClient
    {
        public bool ConfigureAwait { get; set; }

        public CoreHttpClient(bool configureAwait=false)
        {
            ConfigureAwait = configureAwait;
        }

        public Task<string> GetAsync(string url, Dictionary<string, string> headers = null, string bearerToken=null)
        {
            return SendAsync(HttpMethod.Get, url, null, headers, bearerToken);
        }

        public Task<TResponseModel> GetAsync<TResponseModel>(string url, Dictionary<string, string> headers = null, string bearerToken=null)
        {
            return SendAsync<TResponseModel>(HttpMethod.Get, url, headers, bearerToken);
        }

        public Task<string> PostAsync(string url, Dictionary<string, string> headers = null, string bearerToken = null)
        {
            return SendAsync(HttpMethod.Post, url, null, headers, bearerToken);
        }

        public Task<string> PostAsync<TRequestModel>(string url, TRequestModel model, Dictionary<string, string> headers = null, string bearerToken = null)
        {
            return SendAsync<TRequestModel>(HttpMethod.Post, url, model, headers, bearerToken);
        }

        public Task<TResponseModel> PostAsync<TResponseModel>(string url, Dictionary<string, string> headers = null, string bearerToken = null)
        {
            return SendAsync<TResponseModel>(HttpMethod.Post, url, headers, bearerToken);
        }

        public Task<TResponseModel> PostAsync<TRequestModel, TResponseModel>(string url, TRequestModel model, Dictionary<string, string> headers = null, string bearerToken = null)
        {
            return SendAsync<TRequestModel, TResponseModel>(HttpMethod.Post, url, model, headers, bearerToken);
        }

        public Task<string> PutAsync(string url, Dictionary<string, string> headers = null, string bearerToken = null)
        {
            return SendAsync(HttpMethod.Put, url, null, headers, bearerToken);
        }

        public Task<string> PutAsync<TRequestModel>(string url, TRequestModel model, Dictionary<string, string> headers = null, string bearerToken = null)
        {
            return SendAsync<TRequestModel>(HttpMethod.Put, url, model, headers, bearerToken);
        }

        public Task<TResponseModel> PutAsync<TResponseModel>(string url, Dictionary<string, string> headers = null, string bearerToken = null)
        {
            return SendAsync<TResponseModel>(HttpMethod.Put, url, headers, bearerToken);
        }

        public Task<TResponseModel> PutAsync<TRequestModel, TResponseModel>(string url, TRequestModel model, Dictionary<string, string> headers = null, string bearerToken = null)
        {
            return SendAsync<TRequestModel, TResponseModel>(HttpMethod.Put, url, model, headers, bearerToken);
        }

        public Task<string> SendAsync(HttpMethod method, string url, string body, Dictionary<string, string> headers = null, string bearerToken = null)
        {
            var content = body == null ? null : new StringContent(body, Encoding.UTF8, "application/json");
            return SendRawAsync(method, url, content, headers, bearerToken);
        }

        public Task<string> SendAsync<TRequestModel>(HttpMethod method, string url, TRequestModel model, Dictionary<string, string> headers = null, string bearerToken = null)
        {
            var body = Json.Serialise(model);
            return SendAsync(method, url, body, headers, bearerToken);
        }

        public async Task<TResponseModel> SendAsync<TRequestModel, TResponseModel>(HttpMethod method, string url, TRequestModel model, Dictionary<string, string> headers = null, string bearerToken = null)
        {
            var result = await SendAsync(method, url, model, headers, bearerToken);//.ConfigureAwait(ConfigureAwait);
            return Json.Deserialise<TResponseModel>(result);
        }

        public async Task<TResponseModel> SendAsync<TResponseModel>(HttpMethod method, string url, Dictionary<string, string> headers = null, string bearerToken = null)
        {
            var result = await SendAsync(method, url, null, headers, bearerToken);//.ConfigureAwait(ConfigureAwait);
            return Json.Deserialise<TResponseModel>(result);
        }

        public Task<string> SendFileAsync(HttpMethod method, string url, byte[] file, Dictionary<string, string> headers = null, string bearerToken = null)
        {
            var content = new ByteArrayContent(file);
            content.Headers.Add("Content-Type", "application/octet-stream");

            return SendRawAsync(method, url, content, headers, bearerToken);
        }

        public async Task<string> SendRawAsync(HttpMethod method, string url, HttpContent content, Dictionary<string, string> headers = null, string bearerToken = null)
        {
            try
            {
                var request = new HttpRequestMessage(method, url);
                request.Headers.Add("Accept", "application/json");

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        if (request.Headers.Contains(header.Key))
                            request.Headers.Remove(header.Key);

                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                request.Content = content;

                var response = await SendRawAsync(request, bearerToken);//.ConfigureAwait(ConfigureAwait);
                if (response == null || response.Content == null) return null;

                var result = await response.Content.ReadAsStringAsync();//.ConfigureAwait(ConfigureAwait);
                return result;
            }
            catch (AggregateException exception)
            {
                throw exception.Flatten();
            }
        }

        public async Task<HttpResponseMessage> SendRawAsync(HttpRequestMessage request, string bearerToken = null)
        {
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    using (var client = new HttpClient(handler))
                    {
                        if (bearerToken != null)
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                        var result = await client.SendAsync(request);//.ConfigureAwait(ConfigureAwait);
                        result.EnsureSuccessStatusCode();
                        return result;
                    }
                }
            }
            catch (AggregateException exception)
            {
                throw exception.Flatten();
            }
        }
    }
}