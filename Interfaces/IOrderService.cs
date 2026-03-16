using System.Threading.Tasks;
using CAPGEMINI_CROPDEAL.DTO;

public interface IOrderService
{
    Task<Order> CreateOrder(int buyerId, CreateOrderDto dto);
}