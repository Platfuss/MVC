using Microsoft.EntityFrameworkCore;

namespace MVC.Model.DbContexts;

public class TranslationContext(DbContextOptions<TranslationContext> options) : DbContext(options)
{
    public DbSet<LogItem> LogItems { get; set; }
}
