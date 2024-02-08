using StudentCRUD.DTOs.Shared;
using StudentCRUD.DTOs.UserAccount;

namespace StudentCRUD.Services.UserAccount
{
    public interface IUserService
    {
        public Task<ServiceResponse<GetUserRegisterDto>> CreateAccountAsync(AddUserRegisterDto registerDTO);

        public Task<ServiceResponse<GetUserLoginDto>> LoginAccountAsync(UserLoginDto loginDTO);
    }
}
