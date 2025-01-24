using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_uyg.Data;
using web_uyg.Models;

namespace web_uyg.Controllers
{
    public class AlisverisListesiController : Controller
    {
        private readonly AlisverisListesiContext _context;

        public AlisverisListesiController(AlisverisListesiContext context)
        {
            _context = context;
        }

        // Ana sayfa - Liste görüntüleme
        public async Task<IActionResult> Index()
        {
            var liste = await _context.AlisverisListesi
                .Include(u => u.Kategori)
                .OrderByDescending(u => u.EklenmeTarihi)
                .ToListAsync();
            return View(liste);
        }

        // Yeni ürün ekleme sayfası
        public IActionResult Ekle()
        {
            ViewBag.Kategoriler = _context.Kategoriler.OrderBy(k => k.Ad).ToList();
            return View();
        }

        // Yeni ürün ekleme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle([Bind("UrunAdi,Miktar,MiktarBirimi,KategoriId")] AlisverisUrunu alisverisUrunu)
        {
            if (ModelState.IsValid)
            {
                alisverisUrunu.EklenmeTarihi = DateTime.Now;
                alisverisUrunu.AlındiMi = false; // Varsayılan olarak alınmadı
                _context.Add(alisverisUrunu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Kategoriler = _context.Kategoriler.OrderBy(k => k.Ad).ToList();
            return View(alisverisUrunu);
        }

        // Ürün durumunu güncelleme
        [HttpPost]
        public async Task<IActionResult> DurumGuncelle(int id)
        {
            var alisverisOgesi = await _context.AlisverisListesi.FindAsync(id);
            if (alisverisOgesi == null)
            {
                return NotFound();
            }

            alisverisOgesi.AlındiMi = !alisverisOgesi.AlındiMi;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Ürün silme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sil(int id)
        {
            var alisverisOgesi = await _context.AlisverisListesi.FindAsync(id);
            if (alisverisOgesi != null)
            {
                _context.AlisverisListesi.Remove(alisverisOgesi);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Örnek ürünler ekleme
        public async Task<IActionResult> OrnekUrunlerEkle()
        {
            var ornekUrunler = new List<AlisverisUrunu>
            {
                new AlisverisUrunu { UrunAdi = "Süt", Miktar = 2, AlındiMi = false },
                new AlisverisUrunu { UrunAdi = "Ekmek", Miktar = 1, AlındiMi = false },
                new AlisverisUrunu { UrunAdi = "Yumurta", Miktar = 10, AlındiMi = false },
                new AlisverisUrunu { UrunAdi = "Peynir", Miktar = 1, AlındiMi = false },
                new AlisverisUrunu { UrunAdi = "Domates", Miktar = 5, AlındiMi = false },
                new AlisverisUrunu { UrunAdi = "Salatalık", Miktar = 3, AlındiMi = false },
                new AlisverisUrunu { UrunAdi = "Elma", Miktar = 4, AlındiMi = false },
                new AlisverisUrunu { UrunAdi = "Muz", Miktar = 6, AlındiMi = false }
            };

            _context.AlisverisListesi.AddRange(ornekUrunler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}