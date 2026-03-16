using System.ComponentModel.DataAnnotations;

namespace CAPGEMINI_CROPDEAL.Models;

public class Farmer
{
    public int FarmerId { get; set; }

    public string FarmerName { get; set; } = string.Empty;

    [MaxLength(10)]
    public string PhoneNo { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string FarmerGmail { get; set; } = string.Empty;

    // Identity user reference
    public string? UserId { get; set; } 

    public ApplicationUser? User { get; set; }

    public List<Crop> Crops { get; set; } = new();
    public bool IsActive { get; set; } = true;
}