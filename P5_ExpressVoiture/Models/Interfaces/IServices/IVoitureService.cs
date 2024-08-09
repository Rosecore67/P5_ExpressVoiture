using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IServices
{
    public interface IVoitureService
    {
        Task<IEnumerable<Voiture>> GetAllVoituresAsync();
        Task<Voiture> GetVoitureByIdAsync(int id);
        Task<Voiture> GetVoitureByVinAsync(string vin);
        Task AddVoitureAsync(Voiture voiture);
        Task UpdateVoitureAsync(Voiture voiture);
        Task DeleteVoitureAsync(int id);
    }
}
