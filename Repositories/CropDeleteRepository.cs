using CAPGEMINI_CROPDEAL.Data;
using CAPGEMINI_CROPDEAL.Interfaces;
using CAPGEMINI_CROPDEAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPGEMINI_CROPDEAL.repositories;

public class CropDeleteRepository : ICropDeleteRepository
{
    private readonly CropDealDbContext _context;

    public CropDeleteRepository(CropDealDbContext context)
    {
        _context = context;
    }

    public async Task<Crop?> GetById(int cropId)
    {
        return await _context.Crops.FindAsync(cropId);
    }

    public async Task Delete(Crop crop)
    {
        _context.Crops.Remove(crop);
        await _context.SaveChangesAsync();
    }
}
