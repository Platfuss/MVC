using Microsoft.AspNetCore.Mvc;
using MVC.Model;
using MVC.Model.Services;
using System.Diagnostics;

namespace MVC.Controllers;
public class HomeController(ITranslateService translateService) : Controller
{
    private readonly ITranslateService _translateService = translateService;

    public IActionResult Index(TranslationModel? model = null)
    {
        return View(model ?? new());
    }

    public async Task<IActionResult> TranslateAsync(TranslationModel model)
    {
        string? result = await _translateService.TranslateAsync(model.Input);
        if (result == null)
            return RedirectToAction("Error");

        model.Result = result;

        return RedirectToAction("Index", model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
