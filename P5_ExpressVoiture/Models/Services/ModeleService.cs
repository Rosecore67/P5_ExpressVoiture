using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;
using P5_ExpressVoiture.Models.Interfaces.IServices;

namespace P5_ExpressVoiture.Models.Services
{
    public class ModeleService : IModeleService
    {
        private readonly IModeleRepository _modeleRepository;

        public ModeleService(IModeleRepository modeleRepository)
        {
            _modeleRepository = modeleRepository;
        }

        public async Task<IEnumerable<Modele>> GetAllModelesAsync()
        {
            return await _modeleRepository.GetAllAsync();
        }

        public async Task<Modele> GetMarqueByIdIncludingSoftDeletedAsync(int id)
        {
            return await _modeleRepository.GetByIdIncludingSoftDeletedAsync(id);
        }

        public async Task<Modele> GetModeleByIdAsync(int id)
        {
            return await _modeleRepository.GetByIdAsync(id);
        }

        public async Task AddModeleAsync(Modele modele)
        {
            await _modeleRepository.AddAsync(modele);
        }

        public async Task UpdateModeleAsync(Modele modele)
        {
            _modeleRepository.Update(modele);
            await _modeleRepository.SaveChangesAsync();
        }

        public async Task DeleteModeleAsync(int id)
        {
            var modele = await _modeleRepository.GetByIdAsync(id);
            if (modele != null)
            {
                _modeleRepository.Delete(modele);
                await _modeleRepository.SaveChangesAsync();
            }
        }
    }
}
