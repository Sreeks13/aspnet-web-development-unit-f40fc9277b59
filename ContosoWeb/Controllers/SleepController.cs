using ContosoWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoWeb.Controllers;

public class SleepController : Controller
{
    public IActionResult Index()
    {
        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "sleep_log.txt");

        var analyser = new SleepAnalyser();
        var analysis = analyser.Analyse(filePath);

        return View(analysis);
    }
}
