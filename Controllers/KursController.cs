﻿using KursKayir.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KursKayir.Controllers
{
    public class KursController : Controller
    {

        private DataContext _context;

        public KursController(DataContext gelendata)
        {
            _context = gelendata;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Kurs model)
        {
            _context.Kurslar.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Kurs");

        }

        public async Task<IActionResult> Index()
        {
            var kurslar = await _context.Kurslar.Include(k=>k.Ogretmen).ToListAsync();
            return View(kurslar);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurslar
                .Include(k => k.KursKayitlari)
                .ThenInclude(k => k.Ogrenci)
                .FirstOrDefaultAsync(kurs => kurs.KursID == id);
            //  var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o => o.OgrenciKimlik == id);
            //Gelen id veritabainda bir karsiligi olmayabilir
            if (kurs == null)
            {
                return NotFound();
            }
            return View(kurs);
            //Bu bilgiler edit.cshtml icindeki modele gidecek
        }
        [HttpGet]



        [HttpGet]


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.Kurslar.FindAsync(id);

            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);

        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs == null)
            {
                return NotFound();
            }
            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");


        }
    }
}
