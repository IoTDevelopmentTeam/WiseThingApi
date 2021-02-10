using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WiseThing.Data.Respository;

namespace WiseThing.Portal.Business
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserRepository _userRepo;
        public UserHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task AddEditUser(UserDTO user)
        {
            if (user.UserId==0)
            {
                await _userRepo.AddNewUser(user);
            }
            else
            {
                await _userRepo.EditUser(user);
            }
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            var user = await _userRepo.GetUserById(userId);
            if(user !=null)
              user.Password = string.Empty;
            return user;
        }

        public async Task<UserDTO> GetUserByLogin(string userName, string passWord)
        {
            var user= await _userRepo.GetUserByLoginDetails(userName, passWord);
            if (user != null)
                user.Password = string.Empty;
            return user;
        }

        public async Task<bool> IsUserNameAlreadyExsist(string userName)
        {
            return await _userRepo.IsUserExists(userName);
        }
    }
}
