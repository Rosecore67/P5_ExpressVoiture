namespace P5_ExpressVoiture.Models.Entities
{
    public class Reparation
    {
        public int Id { get; set; }
        public int VoitureID { get; set; }
        public DateTime DateReparation { get; set; }
        public decimal CoutReparation { get; set; }
        public int TypeReparationID { get; set; }

        public virtual TypeReparation TypeReparation { get; set; }
        public virtual Voiture Voiture { get; set; }
    }
}
