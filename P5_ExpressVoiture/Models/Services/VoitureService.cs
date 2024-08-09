using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;
using P5_ExpressVoiture.Models.Interfaces.IServices;

namespace P5_ExpressVoiture.Models.Services
{
    public class VoitureService : IVoitureService
    {
        private readonly IVoitureRepository _voitureRepository;

        public VoitureService(IVoitureRepository voitureRepository)
        {
            _voitureRepository = voitureRepository;
        }

        public async Task<IEnumerable<Voiture>> GetAllVoituresAsync()
        {
            return await _voitureRepository.GetAllAsync();
        }

        public async Task<Voiture> GetVoitureByIdAsync(int id)
        {
            return await _voitureRepository.GetByIdAsync(id);
        }

        public async Task<Voiture> GetVoitureByVinAsync(string vin)
        {
            return await _voitureRepository.GetByVinAsync(vin);
        }

        public async Task AddVoitureAsync(Voiture voiture)
        {
            await _voitureRepository.AddAsync(voiture);
        }

        public async Task UpdateVoitureAsync(Voiture voiture)
        {
            _voitureRepository.Update(voiture);
            await _voitureRepository.SaveChangesAsync();
        }

        public async Task DeleteVoitureAsync(int id)
        {
            var voiture = await _voitureRepository.GetByIdAsync(id);
            if (voiture != null)
            {
                _voitureRepository.Delete(voiture);
                await _voitureRepository.SaveChangesAsync();
            }
        }
    }
}