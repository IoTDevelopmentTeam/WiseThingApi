using System;
using System.Collections.Generic;
using System.Text;

namespace WiseThing.Portal.Business
{
    public class DeviceStatus
    {
       public string TagName { get; set; }
        public bool IsUsed { get; set; }
        public DateTime FirstUsed { get; set; }
        public DateTime ExpDate { get; set; }
    }
}
