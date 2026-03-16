using CAPGEMINI_CROPDEAL.Data;
using CAPGEMINI_CROPDEAL.Interfaces;
using CAPGEMINI_CROPDEAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPGEMINI_CROPDEAL.repositories;

public class ManipulateCropRepository : IManipulateCropRepository
{
    private readonly CropDealDbContext _context;
    public ManipulateCropRepository(CropDealDbContext context)
    {
        _context = context;
    }
    public async Task Add(Crop crop)
    {
        await _context.Crops.AddAsync(crop);
        await _context.SaveChangesAsync();
    }
    public async Task<Crop?> GetById(int id)
    {
       return await _context.Crops.FindAsync(id);
     
    }
    public async Task Update(Crop crop)
    {
        _context.Crops.Update(crop);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Crop crop)
    {
        _context.Crops.Remove(crop);
        await _context.SaveChangesAsync();
    }


}