using CAPGEMINI_CROPDEAL.Models;
using CAPGEMINI_CROPDEAL.DTO;

public static class FarmerMapper
{
    public static Farmer Map(Farmer farmer, FarmerDTO dto)
    {
        farmer.FarmerName = dto.FarmerName;
        farmer.PhoneNo = dto.PhoneNo;
        farmer.Location = dto.Location;
        farmer.FarmerGmail = dto.FarmerGmail;

        return farmer;
    }
}