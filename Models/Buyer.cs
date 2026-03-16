using System.ComponentModel.DataAnnotations;

namespace CAPGEMINI_CROPDEAL.Models;

public class Buyer
{
    public int BuyerId { get; set; }

    public string BuyerName { get; set; } = string.Empty;

    public string BuyerGmail { get; set; } = string.Empty;

    [MaxLength(10)]
    public string PhoneNo { get; set; } = string.Empty;

    // Identity user reference
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public ICollection<CropSubscription>? Subscriptions { get; set; }
}