using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_uyg.Data;

namespace web_uyg.Controllers;

public class IstatistikController : Controller
{
    private readonly AlisverisListesiContext _context;

    public IstatistikController(AlisverisListesiContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new Dictionary<string, object>();

        // Toplam ürün sayısı
        viewModel["ToplamUrun"] = await _context.AlisverisListesi.CountAsync();

        // Alınan ürün sayısı ve oranı
        var alinanUrunSayisi = await _context.AlisverisListesi.CountAsync(u => u.AlındiMi);
        viewModel["AlinanUrun"] = alinanUrunSayisi;
        viewModel["AlinanUrunOrani"] = viewModel["ToplamUrun"].ToString() != "0" 
            ? (double)alinanUrunSayisi / (int)viewModel["ToplamUrun"] * 100 
            : 0;

        // Kategori bazlı ürün sayıları
        viewModel["KategoriBazliUrunler"] = await _context.Kategoriler
            .Select(k => new
            {
                k.Ad,
                UrunSayisi = k.Urunler.Count
            })
            .ToListAsync();

        // En çok eklenen 5 ürün
        viewModel["EnCokEklenenUrunler"] = await _context.AlisverisListesi
            .GroupBy(u => u.UrunAdi)
            .Select(g => new
            {
                UrunAdi = g.Key,
                Sayi = g.Count()
            })
            .OrderByDescending(x => x.Sayi)
            .Take(5)
            .ToListAsync();

        return View(viewModel);
    }
}
