using Microsoft.AspNetCore.Mvc;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IServices;
using P5_ExpressVoiture.Models.ViewModels;

namespace P5_ExpressVoiture.Controllers
{
    public class FinanceController : Controller
    {
        private readonly IFinanceService _financeService;
        private readonly IVoitureService _voitureService;

        public FinanceController(IFinanceService financeService, IVoitureService voitureService)
        {
            _financeService = financeService;
            _voitureService = voitureService;
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
                FinanceId = finance.Id, // Utiliser l'ID de la finance ici
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
                return RedirectToAction("Index", new { voitureId = model.VoitureID });
            }
            return View(model);
        }

        // POST: Finance/Delete/1
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int voitureId)
        {
            await _financeService.DeleteFinanceAsync(id);
            return RedirectToAction("Index", "Voiture");
        }
    }
}
