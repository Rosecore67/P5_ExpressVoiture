using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IServices;
using P5_ExpressVoiture.Models.ViewModels;

namespace P5_ExpressVoiture.Controllers
{
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

            var model = new List<VoitureIndexViewModel>();

            foreach (var v in voitures)
            {
                var marque = await _marqueService.GetMarqueByIdAsync(v.MarqueID);
                var modele = await _modeleService.GetModeleByIdAsync(v.ModeleID);
                var coutClient = await _financeService.GetFinanceByVoitureIdAsync(v.Id);
                var prixVente = coutClient?.PrixVente ?? 0;
                var image = string.IsNullOrEmpty(v.Image) ? "~/images/default.png" : v.Image;

                var voiture = new VoitureIndexViewModel
                {
                    Id = v.Id,
                    CodeVIN = v.CodeVIN ?? "",
                    Marque = marque.NomMarque ?? "Marque inconnue",
                    Modele = modele.NomModele ?? "Modele inconnu",
                    Année = v.Année,
                    Finition = v.Finition ?? "",
                    DateAchat = v.DateAchat,
                    DateDisponibiliteVente = v.DateDisponibiliteVente,
                    EstDisponible = v.EstDisponible,
                    Image = image,
                    PrixVente = prixVente
                };

                model.Add(voiture);
            }

            return View(model);

        }

        // GET: /Voiture/Create
        // Afficher le formulaire de création de voiture
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(VoitureViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imagePath = await _voitureService.UploadImageAsync(model.ImageFile);

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
            var marque = await _marqueService.GetMarqueByIdAsync(voiture.MarqueID);
            var modele = await _modeleService.GetModeleByIdAsync(voiture.ModeleID);
            var coutClient = await _financeService.GetFinanceByVoitureIdAsync(voiture.Id);
            var prixVente = coutClient?.PrixVente ?? 0;
            var image = string.IsNullOrEmpty(voiture.Image) ? Url.Content("~/images/default.png") : Url.Content("~/" + voiture.Image);

            var model = new VoitureDetailsViewModel
            {
                Id = voiture.Id,
                CodeVIN = voiture.CodeVIN,
                Marque = marque.NomMarque ?? "Marque inconnue",
                Modele = modele.NomModele ?? "Modèle inconnu",
                Année = voiture.Année,
                Finition = voiture.Finition,
                DateAchat = voiture.DateAchat,
                DateDisponibiliteVente = voiture.DateDisponibiliteVente,
                DateVente = voiture.DateVente,
                EstDisponible = voiture.EstDisponible,
                Image = image,
                PrixVente = prixVente,
            };

            return View(model);
        }

        // GET: /Voiture/Edit/1
        // Afficher le formulaire d'édition d'une voiture
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var voiture = await _voitureService.GetVoitureByIdAsync(id);
            if (voiture == null) return NotFound();
            var marque = await _marqueService.GetAllMarquesAsync();
            var modele = await _modeleService.GetAllModelesAsync();

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
                Marques = marque,
                Modeles = modele,
                Image = voiture.Image
            };

            return View(model);
        }

        // POST: /Voiture/Edit/1
        // Mettre à jour une voiture
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, VoitureViewModel model)
        {
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
                    _voitureService.DeleteImage(voiture.Image);
                    voiture.Image = await _voitureService.UploadImageAsync(model.ImageFile);
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var voiture = await _voitureService.GetVoitureByIdAsync(id);
            if (voiture == null)
            {
                return NotFound();
            }
            var marque = await _marqueService.GetMarqueByIdAsync(voiture.MarqueID);
            var modele = await _modeleService.GetModeleByIdAsync(voiture.ModeleID);

            var voitureViewModel = new VoitureDetailsViewModel
            {
                Id = voiture.Id,
                Marque = marque.NomMarque ?? "Marque inconnue",
                Modele = modele.NomModele ?? "Modèle inconnu",
                Année = voiture.Année,
                Finition = voiture.Finition,
                DateAchat = voiture.DateAchat,
                DateDisponibiliteVente = voiture.DateDisponibiliteVente,
                Image = voiture.Image,
                EstDisponible = voiture.EstDisponible
            };

            return View(voitureViewModel);
        }

        // POST: /Voiture/Delete/1
        // Confirmer la suppression d'une voiture
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voiture = await _voitureService.GetVoitureByIdAsync(id);

            if (voiture == null)
            {
                return NotFound();
            }

            _voitureService.DeleteImage(voiture.Image);
            await _voitureService.DeleteVoitureAsync(id);
            return RedirectToAction("Index");
        }
    }
}