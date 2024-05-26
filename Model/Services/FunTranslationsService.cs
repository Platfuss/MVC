using Model.Services.Strategies;
using MVC.Model.Repositories;

namespace MVC.Model.Services;

public class FunTranslationsService(ITranslationRepository translationRepository, ITranslationProvider translationProvider) : ITranslateService
{
    private readonly ITranslationRepository _translationRepository = translationRepository;
    private readonly ITranslationProvider _translationProvider = translationProvider;

    public async Task<string?> TranslateAsync(string input)
    {
        string? response = await _translationProvider.GetTranslationAsync(input);
        if (response == null || !(await _translationRepository.AddAsync(source: this, input, response)))
            return null;

        return response;
    }
}
