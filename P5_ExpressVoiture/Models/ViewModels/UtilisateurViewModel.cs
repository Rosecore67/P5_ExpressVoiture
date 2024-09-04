using System.ComponentModel.DataAnnotations;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class UtilisateurViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Le nom d'utilisateur est requis.")]
        [Display(Name = "Nom de l'utilisateur")]
        public string NomUtilisateur { get; set; }

        [Required(ErrorMessage = "L'email est requis.")]
        [Display(Name = "Adresse mail")]
        [EmailAddress(ErrorMessage = "Format de l'email invalide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("MotDePasse", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public string ConfirmMotDePasse { get; set; }

        // Liste des rôles disponibles
        public List<UserRoleViewModel>? Roles { get; set; }

        // ID du rôle sélectionné
        [Required(ErrorMessage = "Veuillez sélectionner un rôle.")]
        public string SelectedRoleId { get; set; }
    }
}

