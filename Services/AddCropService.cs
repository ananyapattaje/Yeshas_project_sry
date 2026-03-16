using CAPGEMINI_CROPDEAL.Data;
using CAPGEMINI_CROPDEAL.DTO;
using CAPGEMINI_CROPDEAL.Events;
using CAPGEMINI_CROPDEAL.Interfaces;
using CAPGEMINI_CROPDEAL.Models;

namespace CAPGEMINI_CROPDEAL.Services;

public class AddCropService: IAddCropService
{
    private readonly IManipulateCropRepository _repo;
    private readonly CropEventPublisher _publisher;
    public AddCropService(IManipulateCropRepository repo, CropEventPublisher publisher)
    {
        _repo = repo;
        _publisher = publisher;
    }

    // TRY TO GET THE FARMER ID FROM THE LOGIN TOKEN!!!!!!! <<<<--------------------------------------------------
    public async Task Add(int farmerId, CropDTO dto)
    {
        
        Crop crop = new Crop();
        crop.CropName = dto.CropName;
        crop.CropPrice = dto.CropPrice;
        crop.CropType = dto.CropType;
        crop.Quantity = dto.Quantity;
        crop.FarmerId = farmerId;
        
        await _repo.Add(crop);

        var cropEvent = new CropPublishedEvent
        {
            CropId = crop.CropId,
            CropName = crop.CropName,
            FarmerId = crop.FarmerId,
            Price = crop.CropPrice
        };

        _publisher.PublishCrop(cropEvent);

        
    }

    
}