using System.Threading.Tasks;

public interface IPaymentService
{
    Task<string> PayForOrder(int orderId);
}