using Microsoft.EntityFrameworkCore;
using Model.Services.Strategies;
using MVC.Model.DbContexts;
using MVC.Model.Repositories;
using MVC.Model.Services;
using System.Collections;

namespace MVC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<TranslationContext>(options =>
        {
            IDictionary envVar = Environment.GetEnvironmentVariables();
            string dataSource = GetDictionaryValueOrDefault(envVar, "Server", "localhost");
            string port = GetDictionaryValueOrDefault(envVar, "Port", "1433");
            string initialCatalog = GetDictionaryValueOrDefault(envVar, "InitialCatalog", "FunnyTranslator");
            string saPassword = GetDictionaryValueOrDefault(envVar, "SaPassword", "Password_123#");

            options.UseSqlServer($@"Server={dataSource},{port};Initial Catalog={initialCatalog};User ID=SA;TrustServerCertificate=True;Password={saPassword};Connect Timeout=30");
        });
        builder.Services.AddTransient<ITranslationRepository, TranslationRepository>();
        builder.Services.AddTransient<ITranslationProvider, LeetSpeakProvider>();
        builder.Services.AddTransient<ITranslateService, FunTranslationsService>();

        var app = builder.Build();
        using (IServiceScope scope = app.Services.CreateScope())
        {
            TranslationContext context = scope.ServiceProvider.GetRequiredService<TranslationContext>();
            context.Database.Migrate();
        }
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }

    private static string GetDictionaryValueOrDefault(IDictionary dictionary, string key, string defaultValue) => (dictionary?.Contains(key) ?? false) ? dictionary[key]!.ToString()! : defaultValue;
}
