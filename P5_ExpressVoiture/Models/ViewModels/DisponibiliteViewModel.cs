using System.ComponentModel.DataAnnotations;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class DisponibiliteViewModel
    {
        public int VoitureID { get; set; }

        [Required(ErrorMessage = "Le statut de disponibilité est requis.")]
        public bool EstDisponible { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateDisponibiliteVente { get; set; }
    }
}
