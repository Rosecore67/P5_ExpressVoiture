using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IServices;
using P5_ExpressVoiture.Models.ViewModels;

namespace P5_ExpressVoiture.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ModeleController : Controller
    {
        private readonly IModeleService _modeleService;

        public ModeleController(IModeleService modeleService)
        {
            _modeleService = modeleService;
        }

        // GET: Modele/Index
        public async Task<IActionResult> Index()
        {
            var modeles = await _modeleService.GetAllModelesAsync();
            return View(modeles);
        }

        // GET: Modele/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Modele/Create
        [HttpPost]
        public async Task<IActionResult> Create(ModeleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var modele = new Modele
                {
                    NomModele = model.NomModele
                };
                await _modeleService.AddModeleAsync(modele);
                return RedirectToAction("Index", "Admin");
            }
            return View(model);
        }

        // GET: Modele/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var modele = await _modeleService.GetModeleByIdAsync(id);
            if (modele == null)
            {
                return NotFound();
            }

            var model = new ModeleViewModel
            {
                Id = modele.Id,
                NomModele = modele.NomModele
            };

            return View(model);
        }

        // POST: Modele/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ModeleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var modele = new Modele
                {
                    Id = model.Id,
                    NomModele = model.NomModele
                };

                await _modeleService.UpdateModeleAsync(modele);
                return RedirectToAction("Index", "Admin");
            }

            return View(model);
        }

        // POST: Modele/Delete/1
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _modeleService.DeleteModeleAsync(id);
            return RedirectToAction("Index", "Admin");
        }
    }
}
