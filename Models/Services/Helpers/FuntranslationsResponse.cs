using Newtonsoft.Json;

namespace MVC.Models.Services.Helpers;

public partial class FuntranslationsResponse
{
    [JsonProperty("success")]
    public Success Success { get; set; }

    [JsonProperty("contents")]
    public Contents Contents { get; set; }
}

public partial class Contents
{
    [JsonProperty("translated")]
    public string Translated { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("translation")]
    public string Translation { get; set; }
}

public partial class Success
{
    [JsonProperty("total")]
    public long Total { get; set; }
}
