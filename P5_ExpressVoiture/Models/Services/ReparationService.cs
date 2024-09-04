using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;
using P5_ExpressVoiture.Models.Interfaces.IServices;

namespace P5_ExpressVoiture.Models.Services
{
    public class ReparationService : IReparationService
    {
        private readonly IReparationRepository _reparationRepository;
        private readonly IFinanceService _financeService;

        public ReparationService(IReparationRepository reparationRepository, IFinanceService financeService)
        {
            _reparationRepository = reparationRepository;
            _financeService = financeService;
        }

        public async Task<IEnumerable<Reparation>> GetAllReparationsAsync()
        {
            return await _reparationRepository.GetAllAsync();
        }

        public async Task<Reparation> GetReparationByIdAsync(int id)
        {
            return await _reparationRepository.GetByIdAsync(id);
        }

        public async Task AddReparationAsync(Reparation reparation)
        {
            await _reparationRepository.AddAsync(reparation);
            await RecalculatePrixVenteAsync(reparation.VoitureID);
        }

        public async Task UpdateReparationAsync(Reparation reparation)
        {
            _reparationRepository.Update(reparation);
            await _reparationRepository.SaveChangesAsync();
            await RecalculatePrixVenteAsync(reparation.VoitureID);
        }

        public async Task DeleteReparationAsync(int id)
        {
            var reparation = await _reparationRepository.GetByIdAsync(id);
            if (reparation != null)
            {
                _reparationRepository.Delete(reparation);
                await _reparationRepository.SaveChangesAsync();
                await RecalculatePrixVenteAsync(reparation.VoitureID);
            }
        }

        // Méthode pour obtenir le coût total des réparations
        public async Task<decimal> GetTotalCoutReparationsByVoitureId(int voitureId)
        {
            return await _reparationRepository.GetTotalCoutReparationsByVoitureId(voitureId);
        }
        // Méthode pour obtenir les réparations d'un véhicule
        public async Task<IEnumerable<Reparation>> GetReparationsByVoitureIdAsync(int voitureId)
        {
            return await _reparationRepository.GetReparationsByVoitureIdAsync(voitureId);
        }
        //Méthode pour recalculer le prix de vente à l'ajout d'une réparation
        private async Task RecalculatePrixVenteAsync(int voitureId)
        {
            var finance = await _financeService.GetFinanceByVoitureIdAsync(voitureId);
            if (finance != null)
            {
                // Le prix de vente est calculé et mis à jour dans la méthode UpdateFinanceAsync
                await _financeService.UpdateFinanceAsync(finance);
            }
        }
    }
}