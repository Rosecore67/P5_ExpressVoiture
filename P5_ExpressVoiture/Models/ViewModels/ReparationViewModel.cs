using Microsoft.AspNetCore.Mvc.ModelBinding;
using P5_ExpressVoiture.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class ReparationViewModel
    {
        public int Id { get; set; }
        public int VoitureID { get; set; }

        [Required(ErrorMessage = "La date de la réparation est requise.")]
        [Display(Name = "Date de la réparation")]
        [DataType(DataType.Date)]
        public DateTime DateReparation { get; set; }

        [Required(ErrorMessage = "Le coût de la réparation est requis.")]
        [Display(Name = "Coût de la réparation")]
        [Range(0, double.MaxValue, ErrorMessage = "Le coût doit être un nombre positif.")]
        public decimal CoutReparation { get; set; }

        [Required(ErrorMessage = "Le type de réparation est requis.")]
        public int TypeReparationID { get; set; }

        public IEnumerable<TypeReparation> TypeReparations { get; set; } // Liste des types de réparations disponibles
        

    }
}
