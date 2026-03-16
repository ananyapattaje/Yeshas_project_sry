

namespace CAPGEMINI_CROPDEAL.Models;
public class Crop
{
    public int CropId { get; set; }
    public string CropName { get; set; } = string.Empty;
    public string CropType { get; set; } = string.Empty;
    public decimal CropPrice { get; set; }
    public int Quantity{get; set;}
    public int FarmerId { get; set; }
    public bool IsAvailable { get; set; } = true;
    
    //navigatory reference 
    public Farmer? Farmer { get; set; } 
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}