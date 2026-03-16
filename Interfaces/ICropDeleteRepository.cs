using CAPGEMINI_CROPDEAL.Models;
namespace CAPGEMINI_CROPDEAL.Interfaces
{
    public interface ICropDeleteRepository
    {
        Task<Crop?> GetById(int cropId);
        Task Delete(Crop crop);
    }
}