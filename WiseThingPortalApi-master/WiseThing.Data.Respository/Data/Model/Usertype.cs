using System;
using System.Collections.Generic;

#nullable disable

namespace WiseThing.Data.Respository
{
    internal partial class Usertype
    {
        internal Usertype()
        {
            Users = new HashSet<User>();
        }

        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
