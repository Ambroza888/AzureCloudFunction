using System.Net.Http;
using System.Threading.Tasks;
using Valence.Extensions;

namespace Valence.Helper
{
    public class Agent
    {
        static readonly HttpClient client = new HttpClient();
        private static string YELP_API_KEY = DirectoryExtentions.GetLocalSettingJson("ApiKey");
        private static string YELP_BASE_URL = DirectoryExtentions.GetLocalSettingJson("YELP_BASE_URL");

        public static async Task<string> GetYelpApi(string Location, string Categories)
        {
            // System.Console.WriteLine(YELP_API_KEY);
            // System.Console.WriteLine(YELP_BASE_URL);

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + YELP_API_KEY);
            HttpResponseMessage response = await client.
            GetAsync($"https://api.yelp.com/v3/businesses/search?location={Location}&&categories={Categories}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
    }
}