// using CAPGEMINI_CROPDEAL.Data;
// using CAPGEMINI_CROPDEAL.Interfaces;
// using CAPGEMINI_CROPDEAL.Models;
// namespace CAPGEMINI_CROPDEAL.Repositories;

// public class BuyerRepository : IUpdateRepository<Buyer>
// {
//     private readonly CropDealDbContext _context;
//     public BuyerRepository(CropDealDbContext context)
//     {
//         _context = context;
//     }
//     public async Task<Buyer?> GetByIdAsync(int id)
//     {
//         return await _context.Buyers.FindAsync(id);
//     }

//     public async Task UpdateAsync(Buyer buyer)
//     {
//         _context.Buyers.Update(buyer);
//         await _context.SaveChangesAsync();

//     }
// }