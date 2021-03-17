using System;
using System.Collections.Generic;
using System.Text;

namespace WiseThing.Data.Respository
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string? PhoneNo { get; set; }
        public int UserType { get; set; }
        public string Password { get; set; }
        public int SecurityQuesId { get; set; }

        public string SecurityQuesAns { get; set; }
        public DateTime InputDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
