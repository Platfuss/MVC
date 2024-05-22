using MVC.Models.Repositories;
using MVC.Models.Services.Helpers;
using Newtonsoft.Json;

namespace MVC.Models.Services;

public class LeetSpeakService(ITranslationRepository translationRepository) : ITranslateService
{
    private readonly ITranslationRepository _translationRepository = translationRepository;

    private static readonly HttpClient _httpClient = new() { BaseAddress = new Uri(@"https://api.funtranslations.com/") };

    public string? Translate(string input)
    {
        var content = new FormUrlEncodedContent(new Dictionary<string, string>() {
            { "text", input }
        });

        string resultAsString = _httpClient.PostAsync(@"translate/leetspeak.json", content).Result.Content.ReadAsStringAsync().Result;
        var responseConverted = JsonConvert.DeserializeObject<FuntranslationsResponse>(resultAsString);
        if (responseConverted?.Contents?.Translated == null)
            return null;

        string response = responseConverted.Contents.Translated;
        if (!_translationRepository.Add(source: this, input, response))
            return null;

        return response;
    }
}
