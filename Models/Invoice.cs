namespace CAPGEMINI_CROPDEAL.Models;
public class Invoice
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public string FarmerName { get; set; } = string.Empty;

    public string BuyerName { get; set; } = string.Empty;

    public string CropName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal PricePerUnit { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}