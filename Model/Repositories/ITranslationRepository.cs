using MVC.Model.DbContexts;
using MVC.Model.Services;

namespace MVC.Model.Repositories;

public interface ITranslationRepository
{
    Task<bool> AddAsync(ITranslateService source, string input, string result);
    IEnumerable<LogItem> GetAll();
}
