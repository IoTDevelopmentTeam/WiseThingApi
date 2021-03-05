using System;
using System.Collections.Generic;
using System.Text;

namespace WiseThing.Data.Respository
{
    public class DeviceStatusDTO
    {
        public string DeviceTagName { get; set; }
        public bool? IsUsed { get; set; }
        public DateTime? FirstUse { get; set; }
        public DateTime? ExpDate { get; set; }


    }

    public class DeviceAddStatusDTO
    {
        public string DeviceTagName { get; set; }
        public DateTime FirstUse { get; set; }
       
    }
}
