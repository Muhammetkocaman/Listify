using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_uyg.Data;
using web_uyg.Models;

namespace web_uyg.Controllers;

public class KategoriController : Controller
{
    private readonly AlisverisListesiContext _context;

    public KategoriController(AlisverisListesiContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var kategoriler = await _context.Kategoriler
            .Include(k => k.Urunler)
            .ToListAsync();
        return View(kategoriler);
    }

    public IActionResult Ekle()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Ekle(Kategori kategori)
    {
        if (ModelState.IsValid)
        {
            _context.Add(kategori);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(kategori);
    }

    public async Task<IActionResult> Duzenle(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var kategori = await _context.Kategoriler.FindAsync(id);
        if (kategori == null)
        {
            return NotFound();
        }
        return View(kategori);
    }

    [HttpPost]
    public async Task<IActionResult> Duzenle(int id, Kategori kategori)
    {
        if (id != kategori.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(kategori);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Kategoriler.AnyAsync(k => k.Id == id))
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
        return View(kategori);
    }
}
