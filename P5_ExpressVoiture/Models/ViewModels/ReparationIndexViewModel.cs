namespace P5_ExpressVoiture.Models.ViewModels
{
    public class ReparationIndexViewModel
    {
        public int Id { get; set; }
        public int VoitureID { get; set; }
        public DateTime DateReparation { get; set; }
        public decimal CoutReparation { get; set; }
        public int TypeReparationID { get; set; }
        public string NomTypeReparation { get; set; }
    }
}
