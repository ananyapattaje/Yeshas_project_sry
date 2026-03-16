using CAPGEMINI_CROPDEAL.DTO;

using CAPGEMINI_CROPDEAL.Models;

namespace CAPGEMINI_CROPDEAL.Interfaces;
public interface IAddCropService
{
    public Task Add(int farmerId, CropDTO dto);
    
}