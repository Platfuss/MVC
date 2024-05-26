using MVC.Model.DbContexts;
using MVC.Model.Services;

namespace MVC.Model.Repositories;

public class TranslationRepository(TranslationContext context) : ITranslationRepository
{
    private readonly TranslationContext _context = context;

    public async Task<bool> AddAsync(ITranslateService source, string input, string result)
    {
        string sourceName = source.GetType().Name.Replace("Service", string.Empty, StringComparison.CurrentCultureIgnoreCase);
        LogItem item = new(sourceName, input, result);
        _context.Add(item);
        return await _context.SaveChangesAsync() > 0;
    }

    public IEnumerable<LogItem> GetAll()
    {
        return _context.LogItems.AsEnumerable();
    }
}
