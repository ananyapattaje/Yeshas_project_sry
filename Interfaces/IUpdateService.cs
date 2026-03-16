using CAPGEMINI_CROPDEAL.Models;
using CAPGEMINI_CROPDEAL.DTO;
namespace CAPGEMINI_CROPDEAL.Interfaces;

public interface IUpdateService<T,TDto> where T:class
{
    Task<T?> UpdateServiceAsync(int id,TDto dto);
}