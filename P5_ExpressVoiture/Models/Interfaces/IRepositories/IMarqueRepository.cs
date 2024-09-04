using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IRepositories
{
    public interface IMarqueRepository : IRepository<Marque>
    {
       Task<Marque> GetByIdIncludingSoftDeletedAsync(int id);
    }
}
