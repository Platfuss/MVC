using MVC.Model.DbContexts;
using MVC.Model.Services;

namespace MVC.Model.Repositories;

public interface ITranslationRepository
{
    bool Add(ITranslateService source, string input, string result);
    IEnumerable<LogItem> GetAll();
}
