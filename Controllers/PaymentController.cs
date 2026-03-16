using System.Security.Claims;
using CAPGEMINI_CROPDEAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private readonly PaymentService _paymentService;
    private readonly CropDealDbContext _context;

    public PaymentController(PaymentService paymentService,CropDealDbContext context)
    {
        _paymentService = paymentService;
        _context = context;
    }

    [Authorize(Roles = "Buyer")]
    [HttpPost("{orderId}")]
    public async Task<IActionResult> Pay(int orderId)
    {
        var result = await _paymentService.PayForOrder(orderId);

        return Ok(result);
    }

    [Authorize(Roles = "Farmer")]
    [HttpGet("farmer-receipts")]
    public async Task<IActionResult> GetFarmerReceipts()
    {
          

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var farmer = await _context.Farmers
            .FirstOrDefaultAsync(f => f.UserId == userId);

        if (farmer == null)return NotFound("Farmer not found");
        
        var receipts = await _context.Invoices
            .Where(i => i.Order!.FarmerId == farmer.FarmerId)
            .Select(i => new
            {
                i.BuyerName,
                i.CropName,
                i.Quantity,
                i.PricePerUnit,
                i.TotalAmount,
                i.GeneratedAt
            })
            .ToListAsync();

        return Ok(receipts);
    }
}