using CAPGEMINI_CROPDEAL.DTO;
using CAPGEMINI_CROPDEAL.Models;

namespace CAPGEMINI_CROPDEAL.Interfaces;

public interface IUpdateCropService
{
    public Task<Crop?> Update(int farmerId,int id,CropDTO dto);
}