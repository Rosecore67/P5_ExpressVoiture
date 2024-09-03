using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;
using P5_ExpressVoiture.Models.Interfaces.IServices;

namespace P5_ExpressVoiture.Models.Services
{
    public class VoitureService : IVoitureService
    {
        private readonly IVoitureRepository _voitureRepository;

        public VoitureService(IVoitureRepository voitureRepository)
        {
            _voitureRepository = voitureRepository;
        }

        public async Task<IEnumerable<Voiture>> GetAllVoituresAsync()
        {
            return await _voitureRepository.GetAllAsync();
        }

        public async Task<Voiture> GetVoitureByIdAsync(int id)
        {
            return await _voitureRepository.GetByIdAsync(id);
        }

        public async Task<Voiture> GetVoitureByVinAsync(string vin)
        {
            return await _voitureRepository.GetByVinAsync(vin);
        }

        public async Task AddVoitureAsync(Voiture voiture)
        {
            await _voitureRepository.AddAsync(voiture);
        }

        public async Task UpdateVoitureAsync(Voiture voiture)
        {
            _voitureRepository.Update(voiture);
            await _voitureRepository.SaveChangesAsync();
        }

        public async Task DeleteVoitureAsync(int id)
        {
            var voiture = await _voitureRepository.GetByIdAsync(id);
            if (voiture != null)
            {
                _voitureRepository.Delete(voiture);
                await _voitureRepository.SaveChangesAsync();
            }
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // Générer un nom de fichier unique en ajoutant un GUID avant le nom original
                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";

                // Définir le chemin d'upload
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var imagePath = Path.Combine(uploads, uniqueFileName);

                // Créer le répertoire si nécessaire
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                // Enregistrer l'image dans le répertoire spécifié
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Retourner le chemin relatif de l'image pour être utilisé dans l'application
                return "images/" + uniqueFileName;
            }

            return null;
        }

        public void DeleteImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
        }
    }
}