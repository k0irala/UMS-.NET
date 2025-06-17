using Dapper;
using FluentValidation;
using FluentValidation.Results;
using System.Net;
using UMS.Enums;
using UMS.Models;

namespace UMS.Repositories
{
    public class AccountRepository(IValidator<ManagerRegisterModel> managerValidator,IDapperRepository repository): IAccountRepository
    {
        public LoginResponseModel Login(LoginRequestModel requestModel)
        {
            return new LoginResponseModel
            {
                UserName = requestModel.UserName,
            };
        }

        public bool Logout(string token)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode ManagerRegister(ManagerRegisterModel requestModel)
        { 
            ValidationResult validationResult = managerValidator.Validate(requestModel);
            if (!validationResult.IsValid) { return HttpStatusCode.BadRequest; }

            DynamicParameters parameters = new();
            parameters.Add("@FullName", requestModel.FullName);
            parameters.Add("@UserName", requestModel.UserName);
            parameters.Add("@Password", requestModel.Password);
            parameters.Add("@Email", ConstantValues.MANAGER_DEFAULT_EMAIL);
            parameters.Add("@DesignationId", requestModel.DesignationId);
            parameters.Add("@RoleId", Roles.Manager);
            parameters.Add("@Result", dbType:System.Data.DbType.Int32,direction:System.Data.ParameterDirection.Output);

            repository.Execute(StoredProcedures.MANAGER_REGISTER, parameters);

            int result = parameters.Get<int>("@Result");

            if (result == 1) return HttpStatusCode.OK;
            if (result == -1) return HttpStatusCode.Conflict;
            else throw new Exception("An error occurred while adding the designation.");
        }

        public LoginResponseModel RefreshToken(string token, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode UserRegister(UserRegisterModel requestModel)
        {
            DynamicParameters parameters = new();
            parameters.Add("@FullName", requestModel.FullName);
            parameters.Add("@UserName", requestModel.UserName);
            parameters.Add("@Code", requestModel.Code);
            parameters.Add("@Password", requestModel.Password);
            parameters.Add("@Email", requestModel.Email);
            parameters.Add("@DesignationId", requestModel.DesignationId);
            //parameters.Add("@ManagerId", requestModel.ManagerId);
            //return true;
            return HttpStatusCode.OK;

        }
    }
}
