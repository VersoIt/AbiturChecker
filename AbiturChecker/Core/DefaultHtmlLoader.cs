using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AbiturChecker.Core.Abstract;

namespace AbiturChecker.Core
{
    public class DefaultHtmlLoader : IHtmlLoader<string>
    {

        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> LoadDataAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);

            string source = "";

            if (response != null && response.StatusCode == HttpStatusCode.OK)
                source = await response.Content.ReadAsStringAsync();

            return source;
        }
    }
}
