using Microsoft.Extensions.Options;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;
using P5_ExpressVoiture.Models.Interfaces.IServices;
using P5_ExpressVoiture.Utils;

namespace P5_ExpressVoiture.Models.Services
{
    public class FinanceService : IFinanceService
    {
        private readonly IFinanceRepository _financeRepository;
        private readonly IReparationRepository _reparationRepository;
        private readonly decimal _marge;

        public FinanceService(IFinanceRepository financeRepository, IReparationRepository reparationRepository, IOptions<AppSettings> appSettings)
        {
            _financeRepository = financeRepository;
            _reparationRepository = reparationRepository;
            _marge = appSettings.Value.marge;
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
            finance.PrixVente = await calculTauxReparations(finance.VoitureID, finance.PrixAchat ?? 0);

            await _financeRepository.AddAsync(finance);
            await _financeRepository.SaveChangesAsync();
        }

        public async Task UpdateFinanceAsync(Finance finance)
        {
            
            finance.PrixVente = await calculTauxReparations(finance.VoitureID, finance.PrixAchat ?? 0);

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

        private async Task<decimal> calculTauxReparations(int voitureId, decimal prixAchat)
        {
            var totalCoutReparations = await _reparationRepository.GetTotalCoutReparationsByVoitureId(voitureId);
            return CalculePrix.CalculerPrixVente(prixAchat, totalCoutReparations, _marge);
        }
    }
}
