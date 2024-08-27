using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IServices;
using P5_ExpressVoiture.Models.ViewModels;

namespace P5_ExpressVoiture.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UtilisateurController : Controller
    {
        private readonly IUtilisateurService _utilisateurService;
        private readonly IUserRoleService _userRoleService;

        public UtilisateurController(IUtilisateurService utilisateurService, IUserRoleService userRoleService)
        {
            _utilisateurService = utilisateurService;
            _userRoleService = userRoleService;
        }

        // GET: Utilisateur/Index
        public async Task<IActionResult> Index()
        {
            var utilisateurs = await _utilisateurService.GetAllUtilisateursAsync();
            var model = new List<UtilisateurViewModel>();

            foreach (var utilisateur in utilisateurs)
            {
                var roles = await _utilisateurService.GetRolesAsync(utilisateur);
                var utilisateurViewModel = new UtilisateurViewModel
                {
                    Id = utilisateur.Id,
                    Email = utilisateur.Email ?? "Email non précisé",
                    NomUtilisateur = utilisateur.NomUtilisateur,
                    Roles = roles.Select(r => new UserRoleViewModel
                    {
                        Id = r,
                        NomRole = r
                    }).ToList()
                };

                model.Add(utilisateurViewModel);
            }

            return View(model);
        }

        // GET: Utilisateur/Create
        public async Task<IActionResult> Create()
        {
            var roles = await _userRoleService.GetAllRolesAsync();
            var model = new UtilisateurViewModel
            {
                Roles = roles.Select(r => new UserRoleViewModel
                {
                    Id = r.Id,
                    NomRole = r.NomRole
                }).ToList()
            };
            return View(model);
        }

        // POST: Utilisateur/Create
        [HttpPost]
        public async Task<IActionResult> Create(UtilisateurViewModel model)
        {
            if (ModelState.IsValid)
            {
                var utilisateur = new Utilisateur
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NomUtilisateur = model.NomUtilisateur
                };

                var result = await _utilisateurService.RegisterUtilisateurAsync(utilisateur, model.MotDePasse);
                if (result.Succeeded)
                {
                    // Assigner automatiquement le rôle "Visiteur" à chaque nouvel utilisateur
                    await _utilisateurService.AddToRoleAsync(utilisateur, "Visiteur");

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            model.Roles = (await _userRoleService.GetAllRolesAsync())
                .Select(r => new UserRoleViewModel
                {
                    Id = r.Id,
                    NomRole = r.NomRole
                }).ToList();

            return View(model);
        }

        // GET: Utilisateur/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var utilisateur = await _utilisateurService.GetUtilisateurByIdAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            var roles = await _userRoleService.GetAllRolesAsync();
            var userRoles = await _utilisateurService.GetRolesAsync(utilisateur);
            var selectedRoleId = roles.FirstOrDefault(r => r.NomRole == userRoles.FirstOrDefault())?.Id;

            var model = new UtilisateurEditViewModel
            {
                Id = utilisateur.Id,
                Email = utilisateur.Email ?? "Non précisé",
                NomUtilisateur = utilisateur.NomUtilisateur,
                Roles = roles.Select(r => new UserRoleViewModel
                {
                    Id = r.Id,
                    NomRole = r.NomRole
                }).ToList(),
                SelectedRoleId = selectedRoleId ?? string.Empty
            };

            return View(model);
        }

        // POST: Utilisateur/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(UtilisateurEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var utilisateur = await _utilisateurService.GetUtilisateurByIdAsync(model.Id);
                if (utilisateur == null)
                {
                    return NotFound();
                }

                utilisateur.Email = model.Email;
                utilisateur.NomUtilisateur = model.NomUtilisateur;

                var result = await _utilisateurService.UpdateUtilisateurAsync(utilisateur);
                if (result.Succeeded)
                {
                    // Récupère les rôles associés à l'utilisateur
                    var userRoles = await _utilisateurService.GetRolesAsync(utilisateur);

                    // S'il y a des rôles associés, on les retire
                    if (userRoles.Count > 0)
                    {
                        await _utilisateurService.RemoveFromRoleAsync(utilisateur, userRoles[0]);
                    }

                    // Ajouter le nouvel ID de rôle sélectionné
                    if (!string.IsNullOrEmpty(model.SelectedRoleId))
                    {
                        var role = await _userRoleService.GetRoleByIdAsync(model.SelectedRoleId);
                        await _utilisateurService.AddToRoleAsync(utilisateur, role.NomRole);
                    }

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            model.Roles = (await _userRoleService.GetAllRolesAsync()).Select(r => new UserRoleViewModel
            {
                Id = r.Id,
                NomRole = r.NomRole
            }).ToList();


            return View(model);
        }

        // POST: Utilisateur/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var utilisateur = await _utilisateurService.GetUtilisateurByIdAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            var result = await _utilisateurService.DeleteUtilisateurAsync(utilisateur);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Erreur lors de la suppression de l'utilisateur.");
            return View("Index", await _utilisateurService.GetAllUtilisateursAsync());
        }

        // GET: Utilisateur/ResetPassword/5
        public IActionResult ResetPassword(string id)
        {
            var model = new ResetPasswordViewModel { UserId = id };
            return View(model);
        }

        // POST: Utilisateur/ResetPassword/5
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var utilisateur = await _utilisateurService.GetUtilisateurByIdAsync(model.UserId);
                if (utilisateur == null)
                {
                    return NotFound();
                }

                var resetToken = await _utilisateurService.GeneratePasswordResetTokenAsync(utilisateur);
                var result = await _utilisateurService.ResetPasswordAsync(utilisateur, resetToken, model.NewPassword);

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
    }
}
