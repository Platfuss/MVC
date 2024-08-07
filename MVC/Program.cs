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
            //"Default": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FunnyTranslator;Integrated Security=True;Connect Timeout=60"
            string dataSource = (envVar?.Contains("Server") ?? false) ? envVar["Server"]!.ToString()! : @"localhost";
            string initialCatalog = (envVar?.Contains("InitialCatalog") ?? false) ? envVar["InitialCatalog"]!.ToString()! : @"FunnyTranslator";
            string port = (envVar?.Contains("Port") ?? false) ? envVar["Port"]!.ToString()! : "1433";

            options.UseSqlServer($@"Data Source={dataSource},{port};Initial Catalog={initialCatalog};Integrated Security=True;Connect Timeout=60");
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
}
