namespace P5_ExpressVoiture.Models.Entities
{
    public class Finance
    {
        public int Id { get; set; }
        public int VoitureID { get; set; }
        public Voiture Voiture { get; set; }
        public decimal? PrixAchat { get; set; }
        public decimal? PrixVente { get; set; }
    }
}
