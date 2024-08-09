using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;
using P5_ExpressVoiture.Models.Interfaces.IServices;

namespace P5_ExpressVoiture.Models.Services
{
    public class TypeReparationService : ITypeReparationService
    {
        private readonly ITypeReparationRepository _typeReparationRepository;

        public TypeReparationService(ITypeReparationRepository typeReparationRepository)
        {
            _typeReparationRepository = typeReparationRepository;
        }

        public async Task<IEnumerable<TypeReparation>> GetAllTypeReparationsAsync()
        {
            return await _typeReparationRepository.GetAllAsync();
        }

        public async Task<TypeReparation> GetTypeReparationByIdAsync(int id)
        {
            return await _typeReparationRepository.GetByIdAsync(id);
        }

        public async Task AddTypeReparationAsync(TypeReparation typeReparation)
        {
            await _typeReparationRepository.AddAsync(typeReparation);
        }

        public async Task UpdateTypeReparationAsync(TypeReparation typeReparation)
        {
            _typeReparationRepository.Update(typeReparation);
            await _typeReparationRepository.SaveChangesAsync();
        }

        public async Task DeleteTypeReparationAsync(int id)
        {
            var typeReparation = await _typeReparationRepository.GetByIdAsync(id);
            if (typeReparation != null)
            {
                _typeReparationRepository.Delete(typeReparation);
                await _typeReparationRepository.SaveChangesAsync();
            }
        }
    }
}
