using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TranslateApiDemo
{
    class Program
    {
        private const string apiKey = "_yourapikey_";
        static async Task Main(string[] args)
        {
            await LanguageTranslate();
            await MicrosoftTranslate();
            await GoogleTranslate();
        }

        private static async Task LanguageTranslate()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://language-translation.p.rapidapi.com/translateLanguage/translate"),
                Headers = {
                        { "x-rapidapi-host", "language-translation.p.rapidapi.com" },
                        { "x-rapidapi-key", apiKey }
                },
                Content = new StringContent("{\r\n    \"target\": \"tr\",\r\n    \"text\": \"About Us\",\r\n    \"type\": \"plain\"\r\n}")
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
        }
        private static async Task MicrosoftTranslate()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://microsoft-translator-text.p.rapidapi.com/translate?to=tr&api-version=3.0&profanityAction=NoAction&textType=plain"),
                Headers = {
                    { "x-rapidapi-host", "microsoft-translator-text.p.rapidapi.com" },
                    { "x-rapidapi-key", apiKey }
                },
                Content = new StringContent("[{\"Text\": \"About Us\"}]")
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                dynamic result = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

                Console.WriteLine(result[0].translations[0].text);
            }
        }
        private static async Task GoogleTranslate()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://google-translate1.p.rapidapi.com/language/translate/v2"),
                Headers =
                {
                    { "x-rapidapi-host", "google-translate1.p.rapidapi.com" },
                    { "x-rapidapi-key", apiKey }
                },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "q", "About Us!" },
                    { "target", "tr" },
                    { "source", "en" }
                })
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
        }
    }
}
