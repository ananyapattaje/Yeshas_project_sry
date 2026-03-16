using CAPGEMINI_CROPDEAL.Models;

namespace CAPGEMINI_CROPDEAL.Interfaces;
public interface IUpdateRepository<T> where T: class
{
    Task<T?> GetByIdAsync(int id);
    Task UpdateAsync(T user);
}