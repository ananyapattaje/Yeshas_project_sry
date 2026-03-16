using CAPGEMINI_CROPDEAL.Models;

namespace CAPGEMINI_CROPDEAL.Models;
public class CropSubscription

{
    public int Id { get; set; }

    public int BuyerId { get; set; }
    public Buyer? Buyer { get; set; }

    public string CropName { get; set; } = string.Empty;
}