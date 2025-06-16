using UMS.Models;

namespace UMS.Repositories
{
    public interface IAccountRepository
    {
        LoginResponseModel Login(LoginRequestModel requestModel);
        LoginResponseModel RefreshToken(string token, string refreshToken);
        bool Logout(string token);
        bool UserRegister(UserRegisterModel requestModel);
        bool ManagerRegister(ManagerRegisterModel requestModel);
    }
}
