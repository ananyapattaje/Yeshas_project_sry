using CAPGEMINI_CROPDEAL.Models;
using CAPGEMINI_CROPDEAL.DTO;

public static class BuyerMapper
{
    public static Buyer Map(Buyer buyer, BuyerDTO dto)
    {
        buyer.BuyerName = dto.BuyerName;
        buyer.PhoneNo = dto.PhoneNo;
        buyer.BuyerGmail = dto.BuyerGmail;

        return buyer;
    }
}