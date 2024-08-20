using Microsoft.AspNetCore.Mvc;
using P5_ExpressVoiture.Models.Interfaces.IServices;

namespace P5_ExpressVoiture.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMarqueService _marqueService;
        private readonly IModeleService _modeleService;
        private readonly ITypeReparationService _typeReparationService;

        public AdminController(IMarqueService marqueService, IModeleService modeleService, ITypeReparationService typeReparationService)
        {
            _marqueService = marqueService;
            _modeleService = modeleService;
            _typeReparationService = typeReparationService;
        }

        // GET: Admin/Index
        public async Task<IActionResult> Index()
        {
            var marques = await _marqueService.GetAllMarquesAsync();
            var modeles = await _modeleService.GetAllModelesAsync();
            var typeReparations = await _typeReparationService.GetAllTypeReparationsAsync();

            ViewBag.Marques = marques;
            ViewBag.Modeles = modeles;
            ViewBag.TypeReparation = typeReparations;

            return View();
        }
    }
}
