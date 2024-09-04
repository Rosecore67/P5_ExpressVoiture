using System.ComponentModel.DataAnnotations;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class ReparationIndexViewModel
    {
        public int Id { get; set; }
        public int VoitureID { get; set; }
        [Display(Name = "Date de la réparation")]
        public DateTime DateReparation { get; set; }
        [Display(Name = "Coût de la réparation")]
        public decimal CoutReparation { get; set; }
        public int TypeReparationID { get; set; }
        [Display(Name = "Nom du type de réparation effectuée")]
        public string NomTypeReparation { get; set; }
    }
}
