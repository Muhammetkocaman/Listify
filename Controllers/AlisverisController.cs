using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_uyg.Data;
using web_uyg.Models;

namespace web_uyg.Controllers
{
    public class AlisverisController : Controller
    {
        private readonly AlisverisListesiContext _context;

        public AlisverisController(AlisverisListesiContext context)
        {
            _context = context;
        }

        // GET: Alisveris
        public async Task<IActionResult> Index(string aramaMetni, string filtre)
        {
            var urunler = from u in _context.AlisverisListesi select u;

            if (!string.IsNullOrEmpty(aramaMetni))
            {
                urunler = urunler.Where(u => u.UrunAdi.Contains(aramaMetni));
            }

            switch (filtre)
            {
                case "alinanlar":
                    urunler = urunler.Where(u => u.AlındiMi);
                    break;
                case "alinmayanlar":
                    urunler = urunler.Where(u => !u.AlındiMi);
                    break;
            }

            return View(await urunler.ToListAsync());
        }

        // GET: Alisveris/Ekle
        public IActionResult Ekle()
        {
            return View();
        }

        // POST: Alisveris/Ekle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle([Bind("UrunAdi,Miktar,AlındiMi")] AlisverisUrunu urun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(urun);
        }

        // GET: Alisveris/Duzenle/5
        public async Task<IActionResult> Duzenle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.AlisverisListesi.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }
            return View(urun);
        }

        // POST: Alisveris/Duzenle/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Duzenle(int id, [Bind("Id,UrunAdi,Miktar,AlındiMi")] AlisverisUrunu urun)
        {
            if (id != urun.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunExists(urun.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(urun);
        }

        // POST: Alisveris/Sil/5
        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sil(int id)
        {
            var urun = await _context.AlisverisListesi.FindAsync(id);
            if (urun != null)
            {
                _context.AlisverisListesi.Remove(urun);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Alisveris/DurumGuncelle/5
        [HttpPost]
        public async Task<IActionResult> DurumGuncelle(int id)
        {
            var urun = await _context.AlisverisListesi.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }

            urun.AlındiMi = !urun.AlındiMi;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Alisveris/ListeyiTemizle
        [HttpPost]
        public async Task<IActionResult> ListeyiTemizle(bool sadeceAlinanlar = false)
        {
            var silinecekUrunler = sadeceAlinanlar 
                ? _context.AlisverisListesi.Where(u => u.AlındiMi)
                : _context.AlisverisListesi;

            _context.AlisverisListesi.RemoveRange(silinecekUrunler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunExists(int id)
        {
            return _context.AlisverisListesi.Any(e => e.Id == id);
        }
    }
}
