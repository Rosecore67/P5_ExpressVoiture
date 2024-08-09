using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;
using P5_ExpressVoiture.Models.Interfaces.IServices;

namespace P5_ExpressVoiture.Models.Services
{
    public class MarqueService : IMarqueService
    {
        private readonly IMarqueRepository _marqueRepository;

        public MarqueService(IMarqueRepository marqueRepository)
        {
            _marqueRepository = marqueRepository;
        }

        public async Task<IEnumerable<Marque>> GetAllMarquesAsync()
        {
            return await _marqueRepository.GetAllAsync();
        }

        public async Task<Marque> GetMarqueByIdAsync(int id)
        {
            return await _marqueRepository.GetByIdAsync(id);
        }

        public async Task AddMarqueAsync(Marque marque)
        {
            await _marqueRepository.AddAsync(marque);
        }

        public async Task UpdateMarqueAsync(Marque marque)
        {
            _marqueRepository.Update(marque);
            await _marqueRepository.SaveChangesAsync();
        }

        public async Task DeleteMarqueAsync(int id)
        {
            var marque = await _marqueRepository.GetByIdAsync(id);
            if (marque != null)
            {
                _marqueRepository.Delete(marque);
                await _marqueRepository.SaveChangesAsync();
            }
        }
    }
}
