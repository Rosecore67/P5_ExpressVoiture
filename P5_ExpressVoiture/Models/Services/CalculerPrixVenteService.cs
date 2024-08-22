using P5_ExpressVoiture.Models.Interfaces.IRepositories;
using P5_ExpressVoiture.Models.Interfaces.IServices;

namespace P5_ExpressVoiture.Models.Services
{
    public class CalculerPrixVenteService : ICalculerPrixVenteService
    {
        private readonly IReparationRepository _reparationRepository;

        public CalculerPrixVenteService(IReparationRepository reparationRepository)
        {
            _reparationRepository = reparationRepository;
        }

        public async Task<decimal> CalculatePrixVenteAsync(int voitureId, decimal prixAchat)
        {
            var totalCoutReparations = await _reparationRepository.GetTotalCoutReparationsByVoitureId(voitureId);
            var totalPrixVente = prixAchat + totalCoutReparations + 500; // Marge fixe de 500
            return totalPrixVente;
        }
    }
}
