using CAPGEMINI_CROPDEAL.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CAPGEMINI_CROPDEAL.Interfaces;

public interface IManipulateCropRepository
{
    public Task Add(Crop crop);
    public Task<Crop?> GetById(int id);
    public Task Update(Crop crop);

    public Task Delete(Crop crop);

}