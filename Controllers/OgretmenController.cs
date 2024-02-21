using KursKayir.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KursKayir.Controllers
{
    public class OgretmenController : Controller
    {
        private DataContext _dataContext;

        public OgretmenController(DataContext context)
        {
            _dataContext = context;
        }


        public async Task<IActionResult> Index()
        {
           
            return View(await _dataContext.Ogretmenler.ToListAsync());
        }
        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogretmen model)
        {
            _dataContext.Ogretmenler.Add(model);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index","Ogretmen");
            
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var ogrenci = await _dataContext.Ogretmenler.FindAsync(id);

            if(ogrenci == null)
            {
                return View("Error");
            }
            return View(ogrenci);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id)
        {

            var ogr = await _dataContext.Ogretmenler.FindAsync(id);
            if (ogr == null)
            {
                return NotFound();
            }
            _dataContext.Ogretmenler.Remove(ogr);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var ogretmen = await _dataContext.Ogretmenler
                .FirstOrDefaultAsync(ogr =>ogr.OgretmenId == id);

            if(ogretmen == null)
            {
                return NotFound();
            }

            return View(ogretmen);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(int id, Ogretmen model)
        {
            if (id != model.OgretmenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(model);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dataContext.Ogretmenler.Any(o => o.OgretmenId == model.OgretmenId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("index");
            }
            return View(model);
        }


    }
}
