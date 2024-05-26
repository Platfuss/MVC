namespace MVC.Model.Services;

public interface ITranslateService
{
    public Task<string?> TranslateAsync(string input);
}
