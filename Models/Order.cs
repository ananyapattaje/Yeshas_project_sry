using CAPGEMINI_CROPDEAL.Models;

public class Order
{
    public int Id { get; set; }

    public int BuyerId { get; set; }
    public Buyer? Buyer { get; set; }

    public int FarmerId { get; set; }
    public Farmer? Farmer { get; set; }

    public int CropId { get; set; }
    public Crop? Crop { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}