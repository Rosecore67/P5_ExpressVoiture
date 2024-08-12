using P5_ExpressVoiture.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class ReparationViewModel
    {
        public int VoitureID { get; set; }

        [Required(ErrorMessage = "Le type de réparation est requis.")]
        public int TypeReparationID { get; set; }

        [Required(ErrorMessage = "Le coût de réparation est requis.")]
        [Range(0, double.MaxValue, ErrorMessage = "Le coût de réparation doit être positif.")]
        public decimal CoutReparation { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateReparation { get; set; } = DateTime.Now;

        // Liste des types de réparations disponibles
        public IEnumerable<TypeReparation> TypeReparations { get; set; }
    }
}
