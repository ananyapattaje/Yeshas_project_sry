using CAPGEMINI_CROPDEAL.Models;

namespace CAPGEMINI_CROPDEAL.Interfaces
{
    public interface ICropDeleteService
    {
        public Task Delete(int cropId);
    }
}