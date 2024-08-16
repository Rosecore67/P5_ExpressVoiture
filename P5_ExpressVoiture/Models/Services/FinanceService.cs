using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;
using P5_ExpressVoiture.Models.Interfaces.IServices;

namespace P5_ExpressVoiture.Models.Services
{
    public class FinanceService : IFinanceService
    {
        private readonly IFinanceRepository _financeRepository;
        private readonly IReparationService _reparationService;

        public FinanceService(IFinanceRepository financeRepository, IReparationService reparationService)
        {
            _financeRepository = financeRepository;
            _reparationService = reparationService;
        }

        public async Task<Finance> GetFinanceByVoitureIdAsync(int voitureId)
        {
            return await _financeRepository.GetFinanceByVoitureIdAsync(voitureId);
        }

        public async Task<IEnumerable<Finance>> GetAllFinancesAsync()
        {
            return await _financeRepository.GetAllAsync();
        }

        public async Task<Finance> GetFinanceByIdAsync(int id)
        {
            return await _financeRepository.GetByIdAsync(id);
        }

        public async Task AddFinanceAsync(Finance finance)
        {
            finance.PrixVente = await CalculatePrixVente(finance.VoitureID, finance.PrixAchat ?? 0);
            await _financeRepository.AddAsync(finance);
            await _financeRepository.SaveChangesAsync();
        }

        public async Task UpdateFinanceAsync(Finance finance)
        {
            finance.PrixVente = await CalculatePrixVente(finance.VoitureID, finance.PrixAchat ?? 0);
            _financeRepository.Update(finance);
            await _financeRepository.SaveChangesAsync();
        }

        public async Task DeleteFinanceAsync(int id)
        {
            var finance = await _financeRepository.GetByIdAsync(id);
            if (finance != null)
            {
                _financeRepository.Delete(finance);
                await _financeRepository.SaveChangesAsync();
            }
        }

        // Calcul du prix de vente d'une voiture
        private async Task<decimal> CalculatePrixVente(int voitureId, decimal prixAchat)
        {
            var totalCoutReparations = await _reparationService.GetTotalCoutReparationsByVoitureId(voitureId);
            var totalPrixVente = prixAchat + totalCoutReparations + 500;
            return totalPrixVente;
        }
    }
}
