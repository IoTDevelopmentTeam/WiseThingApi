using System;
using System.Collections.Generic;

#nullable disable

namespace WiseThing.Data.Respository
{
    internal partial class User
    {
        internal User()
        {
            Userdevices = new HashSet<Userdevice>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? PhoneNo { get; set; }
        public int UserType { get; set; }
        public string Password { get; set; }
        public DateTime InputDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Usertype UserTypeNavigation { get; set; }
        public virtual ICollection<Userdevice> Userdevices { get; set; }
    }
}
