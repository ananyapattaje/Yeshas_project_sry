using CAPGEMINI_CROPDEAL.Data;
using CAPGEMINI_CROPDEAL.Models;
using Microsoft.EntityFrameworkCore;

public class PaymentService : IPaymentService
{
    private readonly CropDealDbContext _context;

    public PaymentService(CropDealDbContext context)
    {
        _context = context;
    }

    public async Task<string> PayForOrder(int orderId)
    {
        var order = await _context.Orders
            .Include(o => o.Buyer)
            .Include(o => o.Farmer)
            .Include(o => o.Crop)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            throw new Exception("Order not found");

        if (order.Status == "Paid")
            throw new Exception("Order already paid");

        var payment = new Payment
        {
            OrderId = order.Id,
            BuyerId = order.BuyerId,
            FarmerId = order.FarmerId,
            Amount = order.TotalPrice,
            PaymentStatus = "Success",
            TransactionReference = Guid.NewGuid().ToString()
        };

        _context.Payments.Add(payment);

        order.Status = "Paid";

        var invoice = new Invoice
        {
            OrderId = order.Id,
            FarmerName = order.Farmer!.FarmerName,
            BuyerName = order.Buyer!.BuyerName,
            CropName = order.Crop!.CropName,
            Quantity = order.Quantity,
            PricePerUnit = order.Crop.CropPrice,
            TotalAmount = order.TotalPrice
        };

        order.Crop.Quantity -= order.Quantity;

        if (order.Crop.Quantity <= 0)
        {
            order.Crop.IsAvailable = false;
        }

        _context.Invoices.Add(invoice);

        await _context.SaveChangesAsync();

        return "Payment Successful. Invoice generated.";
    }
}