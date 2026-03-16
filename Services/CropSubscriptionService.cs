using CAPGEMINI_CROPDEAL.Data;
using CAPGEMINI_CROPDEAL.Models;

public class CropSubscriptionService : ICropSubscriptionService
{
    private readonly CropDealDbContext _context;

    public CropSubscriptionService(CropDealDbContext context)
    {
        _context = context;
    }

    public async Task Subscribe(int buyerId, string cropName)
    {
        var subscription = new CropSubscription
        {
            BuyerId = buyerId,
            CropName = cropName
        };

        _context.CropSubscriptions.Add(subscription);

        await _context.SaveChangesAsync();
    }
}