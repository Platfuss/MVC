namespace Model.Services.Strategies;
public interface ITranslationProvider
{
    public Task<string?> GetTranslationAsync(string input);
}
