using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace EsignApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var handler = new HttpClientHandler()
            {
                SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
            };
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://esign.uzex.uz")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(new
            {
                signedContent = "abcd",
                requestId = "1",
                inn = "123456787",
                pinfl = "12345671234567",
                checkCertificateValidAtSigningTime = false
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = await client.PostAsync("/Sign/VerifySign", content);

            request.EnsureSuccessStatusCode();
            
            var response = await request.Content.ReadAsStringAsync();

            Console.WriteLine(response);
        }
    }
}
