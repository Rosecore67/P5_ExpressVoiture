namespace P5_ExpressVoiture.Utils
{
    public class CalculePrix
    {
        public static decimal CalculerPrixVente(decimal prixAchat, decimal totalCoutReparations, decimal marge)
        {
            return prixAchat + totalCoutReparations + marge;
        }
    }
}
