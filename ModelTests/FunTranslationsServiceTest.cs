using Model.Services.Strategies;
using Moq;
using MVC.Model.Repositories;
using MVC.Model.Services;

namespace ModelTests;

public class FunTranslationsServiceTest
{
    private readonly FunTranslationsService _translationsService;
    private readonly Mock<ITranslationRepository> _translationRepoMock = new();
    private readonly Mock<ITranslationProvider> _translationProviderMock = new();

    public FunTranslationsServiceTest()
    {
        _translationsService = new(_translationRepoMock.Object, _translationProviderMock.Object);
    }

    [Fact]
    public async void Translate_ShouldReturnResult_OnSuccessfulTranslation()
    {
        string input = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
        string output = "1Or3m |Pzum |)()L()R zIt Am37, c0|\\|53C73tur AdYp!ZC!]\\[G EL1t, z3d |)0 3Iu5m0d t3mp0R I|\\|kY|)1|)u]\\[7 UT laboR3 eT |)OL0r3 m@6N4 4l|QUa.";
        _translationRepoMock.Setup(x => x.AddAsync(It.IsAny<ITranslateService>(), input, output))
            .Returns(Task.FromResult(true));
        _translationProviderMock.Setup(x => x.GetTranslationAsync(input)).Returns(Task.FromResult((string?)output));

        string? translation = await _translationsService.TranslateAsync(input);

        Assert.Equal(translation, output);
    }

    [Fact]
    public async void Translate_ShouldReturnNull_OnFailedTranslation()
    {
        string input = "test";
        _translationProviderMock.Setup(x => x.GetTranslationAsync(It.IsAny<string>())).Returns(Task.FromResult((string?)null));
        _translationRepoMock.Setup(x => x.AddAsync(It.IsAny<ITranslateService>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.FromResult(true));

        string? translation = await _translationsService.TranslateAsync(input);

        Assert.Null(translation);
    }

    [Fact]
    public async void Translate_ShouldReturnNull_OnFailedDatabaseInsert()
    {
        string test = "test";
        _translationProviderMock.Setup(x => x.GetTranslationAsync(It.IsAny<string>())).Returns(Task.FromResult((string?)test));
        _translationRepoMock.Setup(x => x.AddAsync(It.IsAny<ITranslateService>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.FromResult(false));

        string? translation = await _translationsService.TranslateAsync(test);

        Assert.Null(translation);
    }
}