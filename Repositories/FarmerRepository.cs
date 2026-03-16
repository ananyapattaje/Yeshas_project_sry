// using CAPGEMINI_CROPDEAL.Data;
// using CAPGEMINI_CROPDEAL.Interfaces;
// using CAPGEMINI_CROPDEAL.Models;
// namespace CAPGEMINI_CROPDEAL.Repositories;

// public class FarmerRepository : IUpdateRepository<Farmer>
// {
//     private readonly CropDealDbContext _context;
//     public FarmerRepository(CropDealDbContext context)
//     {
//         _context = context;
//     }
//     public async Task<Farmer?> GetByIdAsync(int id)
//     {
//         return await _context.Farmers.FindAsync(id);
//     }

//     public async Task UpdateAsync(Farmer farmer)
//     {
//         _context.Farmers.Update(farmer);
//         await _context.SaveChangesAsync();

//     }

// }