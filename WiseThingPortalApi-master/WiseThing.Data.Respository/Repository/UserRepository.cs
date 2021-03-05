using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WiseThing.Data.Respository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        //private readonly WisethingPortalContext _context;
        //private readonly IMapper _mapper;
        public UserRepository(WisethingPortalContext context, IMapper mapper ):base(context, mapper)
        {
            //_context = context;
            //_mapper = mapper;
        }

        public async Task AddNewUser(UserDTO userDto)
        {
           var user= _mapper.Map<User>(userDto);
            user.InputDate = DateTime.Now;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task EditUser(UserDTO userDto)
        {
            var user = await _context.Users.SingleAsync(x => x.UserId == userDto.UserId);
           
            user.Email = userDto.Email;
            user.PhoneNo = userDto.PhoneNo;
            user.UserType = user.UserType;
            user.UpdateDate = DateTime.Now;
            user.SecurityQuesId = userDto.SecurityQuesId;
            user.SecurityQuesAns = userDto.SecurityQuesAns;
            await _context.SaveChangesAsync();

        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x=>x.UserId== userId);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserByLoginDetails(string email, string passWord)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email && x.Password== passWord);
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<bool> IsEmailExists(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task ResetPassword(ResetPasswordDTO resetpasswordDto)
        {
            var user = await _context.Users.SingleAsync(x => x.Email == resetpasswordDto.Email);
            user.Password = resetpasswordDto.Password;
            await _context.SaveChangesAsync();

        }
    }
}
