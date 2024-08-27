using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IServices;
using P5_ExpressVoiture.Models.ViewModels;

namespace P5_ExpressVoiture.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TypeReparationController : Controller
    {
        private readonly ITypeReparationService _typeReparationService;

        public TypeReparationController(ITypeReparationService typeReparationService)
        {
            _typeReparationService = typeReparationService;
        }

        // GET: TypeReparation/Index
        public async Task<IActionResult> Index()
        {
            var types = await _typeReparationService.GetAllTypeReparationsAsync();
            return View(types);
        }

        // GET: TypeReparation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeReparation/Create
        [HttpPost]
        public async Task<IActionResult> Create(TypeReparationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var typeReparation = new TypeReparation
                {
                    NomTypeReparation = model.NomTypeReparation
                };
                await _typeReparationService.AddTypeReparationAsync(typeReparation);
                return RedirectToAction("Index", "Admin");
            }
            return View(model);
        }

        // GET: TypeReparation/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var type = await _typeReparationService.GetTypeReparationByIdAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            var model = new TypeReparationViewModel
            {
                Id = type.Id,
                NomTypeReparation = type.NomTypeReparation
            };

            return View(model);
        }

        // POST: TypeReparation/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(TypeReparationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var typeReparation = new TypeReparation
                {
                    Id = model.Id,
                    NomTypeReparation = model.NomTypeReparation
                };

                await _typeReparationService.UpdateTypeReparationAsync(typeReparation);
                return RedirectToAction("Index", "Admin");
            }
            return View(model);
        }

        // POST: TypeReparation/Delete/1
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _typeReparationService.DeleteTypeReparationAsync(id);
            return RedirectToAction("Index", "Admin");
        }
    }
}
