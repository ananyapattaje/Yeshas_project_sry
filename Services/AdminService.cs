using CAPGEMINI_CROPDEAL.Data;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
public class AdminService
{
    private readonly CropDealDbContext _context;

    public AdminService(CropDealDbContext context)
    {
        _context = context;
    }

    public async Task ActivateFarmer(int farmerId)
    {
        var farmer = await _context.Farmers.FindAsync(farmerId);

        if (farmer == null)
            throw new Exception("Farmer not found");

        farmer.IsActive = true;

        await _context.SaveChangesAsync();
    }

    public async Task DeactivateFarmer(int farmerId)
    {
        var farmer = await _context.Farmers.FindAsync(farmerId);

        if (farmer == null)
            throw new Exception("Farmer not found");

        farmer.IsActive = false;

        await _context.SaveChangesAsync();
    }
    public async Task<List<FarmerPerformanceDto>> GetFarmerPerformance()
    {      
        
        var result = await _context.Farmers
            .Select(f => new FarmerPerformanceDto
            {
                FarmerName = f.FarmerName,
                TotalCrops = f.Crops.Count(),
                TotalOrders = _context.Orders.Count(o => o.FarmerId == f.FarmerId)
            })
            .ToListAsync();

        return result;
    }
    public byte[] GenerateExcel(List<FarmerPerformanceDto> data)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Farmer Performance");

        worksheet.Cell(1, 1).Value = "Farmer Name";
        worksheet.Cell(1, 2).Value = "Total Crops";
        worksheet.Cell(1, 3).Value = "Total Orders";

        int row = 2;

        foreach (var farmer in data)
        {
            worksheet.Cell(row, 1).Value = farmer.FarmerName;
            worksheet.Cell(row, 2).Value = farmer.TotalCrops;
            worksheet.Cell(row, 3).Value = farmer.TotalOrders;
            row++;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        return stream.ToArray();
    }
    
}