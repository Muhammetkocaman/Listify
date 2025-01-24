using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_uyg.Models;
using web_uyg.Data;

namespace web_uyg.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AlisverisListesiContext _context;

    public HomeController(ILogger<HomeController> logger, AlisverisListesiContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var sonUrunler = await _context.AlisverisListesi
            .OrderByDescending(u => u.EklenmeTarihi)
            .ToListAsync();
        return View(sonUrunler);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
