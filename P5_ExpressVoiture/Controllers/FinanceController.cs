using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IServices;
using P5_ExpressVoiture.Models.ViewModels;

namespace P5_ExpressVoiture.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FinanceController : Controller
    {
        private readonly IFinanceService _financeService;
        private readonly IVoitureService _voitureService;
        private readonly IMarqueService _marqueService;
        private readonly IModeleService _modeleService;

        public FinanceController(IFinanceService financeService, IVoitureService voitureService, IMarqueService marqueService, IModeleService modeleService)
        {
            _financeService = financeService;
            _voitureService = voitureService;
            _marqueService = marqueService;
            _modeleService = modeleService;
        }

        // GET: Finance/Index?voitureId=1
        public async Task<IActionResult> Index(int voitureId)
        {
            var finance = await _financeService.GetFinanceByVoitureIdAsync(voitureId);
            if (finance == null)
            {
                return RedirectToAction("Create", new { voitureId });
            }

            var voiture = await _voitureService.GetVoitureByIdAsync(voitureId);
            var model = new FinanceViewModel
            {
                VoitureID = voitureId,
                PrixAchat = finance.PrixAchat ?? 0,
                PrixVente = finance.PrixVente ?? 0
            };

            if (voiture == null)
            {
                return NotFound();
            }

            // Récupérer la marque et le modèle
            var marque = await _marqueService.GetMarqueByIdAsync(voiture.MarqueID);
            var modele = await _modeleService.GetModeleByIdAsync(voiture.ModeleID);

            // Assigner le nom de la voiture au ViewBag
            ViewBag.VoitureNom = $"{marque.NomMarque} {modele.NomModele}";


            return View(model);
        }

        // GET: Finance/Create?voitureId=1
        public IActionResult Create(int voitureId)
        {
            var model = new FinanceViewModel { VoitureID = voitureId };
            return View(model);
        }

        // POST: Finance/Create
        [HttpPost]
        public async Task<IActionResult> Create(FinanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var finance = new Finance
                {
                    VoitureID = model.VoitureID,
                    PrixAchat = model.PrixAchat,
                    PrixVente = model.PrixVente
                };

                await _financeService.AddFinanceAsync(finance);

                TempData["SuccessMessage"] = "La finance du véhicule a été ajoutée avec succès !";
                return RedirectToAction("Index", "Voiture");
            }
            return View(model);
        }

        // GET: Finance/Edit/1
        public async Task<IActionResult> Edit(int voitureId)
        {
            var finance = await _financeService.GetFinanceByVoitureIdAsync(voitureId);
            if (finance == null)
            {
                return NotFound();
            }

            var model = new FinanceViewModel
            {
                FinanceId = finance.Id,
                VoitureID = finance.VoitureID,
                PrixAchat = finance.PrixAchat ?? 0,
                PrixVente = finance.PrixVente ?? 0
            };

            return View(model);
        }

        // POST: Finance/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(FinanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var finance = await _financeService.GetFinanceByIdAsync(model.FinanceId);
                if (finance == null)
                {
                    return NotFound();
                }

                finance.PrixAchat = model.PrixAchat;
                finance.PrixVente = model.PrixVente;

                await _financeService.UpdateFinanceAsync(finance);

                TempData["SuccessMessage"] = "La finance du véhicule a été modifiée avec succès !";
                return RedirectToAction("Index", new { voitureId = model.VoitureID });
            }
            return View(model);
        }

        // GET : Finance/EditPrixVente/1
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditPrixVente(int voitureId)
        {
            var finance = await _financeService.GetFinanceByVoitureIdAsync(voitureId);
            if (finance == null)
            {
                return NotFound();
            }

            var model = new EditPrixVenteViewModel
            {
                FinanceID = finance.Id,
                VoitureID = voitureId,
                PrixVente = finance.PrixVente ?? 0
            };

            return View(model);
        }

        //POST : Finance/EditPrixVente/1
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditPrixVente(EditPrixVenteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var finance = await _financeService.GetFinanceByIdAsync(model.FinanceID);
                if (finance == null)
                {
                    return NotFound();
                }

                finance.PrixVente = model.PrixVente;

                await _financeService.UpdateFinancePrixVenteAsync(finance);

                TempData["SuccessMessage"] = "Le prix de vente a été mis à jour avec succès.";
                return RedirectToAction("Details", "Voiture", new { id = model.VoitureID });
            }

            return View(model);
        }

        // POST: Finance/Delete/1
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int voitureId)
        {
            await _financeService.DeleteFinanceAsync(id);

            TempData["SuccessMessage"] = "La finance du véhicule a été supprimée avec succès !";
            return RedirectToAction("Index", "Voiture");
        }
    }
}
