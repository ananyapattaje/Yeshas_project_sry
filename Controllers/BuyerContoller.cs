using CAPGEMINI_CROPDEAL.DTO;
using CAPGEMINI_CROPDEAL.Interfaces;
using CAPGEMINI_CROPDEAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CAPGEMINI_CROPDEAL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuyerController : ControllerBase
{
    private readonly IUpdateService<Buyer, BuyerDTO> _updateService;
    private readonly ICropSubscriptionService _service;

    public BuyerController(
        IUpdateService<Buyer, BuyerDTO> updateService,
        ICropSubscriptionService service)
    {
        _updateService = updateService;
        _service = service;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBuyer(int id, [FromBody] BuyerDTO dto)
    {
        var updatedBuyer = await _updateService.UpdateServiceAsync(id, dto);

        if (updatedBuyer == null)
            return NotFound("Buyer not found");

        return Ok(updatedBuyer);
    }

    [HttpPost("{buyerId}/crop-subscription")]
    public async Task<IActionResult> Subscribe(int buyerId, string cropName)
    {
        await _service.Subscribe(buyerId, cropName);
        return Ok("Buyer subscribed to crop");
    }
}