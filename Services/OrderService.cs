using CAPGEMINI_CROPDEAL.Data;
using CAPGEMINI_CROPDEAL.DTO;
using Microsoft.EntityFrameworkCore;

public class OrderService
{
    private readonly CropDealDbContext _context;

    public OrderService(CropDealDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrder(int buyerId, CreateOrderDto dto)
    {
        var crop = await _context.Crops
            .Include(c => c.Farmer)
            .FirstOrDefaultAsync(c => c.CropId == dto.CropId);

        if (crop == null)
            throw new Exception("Crop not found");

        var totalPrice = crop.CropPrice * dto.Quantity;

        if(dto.Quantity > crop.Quantity)throw new Exception("Not enough crop quantity available");
        
        var order = new Order
        {
            BuyerId = buyerId,
            FarmerId = crop.FarmerId,
            CropId = crop.CropId,
            Quantity = dto.Quantity,
            TotalPrice = totalPrice
        };

        _context.Orders.Add(order);

        await _context.SaveChangesAsync();

        return order;
    }
}