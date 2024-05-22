using MVC.Models.Services;

namespace MVC.Models.Repositories;

public interface ITranslationRepository
{
    bool Add(ITranslateService source, string input, string result);
}
