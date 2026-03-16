using Microsoft.EntityFrameworkCore;
using CAPGEMINI_CROPDEAL.Interfaces;
using CAPGEMINI_CROPDEAL.Data;
public class GenericUpdateRepository<TEntity> : IUpdateRepository<TEntity> where TEntity : class
{
    private readonly CropDealDbContext _context;
    private readonly DbSet<TEntity> _table;

    public GenericUpdateRepository(CropDealDbContext context)
    {
        _context = context;
        _table = context.Set<TEntity>(); 
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _table.FindAsync(id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _table.Update(entity);
        await _context.SaveChangesAsync();
    }
}