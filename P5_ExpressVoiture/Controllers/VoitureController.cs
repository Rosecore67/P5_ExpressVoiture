using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IServices;
using P5_ExpressVoiture.Models.ViewModels;

namespace P5_ExpressVoiture.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class VoitureController : Controller
    {
        private readonly IVoitureService _voitureService;
        private readonly IMarqueService _marqueService;
        private readonly IModeleService _modeleService;
        private readonly IFinanceService _financeService;
        private readonly IReparationService _reparationService;

        public VoitureController(
            IVoitureService voitureService,
            IMarqueService marqueService,
            IModeleService modeleService,
            IFinanceService financeService,
            IReparationService reparationService)
        {
            _voitureService = voitureService;
            _marqueService = marqueService;
            _modeleService = modeleService;
            _financeService = financeService;
            _reparationService = reparationService;
        }

        // GET: /Voiture/Index
        // Afficher la liste des voitures
        public async Task<IActionResult> Index()
        {
            var voitures = await _voitureService.GetAllVoituresAsync();

            var model = voitures.Select(v => new VoitureIndexViewModel
            {
                Id = v.Id,
                CodeVIN = v.CodeVIN,
                Marque = v.MarqueID > 0 ? _marqueService.GetMarqueByIdAsync(v.MarqueID).Result?.NomMarque : "Marque inconnue",
                Modele = v.ModeleID > 0 ? _modeleService.GetModeleByIdAsync(v.ModeleID).Result?.NomModele : "Modèle inconnu",
                Année = v.Année,
                Finition = v.Finition,
                DateAchat = v.DateAchat,
                EstDisponible = v.EstDisponible,
                Image = v.Image,
                PrixVente = v.Finance?.PrixVente ?? 0m
            }).ToList();

            return View(model);
        }

        // GET: /Voiture/Create
        // Afficher le formulaire de création de voiture
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new VoitureViewModel
            {
                Marques = await _marqueService.GetAllMarquesAsync(),
                Modeles = await _modeleService.GetAllModelesAsync()
            };
            return View(model);
        }

        // POST: /Voiture/Create
        // Créer une nouvelle voiture
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(VoitureViewModel model)
        {
            ModelState.Remove("Marques");
            ModelState.Remove("Modeles");
            if (ModelState.IsValid)
            {
                string imagePath = null;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    imagePath = Path.Combine(uploads, model.ImageFile.FileName);

                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    imagePath = "images/" + model.ImageFile.FileName;
                }

                var voiture = new Voiture
                {
                    CodeVIN = model.CodeVIN,
                    MarqueID = model.MarqueID,
                    ModeleID = model.ModeleID,
                    Année = model.Année,
                    Finition = model.Finition,
                    DateAchat = model.DateAchat,
                    DateDisponibiliteVente = model.DateDisponibiliteVente,
                    Image = imagePath,
                    EstDisponible = model.EstDisponible
                };

                await _voitureService.AddVoitureAsync(voiture);

                return RedirectToAction("Index");
            }

            model.Marques = await _marqueService.GetAllMarquesAsync();
            model.Modeles = await _modeleService.GetAllModelesAsync();
            return View(model);
        }

        // GET: /Voiture/Details/1
        // Détails d'une voiture
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var voiture = await _voitureService.GetVoitureByIdAsync(id);
            if (voiture == null) return NotFound();

            var model = new VoitureDetailsViewModel
            {
                Id = voiture.Id,
                CodeVIN = voiture.CodeVIN,
                Marque = voiture.Marque?.NomMarque,
                Modele = voiture.Modele?.NomModele,
                Année = voiture.Année,
                Finition = voiture.Finition,
                DateAchat = voiture.DateAchat,
                DateDisponibiliteVente = voiture.DateDisponibiliteVente,
                DateVente = voiture.DateVente,
                EstDisponible = voiture.EstDisponible,
                Image = voiture.Image,
                PrixVente = voiture.Finance?.PrixVente
            };

            return View(model);
        }

        // GET: /Voiture/Edit/1
        // Afficher le formulaire d'édition d'une voiture
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var voiture = await _voitureService.GetVoitureByIdAsync(id);
            if (voiture == null) return NotFound();

            var model = new VoitureViewModel
            {
                Id = voiture.Id,
                CodeVIN = voiture.CodeVIN,
                MarqueID = voiture.MarqueID,
                ModeleID = voiture.ModeleID,
                Année = voiture.Année,
                Finition = voiture.Finition,
                DateAchat = voiture.DateAchat,
                DateDisponibiliteVente = voiture.DateDisponibiliteVente,
                EstDisponible = voiture.EstDisponible,
                Marques = await _marqueService.GetAllMarquesAsync(),
                Modeles = await _modeleService.GetAllModelesAsync(),
                Image = voiture.Image
            };

            return View(model);
        }

        // POST: /Voiture/Edit/1
        // Mettre à jour une voiture
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, VoitureViewModel model)
        {
            ModelState.Remove("Marques");
            ModelState.Remove("Modeles");
            if (ModelState.IsValid)
            {
                var voiture = await _voitureService.GetVoitureByIdAsync(id);
                if (voiture == null) return NotFound();

                voiture.CodeVIN = model.CodeVIN;
                voiture.MarqueID = model.MarqueID;
                voiture.ModeleID = model.ModeleID;
                voiture.Année = model.Année;
                voiture.Finition = model.Finition;
                voiture.DateAchat = model.DateAchat;
                voiture.DateDisponibiliteVente = model.DateDisponibiliteVente;
                voiture.EstDisponible = model.EstDisponible;

                if (model.ImageFile != null)
                {
                    string imagePath = Path.Combine("wwwroot/images", model.ImageFile.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }
                    voiture.Image = "images/" + model.ImageFile.FileName;
                }

                await _voitureService.UpdateVoitureAsync(voiture);

                return RedirectToAction("Index");
            }

            model.Marques = await _marqueService.GetAllMarquesAsync();
            model.Modeles = await _modeleService.GetAllModelesAsync();
            return View(model);
        }

        // GET: /Voiture/Delete/1
        // Afficher la page de suppression d'une voiture
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var voiture = await _voitureService.GetVoitureByIdAsync(id);
            if (voiture == null) return NotFound();

            return View(voiture);
        }

        // POST: /Voiture/Delete/1
        // Confirmer la suppression d'une voiture
        //[Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _voitureService.DeleteVoitureAsync(id);
            return RedirectToAction("Index");
        }       
    }
}