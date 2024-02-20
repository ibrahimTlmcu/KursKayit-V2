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

   
        public async Task<IActionResult> Create(Ogretmen model)
        {
            _dataContext.Ogretmenler.Add(model);
            await _dataContext.SaveChangesAsync();
            return View(model);
            
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



    }
}
