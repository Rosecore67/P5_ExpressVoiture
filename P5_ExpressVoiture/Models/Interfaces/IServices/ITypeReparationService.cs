using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IServices
{
    public interface ITypeReparationService
    {
        Task<IEnumerable<TypeReparation>> GetAllTypeReparationsAsync();
        Task<TypeReparation> GetTypeReparationByIdAsync(int id);
        Task<TypeReparation> GetMarqueByIdIncludingSoftDeletedAsync(int id);
        Task AddTypeReparationAsync(TypeReparation typeReparation);
        Task UpdateTypeReparationAsync(TypeReparation typeReparation);
        Task DeleteTypeReparationAsync(int id);
    }
}
