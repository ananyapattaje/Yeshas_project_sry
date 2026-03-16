using System.Security.Claims;
using CAPGEMINI_CROPDEAL.Data;
using CAPGEMINI_CROPDEAL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly CropDealDbContext _context;

    public OrderController(OrderService orderService,CropDealDbContext context)
    {
        _orderService = orderService;
        _context = context;
    }

    [Authorize(Roles = "Buyer")]
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var buyer = await _context.Buyers
            .FirstOrDefaultAsync(b => b.UserId == userId);

        if (buyer == null)
            return BadRequest("Buyer not found");

        var order = await _orderService.CreateOrder(buyer.BuyerId, dto);

        var response = new OrderResponseDto
        {
            OrderId = order.Id,
            CropId = order.CropId,
            Quantity = order.Quantity,
            TotalPrice = order.TotalPrice,
            FarmerId = order.FarmerId
        };

        return Ok(response);
    }
}