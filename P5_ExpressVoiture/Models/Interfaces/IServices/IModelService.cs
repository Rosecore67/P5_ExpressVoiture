using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IServices
{
    public interface IModeleService
    {
        Task<IEnumerable<Modele>> GetAllModelesAsync();
        Task<Modele> GetModeleByIdAsync(int id);
        Task<Modele> GetMarqueByIdIncludingSoftDeletedAsync(int id);
        Task AddModeleAsync(Modele modele);
        Task UpdateModeleAsync(Modele modele);
        Task DeleteModeleAsync(int id);
    }
}
