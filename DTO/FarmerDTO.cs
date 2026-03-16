namespace CAPGEMINI_CROPDEAL.DTO;
using System.ComponentModel.DataAnnotations;

public class FarmerDTO
{    
    public string FarmerName { get; set; } = string.Empty;  
    public string PhoneNo { get; set; } = string.Empty;
    [MaxLength(10)]
    public string Location { get; set; } = string.Empty;
    public string FarmerGmail { get; set; } = string.Empty;
}