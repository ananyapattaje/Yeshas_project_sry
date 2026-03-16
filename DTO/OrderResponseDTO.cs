public class OrderResponseDto
{
    public int OrderId { get; set; }
    public int CropId { get; set; }
    public int Quantity { get; set; }
    public int FarmerId { get; set; }
    public decimal TotalPrice { get; set; }
}