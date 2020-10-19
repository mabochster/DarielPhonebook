using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dariel.Mahlatse.Phonebook.Core.Rest
{
    public static class RestHelper
    {
        //TODO : further refine the class to be configurable.
        public static async Task<IRestResponse<T>> Get<T>(string resourceLocation, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            //get the data
            //var accessToken = ((ClaimsPrincipal)System.Web.HttpContext.Current.User).FindFirst("access_token").Value;
            var client = new RestClient(resourceLocation);
            RestRequest request = new RestRequest(resourceLocation, Method.GET);
            List<KeyValuePair<string, object>> keyValuePairs = (parameters ?? throw new ArgumentNullException(nameof(parameters))).ToList();
            if (keyValuePairs.Any())
            {
                foreach (var param in keyValuePairs)
                {
                    request.AddParameter(param.Key, param.Value);
                }
            }

            //not using Bearer token JWT authentication, code commented out due to time constraints
            //request.AddParameter("Authorization",
            //       $"Bearer {accessToken}",
            //       ParameterType.HttpHeader);

            return await client.ExecuteGetAsync<T>(request);
        }

        public static async Task<IRestResponse<T>> Save<T>(string resourceLocation, object model)
        {
            //get the data
            //var accessToken = ((ClaimsPrincipal)System.Web.HttpContext.Current.User).FindFirst("access_token").Value;
            var client = new RestClient(resourceLocation);
            RestRequest request = new RestRequest(resourceLocation, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(model);
            //request.AddParameter("Authorization",
            //       $"Bearer {accessToken}",
            //       ParameterType.HttpHeader);

            return await client.ExecutePostAsync<T>(request);
        }

        public static async Task<IRestResponse> Save(string resourceLocation, object model)
        {
            //get the data
            //var accessToken = ((ClaimsPrincipal)System.Web.HttpContext.Current.User).FindFirst("access_token").Value;
            var client = new RestClient(resourceLocation);
            RestRequest request = new RestRequest(resourceLocation, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(model);
            //request.AddParameter("Authorization",
            //       $"Bearer {accessToken}",
            //       ParameterType.HttpHeader);

            return await client.ExecutePostAsync(request);
        }
    }
}
