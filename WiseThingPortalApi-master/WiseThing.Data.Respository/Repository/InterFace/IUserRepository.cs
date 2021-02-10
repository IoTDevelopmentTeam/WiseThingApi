using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WiseThing.Data.Respository
{
    public interface IUserRepository
    {
        Task AddNewUser(UserDTO userDto);
        Task EditUser(UserDTO userDto);
        Task<UserDTO> GetUserById(int userId);
        Task<UserDTO >GetUserByLoginDetails(string userName, string passWord);
        Task<bool> IsUserExists(string userName);

    }
}
