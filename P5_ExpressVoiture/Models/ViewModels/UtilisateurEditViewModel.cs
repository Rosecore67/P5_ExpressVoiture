using System.ComponentModel.DataAnnotations;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class UtilisateurEditViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Le nom d'utilisateur est requis.")]
        public string NomUtilisateur { get; set; }

        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "Format de l'email invalide.")]
        public string Email { get; set; }

        // Liste des rôles disponibles
        public List<UserRoleViewModel>? Roles { get; set; }

        // ID du rôle sélectionné
        [Required(ErrorMessage = "Veuillez sélectionner un rôle.")]
        public string SelectedRoleId { get; set; }
    }
}
