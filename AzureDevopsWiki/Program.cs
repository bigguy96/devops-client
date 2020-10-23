using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureDevOpsWiki
{
    class Program
    {
        private const string Token = "token";
        private const string BaseUri = "url";
        private static readonly HttpClient Client = new HttpClient { BaseAddress = new Uri(BaseUri) };


        static async Task Main(string[] args)
        {

            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{Token}"));
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            var getResult = await Client.GetAsync(BaseUri);
            var etag = getResult.Headers.GetValues("ETag");
            var response = await getResult.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<WikiDetails>(response);

            Console.WriteLine(json.content);
            Console.ReadLine();
        }
    }
}

//https://docs.microsoft.com/en-us/rest/api/azure/devops/wiki/pages/get?view=azure-devops-rest-5.0
//https://docs.microsoft.com/en-us/rest/api/azure/devops/wiki/pages/update?view=azure-devops-rest-6.0
//https://docs.microsoft.com/en-us/azure/devops/integrate/get-started/rest/samples?view=azure-devops
//https://stackoverflow.com/questions/54404646/update-wiki-page-of-tfs-by-calling-rest-api
//https://stackoverflow.com/questions/57079316/how-to-programmatically-list-wiki-pages-with-azure-dev-ops-service-rest-api
