using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IRepositories
{
    public interface IReparationRepository : IRepository<Reparation>
    {
        Task<decimal> GetTotalCoutReparationsByVoitureId(int voitureId);
    }
}
