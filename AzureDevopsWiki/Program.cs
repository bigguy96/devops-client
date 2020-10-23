using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureDevOpsWiki
{
    internal class Program
    {
        private const string Token = "";
        private const string BaseUri = "";
        private static readonly HttpClient Client = new HttpClient();

        private static async Task Main()
        {
            var sb = new StringBuilder("");
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{Token}"));
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            var wikiDetails = await WikiDetails("/Documentation");

            foreach (var subPage in wikiDetails.subPages)
            {
                var subPages = await WikiDetails(subPage.path);

                foreach (var details in subPages.subPages)
                {
                    var detailsPages = await WikiDetails(details.path);
                    sb.AppendLine(detailsPages.content);
                }
            }

            Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }

        private static async Task<WikiDetails> WikiDetails(string path)
        {
            var getResult = await Client.GetAsync(string.Format(BaseUri, path));
            var response = await getResult.Content.ReadAsStringAsync();
            var wikiDetails = JsonSerializer.Deserialize<WikiDetails>(response);
            return wikiDetails;
        }
    }
}

//https://docs.microsoft.com/en-us/rest/api/azure/devops/wiki/pages/get?view=azure-devops-rest-5.0
//https://docs.microsoft.com/en-us/rest/api/azure/devops/wiki/pages/update?view=azure-devops-rest-6.0
//https://docs.microsoft.com/en-us/azure/devops/integrate/get-started/rest/samples?view=azure-devops
//https://stackoverflow.com/questions/54404646/update-wiki-page-of-tfs-by-calling-rest-api
//https://stackoverflow.com/questions/57079316/how-to-programmatically-list-wiki-pages-with-azure-dev-ops-service-rest-api
