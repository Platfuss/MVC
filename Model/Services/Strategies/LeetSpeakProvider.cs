﻿using MVC.Model.Services.Helpers;
using Newtonsoft.Json;

namespace Model.Services.Strategies;
public class LeetSpeakProvider : ITranslationProvider
{
    private readonly HttpClient _httpClient = new() { BaseAddress = new Uri(@"https://api.funtranslations.com/") };

    public string? GetTranslation(string input)
    {
        var content = new FormUrlEncodedContent(new Dictionary<string, string>() {
            { "text", input }
        });

        string resultAsString = _httpClient.PostAsync(@"translate/leetspeak.json", content).Result.Content.ReadAsStringAsync().Result;
        var responseConverted = JsonConvert.DeserializeObject<FuntranslationsResponse>(resultAsString);
        if (responseConverted?.Contents?.Translated == null)
            return null;

        return responseConverted.Contents.Translated;
    }
}
