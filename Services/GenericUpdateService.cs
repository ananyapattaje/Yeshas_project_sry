using System.Threading.Tasks;
using CAPGEMINI_CROPDEAL.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CAPGEMINI_CROPDEAL.Services;

public class GenericUpdateService<TEntity,TDto> : IUpdateService<TEntity,TDto> where TEntity:class
{   
    private readonly IUpdateRepository<TEntity> _repo ;

    // IDK why we are using Func<> delegate here ? there should be some reason.
     private readonly Func<TEntity, TDto, TEntity> _mapper;
    public GenericUpdateService(IUpdateRepository<TEntity> repo, Func<TEntity, TDto, TEntity> mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    
    public async Task<TEntity?> UpdateServiceAsync(int id , TDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);

        if (entity == null)
            return null;

        entity = _mapper(entity, dto);

        await _repo.UpdateAsync(entity);

        return entity;

        

    }
}