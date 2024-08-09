using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IRepositories
{
    public interface IVoitureRepository : IRepository<Voiture>
    {
        Task<Voiture> GetByVinAsync(string vin);
    }
}
