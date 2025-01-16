using System.Net.Http.Headers;
using Botvex.osu.Services.Interfaces;
using Newtonsoft.Json;

namespace Botvex.osu.Services;

public class OsuApiService : IOsuApiService
{
    private readonly HttpClient _httpClient;
    private const string BaseAddress = "https://osu.ppy.sh/api/v2";

    public OsuApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private async Task<string> GetBearerToken()
    {
        var request = new HttpRequestMessage()
        {
            RequestUri = new Uri("https://osu.ppy.sh/oauth/token"),
            Method = HttpMethod.Post
        };

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        // request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

        //Todo: store these credentials in the appsettings
        var content = new Dictionary<string, string>
        {
            { "client_id", "28349" },
            { "client_secret", "zO5U5ABjU1fZJ12AzT7tS6goxoxB6TkjEBox2lH8" },
            { "grant_type", "client_credentials" },
            { "scope", "public" }
        };

        request.Content = new FormUrlEncodedContent(content);

        var response = await _httpClient.SendAsync(request);
        var responseBody = JsonConvert.DeserializeObject<AuthSuccess>(
            await response.Content.ReadAsStringAsync()
        );

        if (responseBody is null)
        {
            return JsonConvert
                .DeserializeObject<AuthFailure>(await response.Content.ReadAsStringAsync())!
                .Error;
        }

        return "Bearer " + responseBody.Access_token;
    }

    private class AuthSuccess
    {
        public string Token_type { get; set; } = string.Empty;
        public string Expires_in { get; set; } = string.Empty;
        public string Access_token { get; set; } = string.Empty;
    }

    private class AuthFailure
    {
        public string Error { get; set; } = string.Empty;
        public string Error_description { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
