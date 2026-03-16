using Microsoft.AspNetCore.Identity;

namespace CAPGEMINI_CROPDEAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}