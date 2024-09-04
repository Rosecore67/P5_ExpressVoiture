using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IServices;
using P5_ExpressVoiture.Models.Services;
using P5_ExpressVoiture.Models.ViewModels;

namespace P5_ExpressVoiture.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReparationController : Controller
    {
        private readonly IReparationService _reparationService;
        private readonly ITypeReparationService _typeReparationService;
        private readonly IVoitureService _voitureService;

        public ReparationController(IReparationService reparationService, ITypeReparationService typeReparationService, IVoitureService voitureService)
        {
            _reparationService = reparationService;
            _typeReparationService = typeReparationService;
            _voitureService = voitureService;
        }

        public async Task<IActionResult> Index(int voitureId)
        {
            var reparations = await _reparationService.GetReparationsByVoitureIdAsync(voitureId);
            var voiture = await _voitureService.GetVoitureByIdAsync(voitureId);

            if (voiture == null)
            {
                return NotFound();
            }

            var model = reparations.Select(r => new ReparationIndexViewModel
            {
                Id = r.Id,
                VoitureID = r.VoitureID,
                DateReparation = r.DateReparation,
                CoutReparation = r.CoutReparation,
                TypeReparationID = r.TypeReparationID,
                NomTypeReparation = r.TypeReparation.NomTypeReparation,
            }).ToList();

            ViewBag.VoitureId = voitureId;

            return View(model);
        }

        // GET: Reparation/Index
        public async Task<IActionResult> Create(int voitureId)
        {
            var model = new ReparationViewModel
            {
                VoitureID = voitureId,
                DateReparation = DateTime.Now,
                TypeReparations = await _typeReparationService.GetAllTypeReparationsAsync()
            };

            return View(model);
        }

        // POST: Reparation/Create
        [HttpPost]
        public async Task<IActionResult> Create(ReparationViewModel model)
        {
            ModelState.Remove("TypeReparations");
            if (ModelState.IsValid)
            {
                var reparation = new Reparation
                {
                    VoitureID = model.VoitureID,
                    DateReparation = model.DateReparation,
                    CoutReparation = model.CoutReparation,
                    TypeReparationID = model.TypeReparationID
                };

                await _reparationService.AddReparationAsync(reparation);

                TempData["SuccessMessage"] = "La réparation a été ajoutée avec succès !";
                return RedirectToAction("Index", new { voitureId = model.VoitureID });
            }
            
            model.TypeReparations = await _typeReparationService.GetAllTypeReparationsAsync();
            return View(model);
        }

        // GET: /Reparation/Edit/1
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var reparation = await _reparationService.GetReparationByIdAsync(id);
            if (reparation == null) return NotFound();

            var model = new ReparationViewModel
            {
                Id = reparation.Id,
                VoitureID = reparation.VoitureID,
                DateReparation = reparation.DateReparation,
                CoutReparation = reparation.CoutReparation,
                TypeReparationID = reparation.TypeReparationID,
                TypeReparations = await _typeReparationService.GetAllTypeReparationsAsync()
            };

            return View(model);
        }

        // POST: /Reparation/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(ReparationViewModel model)
        {
            ModelState.Remove("TypeReparations");
            if (ModelState.IsValid)
            {
                var reparation = await _reparationService.GetReparationByIdAsync(model.Id);
                if (reparation == null) return NotFound();

                reparation.DateReparation = model.DateReparation;
                reparation.CoutReparation = model.CoutReparation;
                reparation.TypeReparationID = model.TypeReparationID;

                await _reparationService.UpdateReparationAsync(reparation);

                TempData["SuccessMessage"] = "La réparation a été modifiée avec succès !";
                return RedirectToAction("Index", new { voitureId = model.VoitureID });
            }

            model.TypeReparations = await _typeReparationService.GetAllTypeReparationsAsync();
            return View(model);
        }

        // POST: Reparation/Delete/1
        [HttpGet]
        public async Task<IActionResult> Delete(int id, int voitureId)
        {
            await _reparationService.DeleteReparationAsync(id);

            TempData["SuccessMessage"] = "La réparation a été supprimée avec succès !";
            return RedirectToAction("Index", new { voitureId });
        }
    }
}
