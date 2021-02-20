using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace WiseThing.Data.Respository
{
    
    public class DeviceDTO
    {
        public int DeviceId { get; set; }
        public string DeviceUniqueIdentifier { get; set; }
        public string DeviceTagName { get; set; }
        public string DeviceName { get; set; }
        public bool? IsUsed { get; set; }
        public DateTime? FirstUse { get; set; }
        public DateTime? InputDate { get; set; }
        public string InputBy { get; set; }
    }

    

    public class DashboardModel
    {
        //public Tuple<string,string> content { get; set; }
        //public Data content { get; set; }
        public Data content { get; set; }
        public string thingname { get; set; }
        public  string ctime { get; set; }
        public string ptime { get; set; }
    }

    public class Data
    {
        public string loc { get; set; }
        public string temp { get; set; }
    }


    public class DashboardModel1
    {
        //public Tuple<string,string> content { get; set; }
        //public Data content { get; set; }
        public Dictionary<string,string> content { get; set; }
        public string thingname { get; set; }
        public string ctime { get; set; }
        public string ptime { get; set; }
    }



}
