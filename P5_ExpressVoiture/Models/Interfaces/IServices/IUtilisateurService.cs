using Microsoft.AspNetCore.Identity;
using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IServices
{
    public interface IUtilisateurService
    {
        Task<IEnumerable<Utilisateur>> GetAllUtilisateursAsync();
        Task<Utilisateur> GetUtilisateurByIdAsync(string id);
        Task<Utilisateur> GetUtilisateurByEmailAsync(string email);
        Task<IdentityResult> RegisterUtilisateurAsync(Utilisateur utilisateur, string password);
        Task<IdentityResult> UpdateUtilisateurAsync(Utilisateur utilisateur);
        Task<IdentityResult> DeleteUtilisateurAsync(Utilisateur utilisateur);
        Task<bool> CheckPasswordAsync(Utilisateur utilisateur, string password);
        Task<IList<string>> GetRolesAsync(Utilisateur utilisateur);
        Task<IdentityResult> AddToRoleAsync(Utilisateur utilisateur, string role);
        Task<IdentityResult> RemoveFromRoleAsync(Utilisateur utilisateur, string role);
        Task<bool> IsInRoleAsync(Utilisateur utilisateur, string role);
        Task<string> GeneratePasswordResetTokenAsync(Utilisateur utilisateur);
        Task<IdentityResult> ResetPasswordAsync(Utilisateur utilisateur, string token, string newPassword);
    }
}
