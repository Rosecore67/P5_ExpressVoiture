using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;
using P5_ExpressVoiture.Models.Interfaces.IServices;

namespace P5_ExpressVoiture.Models.Services
{
    public class ReparationService : IReparationService
    {
        private readonly IReparationRepository _reparationRepository;

        public ReparationService(IReparationRepository reparationRepository)
        {
            _reparationRepository = reparationRepository;
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
        }

        public async Task UpdateReparationAsync(Reparation reparation)
        {
            _reparationRepository.Update(reparation);
            await _reparationRepository.SaveChangesAsync();
        }

        public async Task DeleteReparationAsync(int id)
        {
            var reparation = await _reparationRepository.GetByIdAsync(id);
            if (reparation != null)
            {
                _reparationRepository.Delete(reparation);
                await _reparationRepository.SaveChangesAsync();
            }
        }
    }
}