using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IServices;
using P5_ExpressVoiture.Models.ViewModels;

namespace P5_ExpressVoiture.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MarqueController : Controller
    {
        private readonly IMarqueService _marqueService;

        public MarqueController(IMarqueService marqueService)
        {
            _marqueService = marqueService;
        }

        // GET: Marque/Index
        public async Task<IActionResult> Index()
        {
            var marques = await _marqueService.GetAllMarquesAsync();
            return View(marques);
        }

        // GET: Marque/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marque/Create
        [HttpPost]
        public async Task<IActionResult> Create(MarqueViewModel model)
        {
            if (ModelState.IsValid)
            {
                var marque = new Marque
                {
                    NomMarque = model.NomMarque
                };
                await _marqueService.AddMarqueAsync(marque);
                return RedirectToAction("Index", "Admin");
            }
            return View(model);
        }

        // GET: Marque/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var marque = await _marqueService.GetMarqueByIdAsync(id);
            if (marque == null)
            {
                return NotFound();
            }

            var model = new MarqueViewModel
            {
                Id = marque.Id,
                NomMarque = marque.NomMarque
            };

            return View(model);
        }

        // POST: Marque/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(int id, MarqueViewModel model)
        {
            if (ModelState.IsValid)
            {
                var marque = new Marque
                {
                    Id = model.Id,
                    NomMarque = model.NomMarque
                };

                await _marqueService.UpdateMarqueAsync(marque);
                return RedirectToAction("Index", "Admin");
            }

            return View(model);
        }

        // POST: Marque/Delete/1
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _marqueService.DeleteMarqueAsync(id);
            return RedirectToAction("Index", "Admin");
        }
    }
}
