namespace Manila.AirFrog.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net.Http;
    using Newtonsoft.Json;

    class Utility
    {
        public static async Task<string> HttpJsonRequestPosterAsync(object obj, string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    if (response.StatusCode >= System.Net.HttpStatusCode.BadRequest)
                    {
                        throw new Exception(string.Format("HttpJsonRequestPoster failed to send to {0} with return code {1} and msg {2}", url, response.StatusCode, response.Content));
                    }
                    return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string HttpJsonRequestPoster(object obj, string url)
        {
            return HttpJsonRequestPosterAsync(obj, url).Result;
        }

        public static Uri CombineUri(string baseUri, string relativeOrAbsoluteUri)
        {
            return new Uri(new Uri(baseUri), relativeOrAbsoluteUri);
        }

        public static string CombineUriToString(string baseUri, string relativeOrAbsoluteUri)
        {
            return new Uri(new Uri(baseUri), relativeOrAbsoluteUri).ToString();
        }

    }

    public class ProcessResult
    {
        public bool Result;
        public string ErrorMessage;
    }
}
