using P5_ExpressVoiture.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class ModeleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du modèle est requis.")]
        [Display(Name = "Nom du modèle")]
        [StringLength(100, ErrorMessage = "Le nom du modèle ne doit pas dépasser 100 caractères.")]
        public string NomModele { get; set; }

    }
}
