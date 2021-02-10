using System;
using System.Collections.Generic;

#nullable disable

namespace WiseThing.Data.Respository
{
    internal partial class Userdevice
    {
        public int UserId { get; set; }
        public int DeviceId { get; set; }
        public DateTime InputDate { get; set; }

        public virtual Device Device { get; set; }
        public virtual User User { get; set; }
    }
}
