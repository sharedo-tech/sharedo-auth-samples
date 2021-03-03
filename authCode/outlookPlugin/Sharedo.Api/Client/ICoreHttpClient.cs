using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sharedo.Api.Client
{
    public interface ICoreHttpClient
    {
        bool ConfigureAwait { get; set; }
        Task<string> GetAsync(string url, Dictionary<string, string> headers = null, string bearerToken=null);
        Task<TResponseModel> GetAsync<TResponseModel>(string url, Dictionary<string, string> headers = null, string bearerToken=null);
        Task<string> PostAsync(string url, Dictionary<string, string> headers = null, string bearerToken = null);
        Task<string> PostAsync<TRequestModel>(string url, TRequestModel model, Dictionary<string, string> headers = null, string bearerToken = null);
        Task<TResponseModel> PostAsync<TResponseModel>(string url, Dictionary<string, string> headers = null, string bearerToken = null);
        Task<TResponseModel> PostAsync<TRequestModel, TResponseModel>(string url, TRequestModel model, Dictionary<string, string> headers = null, string bearerToken = null);
        Task<string> PutAsync(string url, Dictionary<string, string> headers = null, string bearerToken = null);
        Task<string> PutAsync<TRequestModel>(string url, TRequestModel model, Dictionary<string, string> headers = null, string bearerToken = null);
        Task<TResponseModel> PutAsync<TResponseModel>(string url, Dictionary<string, string> headers = null, string bearerToken = null);
        Task<TResponseModel> PutAsync<TRequestModel, TResponseModel>(string url, TRequestModel model, Dictionary<string, string> headers = null, string bearerToken = null);
        Task<string> SendAsync(HttpMethod method, string url, string body, Dictionary<string, string> headers = null, string bearerToken = null);
        Task<string> SendAsync<TRequestModel>(HttpMethod method, string url, TRequestModel model, Dictionary<string, string> headers = null, string bearerToken = null);
        Task<TResponseModel> SendAsync<TRequestModel, TResponseModel>(HttpMethod method, string url, TRequestModel model, Dictionary<string, string> headers = null, string bearerToken = null);
        Task<TResponseModel> SendAsync<TResponseModel>(HttpMethod method, string url, Dictionary<string, string> headers = null, string bearerToken = null);
        Task<string> SendFileAsync(HttpMethod method, string url, byte[] file, Dictionary<string, string> headers = null, string bearerToken = null);
        Task<string> SendRawAsync(HttpMethod method, string url, HttpContent content, Dictionary<string, string> headers = null, string bearerToken = null);
        Task<HttpResponseMessage> SendRawAsync(HttpRequestMessage request, string bearerToken = null);
    }
}