using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IServices;
using P5_ExpressVoiture.Models.ViewModels;

namespace P5_ExpressVoiture.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRoleController : Controller
    {
        private readonly IUserRoleService _roleService;
        private readonly UserManager<Utilisateur> _userManager;

        public UserRoleController(IUserRoleService roleService, UserManager<Utilisateur> userManager)
        {
            _roleService = roleService;
            _userManager = userManager;
        }

        // GET: Role/Index
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRolesAsync();

            var model = roles.Select(role => new UserRoleViewModel
            {
                Id = role.Id,
                NomRole = role.NomRole
            }).ToList();

            return View(model);
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        public async Task<IActionResult> Create(UserRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Vérifiez si un rôle avec ce nom existe déjà, y compris les rôles soft deleted
                var existingRole = await _roleService.GetByNameIncludeSoftDeleteAsync(model.NomRole);

                if (existingRole != null)
                {
                    // Si le rôle existe et est soft deleted, le réactiver
                    if (existingRole.SoftDelete)
                    {
                        existingRole.SoftDelete = false;
                        var updateResult = await _roleService.UpdateRoleAsync(existingRole);

                        if (updateResult.Succeeded)
                        {
                            TempData["SuccessMessage"] = "Le rôle a été réactivé avec succès.";
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            foreach (var error in updateResult.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Un rôle avec ce nom existe déjà.");
                        return View(model);
                    }
                }

                // Si le rôle n'existe pas, créer un nouveau rôle
                var role = new UserRole { Name = model.NomRole, NomRole = model.NomRole };
                var result = await _roleService.CreateRoleAsync(role);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Le rôle a été ajouté avec succès !";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // GET: Role/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var model = new UserRoleViewModel
            {
                Id = role.Id,
                NomRole = role.NomRole
            };

            return View(model);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleService.GetRoleByIdAsync(model.Id);
                if (role == null)
                {
                    return NotFound();
                }

                role.Name = model.NomRole;
                role.NomRole = model.NomRole;

                var result = await _roleService.UpdateRoleAsync(role);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Le rôle a été modifiée avec succès !";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // POST: Role/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleService.DeleteRoleAsync(role);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Le rôle a été supprimée avec succès !";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error deleting role.");
            return View("Index", await _roleService.GetAllRolesAsync());
        }
    }
}
