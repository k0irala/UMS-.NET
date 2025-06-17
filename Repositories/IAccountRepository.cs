using System.Net;
using UMS.Models;

namespace UMS.Repositories
{
    public interface IAccountRepository
    {
        LoginResponseModel Login(LoginRequestModel requestModel);
        LoginResponseModel RefreshToken(string token, string refreshToken);
        bool Logout(string token);
        HttpStatusCode UserRegister(UserRegisterModel requestModel);
        HttpStatusCode ManagerRegister(ManagerRegisterModel requestModel);

    }
}
