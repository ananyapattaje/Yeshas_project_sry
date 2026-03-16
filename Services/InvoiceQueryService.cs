using CAPGEMINI_CROPDEAL.Data;
using Microsoft.EntityFrameworkCore;

public class InvoiceQueryService
{
    private readonly CropDealDbContext _context;

    public InvoiceQueryService(CropDealDbContext context)
    {
        _context = context;
    }

    public async Task<object?> GetInvoice(int orderId)
    {
        return await _context.Invoices
            .Where(i => i.OrderId == orderId)
            .Select(i => new
            {
                i.FarmerName,
                i.BuyerName,
                i.CropName,
                i.Quantity,
                i.PricePerUnit,
                i.TotalAmount,
                i.GeneratedAt
            })
            .FirstOrDefaultAsync();
    }
}