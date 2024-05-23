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
    public void Translate_ShouldReturnResult_OnSuccessfulTranslation()
    {
        string input = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
        string output = "1Or3m |Pzum |)()L()R zIt Am37, c0|\\|53C73tur AdYp!ZC!]\\[G EL1t, z3d |)0 3Iu5m0d t3mp0R I|\\|kY|)1|)u]\\[7 UT laboR3 eT |)OL0r3 m@6N4 4l|QUa.";
        _translationRepoMock.Setup(x => x.Add(It.IsAny<ITranslateService>(), input, output))
            .Returns(() => true);
        _translationProviderMock.Setup(x => x.GetTranslation(input)).Returns(output);

        string? translation = _translationsService.Translate(input);

        Assert.Equal(translation, output);
    }
}