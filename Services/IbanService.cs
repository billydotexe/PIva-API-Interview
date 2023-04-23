using System;

namespace PIva.Api.Services
{
    public class IbanService
    {
        private readonly string url = @"https://www.iban.com/vat-checker";
        private readonly HttpClient _httpClient;

        public IbanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> LoadPage(string vat)
        {
            var data = new Dictionary<string, string>
            {
                { "vat_id", vat }
            };
            var payload = new FormUrlEncodedContent(data);
            var response = await _httpClient.PostAsync(url, payload);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
