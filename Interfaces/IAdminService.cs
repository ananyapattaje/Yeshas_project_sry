using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAdminService
{
    Task ActivateFarmer(int farmerId);
    Task DeactivateFarmer(int farmerId);

    Task<List<FarmerPerformanceDto>> GetFarmerPerformance();
    byte[] GenerateExcel(List<FarmerPerformanceDto> data);
}