using CAPGEMINI_CROPDEAL.Data;
using CAPGEMINI_CROPDEAL.DTO;
using CAPGEMINI_CROPDEAL.Interfaces;
using CAPGEMINI_CROPDEAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CAPGEMINI_CROPDEAL.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Farmer")]
public class FarmerController : ControllerBase
{
    private readonly IUpdateService<Farmer, FarmerDTO> _updateService;
    private readonly IAddCropService _addcropservice;
    private readonly IUpdateCropService _updatecropservice;
    private readonly ICropDeleteService _deleteService;
    private readonly CropDealDbContext _context;

    public FarmerController(
        IUpdateService<Farmer, FarmerDTO> updateService,
        IAddCropService addcropservice,
        IUpdateCropService updatecropservice,
        ICropDeleteService deleteService,
        CropDealDbContext context)
    {
        _updateService = updateService;
        _addcropservice = addcropservice;
        _updatecropservice = updatecropservice;
        _deleteService = deleteService;
        _context = context;
    }
    [HttpGet("ping")]
    [AllowAnonymous]
    public IActionResult Ping() => Ok("Controller is reachable");

    [HttpPut("updateprofile")]
    public async Task<IActionResult> UpdateFarmer([FromBody] FarmerDTO dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var farmer = await _context.Farmers
            .FirstOrDefaultAsync(f => f.UserId == userId);

        if (farmer == null)
            return NotFound("Farmer not found");

        var updatedFarmer = await _updateService.UpdateServiceAsync(farmer.FarmerId, dto);

        return Ok(updatedFarmer);
    }
    [HttpPost("addcrop")]
    public async Task<IActionResult> AddCrop([FromBody] CropDTO dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var allClaims = User.Claims.Select(c => new { c.Type, c.Value });
        Console.WriteLine($"UserId from token: {userId}");
        Console.WriteLine($"All claims: {System.Text.Json.JsonSerializer.Serialize(allClaims)}");
        
        var farmerCount = await _context.Farmers.CountAsync();
        Console.WriteLine($"Total farmers in DB: {farmerCount}");
        if (userId == null)return Unauthorized("User not logged in");
        var farmer = await _context.Farmers
            .FirstOrDefaultAsync(f => f.UserId == userId);

        if (farmer == null)
            return BadRequest("Farmer not found");

        await _addcropservice.Add(farmer.FarmerId, dto);

        return Ok("The crop was successfully added.");
    }
    [HttpPut("updatecrop/{id}")]
    public async Task<IActionResult> UpdateCrop(int id, [FromBody] CropDTO dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var farmer = await _context.Farmers
            .FirstOrDefaultAsync(f => f.UserId == userId);

        if (farmer == null)
            return BadRequest("Farmer not found");

        Crop? crop = await _updatecropservice.Update(farmer.FarmerId, id, dto);

        if (crop == null)
            return NotFound("Could not find the crop.");

        return Ok(crop);
    }
    [HttpDelete("deletecrop/{cropId}")]
    public async Task<IActionResult> DeleteCrop(int cropId)
    {
        await _deleteService.Delete(cropId);

        return Ok("Crop deleted successfully");
    }
}














































// using CAPGEMINI_CROPDEAL.DTO;
// using CAPGEMINI_CROPDEAL.Interfaces;
// using CAPGEMINI_CROPDEAL.Models;
// using CAPGEMINI_CROPDEAL.Services;
// using Microsoft.AspNetCore.Mvc;

// namespace CAPGEMINI_CROPDEAL.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// public class FarmerController : ControllerBase
// {
//     private readonly IUpdateService<Farmer, FarmerDTO> _updateService;
//     private readonly IAddCropService _addcropservice;
//     private readonly IUpdateCropService _updatecropservice;

//     private readonly ICropDeleteService _deleteService;
//     public FarmerController(IUpdateService<Farmer, FarmerDTO> updateService , IAddCropService addcropservice,IUpdateCropService updatecropservice,ICropDeleteService deleteService)
//     {
//         _updateService = updateService;
//         _addcropservice = addcropservice; 
//         _updatecropservice = updatecropservice;
//         _deleteService = deleteService;
//     }

//     [HttpPut("{id}")]
//     public async Task<IActionResult> UpdateFarmer(int id, [FromBody] FarmerDTO dto)
//     {
//         var updatedFarmer = await _updateService.UpdateServiceAsync(id, dto);

//         if (updatedFarmer == null)
//             return NotFound("Farmer not found");

//         return Ok(updatedFarmer);
//     }

    

//     [HttpPost("{farmerId}/addcrop")]
//     public async Task<IActionResult> AddCrop(int farmerId, [FromBody] CropDTO dto)
//     {
//         await _addcropservice.Add(farmerId,dto);

//         return Ok("The crop was successfully added.");
//     }
    

//     [HttpPut("{farmerId}/{id}/updatecrop")]
//     public async Task<IActionResult> UpdateCrop(int farmerId,int id, [FromBody] CropDTO dto)
//     {
//         Crop? crop = await _updatecropservice.Update(farmerId, id, dto );
//         if(crop == null) return NotFound("Could not find the crop.");
//         return Ok(crop);
//     }



//     [HttpDelete("{cropId}")]
//     public async Task<IActionResult> DeleteCrop(int cropId)
//     {
//         await _deleteService.Delete(cropId);
//         return Ok("Crop deleted successfully");
//     }

// }   