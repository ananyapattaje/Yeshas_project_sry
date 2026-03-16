namespace CAPGEMINI_CROPDEAL.DTO;

public class CropDTO
{
    public string CropName { get; set; } = string.Empty;
    public string CropType { get; set; } = string.Empty;
    public decimal CropPrice { get; set; }
    public int Quantity{get; set;}

}