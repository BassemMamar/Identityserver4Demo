using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientDemo
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            try
            {
                // discover endpoints from metadata
                var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
                if (disco.IsError)
                {
                    Console.WriteLine(disco.Error);
                    Console.ReadLine();
                    return;
                }

                // request token
                var tokenClient = new TokenClient(disco.TokenEndpoint, "ConsoleClientDemo", "secret");
                var tokenResponse = await tokenClient.RequestClientCredentialsAsync("ApiDemo");

                //var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.ConsoleClientDemo", "secret");
                //var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "ApiDemo");

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine(tokenResponse.Json);
                Console.WriteLine("\n\n");

                // call api
                var client = new HttpClient();
                client.SetBearerToken(tokenResponse.AccessToken);

                var response = await client.GetAsync("http://localhost:5001/api/identity");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(JArray.Parse(content));
                }

                Console.WriteLine("///////////////////////////");
                Console.ReadLine();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                Console.ReadLine();
            }

        }
    }
}