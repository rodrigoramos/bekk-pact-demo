using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Consumer.Console
{
    public class UserServiceClient
    {
        private readonly HttpClient _client;

        public UserServiceClient(string baseUri = null)
            : this(new HttpClient {BaseAddress = new Uri(baseUri ?? "http://my-api")})
        {
        }

        public UserServiceClient(HttpClient client) 
            => _client = client;

        public async Task<IEnumerable<UserApiResponse>> GetUsers()
        {
            string reasonPhrase;

            var request = new HttpRequestMessage(HttpMethod.Get, "/User");
            request.Headers.Add("Accept", "application/json");

            var response = await _client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            var status = response.StatusCode;

            reasonPhrase =
                response
                    .ReasonPhrase; //NOTE: any Pact mock provider errors will be returned here and in the response body

            request.Dispose();
            response.Dispose();

            if (status == HttpStatusCode.OK)
            {
                return !string.IsNullOrEmpty(content)
                    ? JsonConvert.DeserializeObject<IEnumerable<UserApiResponse>>(content)
                    : null;
            }

            throw new Exception(reasonPhrase);
        }
    }
}
