using CAPGEMINI_CROPDEAL.Interfaces;
using CAPGEMINI_CROPDEAL.Models;
using CAPGEMINI_CROPDEAL.DTO;

namespace CAPGEMINI_CROPDEAL.Services;

public class UpdateCropService : IUpdateCropService
{
    private readonly IManipulateCropRepository _repo;

    public UpdateCropService(IManipulateCropRepository repo)
    {
        _repo = repo;
    }

    public async Task<Crop?> Update(int farmerId, int id, CropDTO dto)
    {
        Crop? crop = await _repo.GetById(id);

        if (crop == null)
            return null;

        // SECURITY CHECK
        if (crop.FarmerId != farmerId)
            throw new Exception("You are not allowed to update this crop");

        crop.CropName = dto.CropName;
        crop.CropPrice = dto.CropPrice;
        crop.CropType = dto.CropType;
        crop.Quantity = dto.Quantity;

        await _repo.Update(crop);

        return crop;
    }
}