namespace CAPGEMINI_CROPDEAL.Events;
public class CropPublishedEvent
{
    public int CropId { get; set; }
    public string? CropName { get; set; }
    public int FarmerId { get; set; }
    public decimal Price { get; set; }
}