using MVC.Model.DbContexts;
using MVC.Model.Services;

namespace MVC.Model.Repositories;

public class TranslationRepository(TranslationContext context) : ITranslationRepository
{
    private readonly TranslationContext _context = context;

    public bool Add(ITranslateService source, string input, string result)
    {
        string sourceName = source.GetType().Name.Replace("Service", string.Empty, StringComparison.CurrentCultureIgnoreCase);
        LogItem item = new(sourceName, input, result);
        _context.Add(item);
        return _context.SaveChanges() > 0;
    }

    public IEnumerable<LogItem> GetAll()
    {
        return _context.LogItems.AsEnumerable();
    }
}
