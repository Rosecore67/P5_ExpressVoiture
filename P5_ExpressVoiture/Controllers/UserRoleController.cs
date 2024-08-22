using Microsoft.AspNetCore.Mvc;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IServices;
using P5_ExpressVoiture.Models.ViewModels;

namespace P5_ExpressVoiture.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly IUserRoleService _roleService;

        public UserRoleController(IUserRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: Role/Index
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRolesAsync();

            // Conversion de UserRole en UserRoleViewModel
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
                var role = new UserRole { Name = model.NomRole, NomRole = model.NomRole };
                var result = await _roleService.CreateRoleAsync(role);
                if (result.Succeeded)
                {
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
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error deleting role.");
            return View("Index", await _roleService.GetAllRolesAsync());
        }
    }
}
