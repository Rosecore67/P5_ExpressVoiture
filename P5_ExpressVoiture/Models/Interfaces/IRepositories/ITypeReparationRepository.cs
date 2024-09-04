using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IRepositories
{
    public interface ITypeReparationRepository : IRepository<TypeReparation>
    {
        Task<TypeReparation> GetByIdIncludingSoftDeletedAsync(int id);
    }
}
