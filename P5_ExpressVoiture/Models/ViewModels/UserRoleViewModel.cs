using System.ComponentModel.DataAnnotations;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class UserRoleViewModel
    {
        [Required(ErrorMessage = "Le nom du rôle est requis.")]
        public string NomRole { get; set; }
    }
}
