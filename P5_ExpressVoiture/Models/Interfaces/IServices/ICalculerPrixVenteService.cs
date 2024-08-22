namespace P5_ExpressVoiture.Models.Interfaces.IServices
{
    public interface ICalculerPrixVenteService
    {
        Task<decimal> CalculatePrixVenteAsync(int voitureId, decimal prixAchat);
    }
}
