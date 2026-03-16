using System.ComponentModel.DataAnnotations;

namespace CAPGEMINI_CROPDEAL.DTO;

public class BuyerDTO

{    
    public string BuyerName { get; set; } = string.Empty;  
    [MaxLength(10)]
    public string PhoneNo { get; set; } = string.Empty;
    public string BuyerGmail { get; set; } = string.Empty;
}