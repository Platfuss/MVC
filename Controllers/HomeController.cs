using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.Services;
using System.Diagnostics;

namespace MVC.Controllers;
public class HomeController : Controller
{
    private readonly ITranslateService _translateService;

    public HomeController(ITranslateService translateService)
    {
        _translateService = translateService;
    }

    public IActionResult Index(TranslationModel? model = null)
    {
        return View(model ?? new());
    }

    public IActionResult Translate(TranslationModel model)
    {
        string? result = _translateService.Translate(model.Input);
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
