using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WiseThing.Data.Respository;

namespace WiseThing.Portal.Business
{
    public interface IUserHandler
    {
        Task AddEditUser(UserDTO user);
        Task<UserDTO> GetUserByLogin(string email, string passWord);
        Task<bool> IsUserNameAlreadyExsist(string userName);
        Task<UserDTO> GetUserById(int userId);
        Task ResetPassword(ResetPasswordDTO resetpassword);
    }
}
