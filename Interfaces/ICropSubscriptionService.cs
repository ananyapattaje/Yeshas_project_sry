using System.Threading.Tasks;

public interface ICropSubscriptionService
{
    Task Subscribe(int buyerId, string cropName);
}