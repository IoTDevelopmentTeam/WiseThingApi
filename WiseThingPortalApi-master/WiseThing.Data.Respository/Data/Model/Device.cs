using System;
using System.Collections.Generic;

#nullable disable

namespace WiseThing.Data.Respository
{
    internal partial class Device
    {
        internal Device()
        {
            Userdevices = new HashSet<Userdevice>();
        }

        public int DeviceId { get; set; }
        public string DeviceUniqueIdentifier { get; set; }
        public string DeviceTagName { get; set; }
        public string DeviceName { get; set; }
        public DateTime InputDate { get; set; }
        public string InputBy { get; set; }

      

        internal virtual ICollection<Userdevice> Userdevices { get; set; }
    }
}
