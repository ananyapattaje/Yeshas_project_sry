namespace CAPGEMINI_CROPDEAL.Models;
public class Payment
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public int BuyerId { get; set; }

    public int FarmerId { get; set; }

    public decimal Amount { get; set; }

    public string? PaymentStatus { get; set; }

    public string? TransactionReference { get; set; }

    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
}