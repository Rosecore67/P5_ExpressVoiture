using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IRepositories
{
    public interface IModeleRepository : IRepository<Modele>
    {
        Task<Modele> GetByIdIncludingSoftDeletedAsync(int id);
    }
}
