using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace PortfolioDotnet.Models
{
    public class Projects
    {
        public class Owner
        {
            public string Login { get; set; }
            public string StarredUrl { get; set; }
        }

        //public string starred_url { get; set; }
        //public class User
        //{           
        //}
        public static List<Projects> GetProjects(string login, string starred_url)
        {
            var client = new RestClient("https://api.github.com");
            var request = new RestRequest("/users/steve-burton/starred/?client_id=" + EnvironmentVariables.ClientId + "&client_secret=" + EnvironmentVariables.ClientSecret, Method.GET);
            client.Authenticator = new HttpBasicAuthenticator(EnvironmentVariables.ClientId, EnvironmentVariables.ClientSecret);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            var projectList = JsonConvert.DeserializeObject<List<Projects>>(jsonResponse["projects"].ToString());

            return projectList;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => 
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
