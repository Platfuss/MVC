using MVC.Models.DbContexts;
using MVC.Models.Services;

namespace MVC.Models.Repositories;

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
}
