using Dapper;
using UMS.Models;

namespace UMS.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public LoginResponseModel Login(LoginRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public bool Logout(string token)
        {
            throw new NotImplementedException();
        }

        public bool ManagerRegister(ManagerRegisterModel requestModel)
        { 
            throw new NotImplementedException();
        }

        public LoginResponseModel RefreshToken(string token, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public bool UserRegister(UserRegisterModel requestModel)
        {
            DynamicParameters parameters = new();
            parameters.Add("@FullName", requestModel.FullName);
            parameters.Add("@UserName", requestModel.UserName);
            parameters.Add("@Code", requestModel.Code);
            parameters.Add("@Password", requestModel.Password);
            parameters.Add("@Email", requestModel.Email);
            parameters.Add("@DesignationId", requestModel.DesignationId);
            //parameters.Add("@ManagerId", requestModel.ManagerId);
            return true;

        }
    }
}
