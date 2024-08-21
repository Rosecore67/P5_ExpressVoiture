using Microsoft.AspNetCore.Identity;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;
using P5_ExpressVoiture.Models.Interfaces.IServices;

namespace P5_ExpressVoiture.Models.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly IUtilisateurRepository _utilisateurRepository;
        private readonly UserManager<Utilisateur> _userManager;

        public UtilisateurService(IUtilisateurRepository utilisateurRepository, UserManager<Utilisateur> userManager)
        {
            _utilisateurRepository = utilisateurRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Utilisateur>> GetAllUtilisateursAsync()
        {
            return await _utilisateurRepository.GetAllAsync();
        }

        public async Task<Utilisateur> GetUtilisateurByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<Utilisateur> GetUtilisateurByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> RegisterUtilisateurAsync(Utilisateur utilisateur, string password)
        {
            return await _userManager.CreateAsync(utilisateur, password);
        }

        public async Task<IdentityResult> UpdateUtilisateurAsync(Utilisateur utilisateur)
        {
            return await _userManager.UpdateAsync(utilisateur);
        }

        public async Task<IdentityResult> DeleteUtilisateurAsync(Utilisateur utilisateur)
        {
            return await _userManager.DeleteAsync(utilisateur);
        }

        public async Task<bool> CheckPasswordAsync(Utilisateur utilisateur, string password)
        {
            return await _userManager.CheckPasswordAsync(utilisateur, password);
        }

        public async Task<IList<string>> GetRolesAsync(Utilisateur utilisateur)
        {
            return await _userManager.GetRolesAsync(utilisateur);
        }

        public async Task<IdentityResult> AddToRoleAsync(Utilisateur utilisateur, string role)
        {
            return await _userManager.AddToRoleAsync(utilisateur, role);
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(Utilisateur utilisateur, string role)
        {
            return await _userManager.RemoveFromRoleAsync(utilisateur, role);
        }

        public async Task<bool> IsInRoleAsync(Utilisateur utilisateur, string role)
        {
            return await _userManager.IsInRoleAsync(utilisateur, role);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(Utilisateur utilisateur)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(utilisateur);
        }

        public async Task<IdentityResult> ResetPasswordAsync(Utilisateur utilisateur, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(utilisateur, token, newPassword);
        }
    }
}
