using CAPGEMINI_CROPDEAL.Interfaces;
using CAPGEMINI_CROPDEAL.Models;

namespace CAPGEMINI_CROPDEAL.Services
{
    public class CropDeleteService : ICropDeleteService
    {
        private readonly ICropDeleteRepository _repo;

        public CropDeleteService(ICropDeleteRepository repo)
        {
            _repo = repo;
        }

        public async Task Delete(int cropId)
        {
            var crop = await _repo.GetById(cropId);

            if (crop == null)
                throw new Exception("Crop not found");

            await _repo.Delete(crop);
        }
    }
}