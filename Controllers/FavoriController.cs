using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_uyg.Data;
using web_uyg.Models;

namespace web_uyg.Controllers;

public class FavoriController : Controller
{
    private readonly AlisverisListesiContext _context;

    public FavoriController(AlisverisListesiContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var favoriler = await _context.FavoriUrunler
            .OrderByDescending(f => f.EklenmeTarihi)
            .ToListAsync();
        return View(favoriler);
    }

    public IActionResult Ekle()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Ekle(FavoriUrun favoriUrun)
    {
        if (ModelState.IsValid)
        {
            favoriUrun.EklenmeTarihi = DateTime.Now;
            _context.Add(favoriUrun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(favoriUrun);
    }

    [HttpPost]
    public async Task<IActionResult> Sil(int id)
    {
        var favoriUrun = await _context.FavoriUrunler.FindAsync(id);
        if (favoriUrun != null)
        {
            _context.FavoriUrunler.Remove(favoriUrun);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> ListeyeEkle(int id)
    {
        var favoriUrun = await _context.FavoriUrunler.FindAsync(id);
        if (favoriUrun != null)
        {
            var alisverisUrunu = new AlisverisUrunu
            {
                UrunAdi = favoriUrun.UrunAdi,
                Miktar = favoriUrun.VarsayilanMiktar ?? 1,
                AlındiMi = false,
                EklenmeTarihi = DateTime.Now
            };

            _context.AlisverisListesi.Add(alisverisUrunu);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
