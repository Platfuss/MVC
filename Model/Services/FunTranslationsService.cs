using Model.Services.Strategies;
using MVC.Model.Repositories;

namespace MVC.Model.Services;

public class FunTranslationsService(ITranslationRepository translationRepository, ITranslationProvider translationProvider) : ITranslateService
{
    private readonly ITranslationRepository _translationRepository = translationRepository;
    private readonly ITranslationProvider _translationProvider = translationProvider;

    public string? Translate(string input)
    {
        string? response = _translationProvider.GetTranslation(input);
        if (response == null || !_translationRepository.Add(source: this, input, response))
            return null;

        return response;
    }
}
