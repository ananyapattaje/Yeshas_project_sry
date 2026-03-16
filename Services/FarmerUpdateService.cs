// using CAPGEMINI_CROPDEAL.DTO;
// using CAPGEMINI_CROPDEAL.Interfaces;
// using CAPGEMINI_CROPDEAL.Models;
// using Microsoft.AspNetCore.Http.HttpResults;

// namespace CAPGEMINI_CROPDEAL.Services;

// public class FarmerUpdateService : IUpdateService<Farmer,FarmerDTO>
// {
//     private readonly IUpdateRepository _repo;
//     public FarmerUpdateService(IUpdateRepository repo)
//     {
//         _repo = repo;
//     }
//     public async Task<Farmer?> UpdateFarmerServiceAsync(int id, FarmerDTO dto)
//     {
//         var farmer = await _repo.GetFarmerByIdAsync(id);
//         if(farmer == null) return null;

//         farmer.FarmerName = dto.FarmerName;
//         farmer.PhoneNo = dto.PhoneNo;
//         farmer.Location = dto.Location;
//         farmer.FarmerGmail = dto.FarmerGmail;

//         await _repo.UpdateFarmerAsync(farmer);
//         return farmer;
//     }
// }