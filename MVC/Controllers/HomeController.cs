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

    public async Task<JsonResult> TranslateAsync(string input)
    {
        string? result = await _translateService.TranslateAsync(input);
        if (result == null)
            return Json("Error - translation not found");

        return Json(result);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
