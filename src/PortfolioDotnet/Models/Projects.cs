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
        public string Name { get; set; }
        public string HtmlUrl { get; set; }

        //public static List<JObject> GetProjects()
        public static List<Projects> GetProjects()
        {
            var client = new RestClient("https://api.github.com");
            //var request = new RestRequest("search/repositories?q=user:steve-burton&sort=stars:&order=desc&?page=1&per_page=1?client_id=" + EnvironmentVariables.ClientId + "&client_secret=" + EnvironmentVariables.ClientSecret, Method.GET);
            var request = new RestRequest("search/repositories?q=user:steve-burton&sort=stars:&order=asc&?per_page=3?client_id=" + EnvironmentVariables.ClientId + "&client_secret=" + EnvironmentVariables.ClientSecret, Method.GET);
            request.AddHeader("User-Agent", "steve-burton");
            //client.Authenticator = new HttpBasicAuthenticator(EnvironmentVariables.ClientId, EnvironmentVariables.ClientSecret);
            var response = new RestResponse();
            Console.WriteLine(".............response: ................" + response);
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            Console.WriteLine(".............json response: ................" + jsonResponse);
            //List<JObject> projectList = JsonConvert.DeserializeObject<List<JObject>>(jsonResponse["items"].ToString());
            var projectList = JsonConvert.DeserializeObject<List<Projects>>(jsonResponse["items"].ToString());
            Debug.WriteLine(jsonResponse);
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
