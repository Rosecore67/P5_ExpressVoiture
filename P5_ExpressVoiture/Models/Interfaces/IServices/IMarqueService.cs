using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IServices
{
    public interface IMarqueService
    {
        Task<IEnumerable<Marque>> GetAllMarquesAsync();
        Task<Marque> GetMarqueByIdAsync(int id);
        Task AddMarqueAsync(Marque marque);
        Task UpdateMarqueAsync(Marque marque);
        Task DeleteMarqueAsync(int id);
    }
}