using System;
using System.Collections.Generic;
using System.Text;

namespace WiseThing.Portal.Business
{
    public class UserDeviceAssociation
    {
        public int UserId { get; set; }
        public string DeviceName { get; set; }
        public string TagName { get; set; }
    }
}
