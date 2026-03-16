using CAPGEMINI_CROPDEAL.DTO;

namespace CAPGEMINI_CROPDEAL.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDTO dto);

        Task<string> Login(LoginDTO dto);
    }
}