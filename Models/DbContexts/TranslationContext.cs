using Microsoft.EntityFrameworkCore;

namespace MVC.Models.DbContexts;

public class TranslationContext(DbContextOptions<TranslationContext> options) : DbContext(options)
{
    public DbSet<LogItem> LogItems { get; set; }
}
