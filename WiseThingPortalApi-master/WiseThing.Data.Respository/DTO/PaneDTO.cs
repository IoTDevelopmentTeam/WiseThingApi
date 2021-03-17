using System;
using System.Collections.Generic;
using System.Text;

namespace WiseThing.Data.Respository
{
    public class ConfigDetailsDTO
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int PaneId { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        
    }

    public class PaneDetailsDTO
    {
        public int PaneId { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }

        public int? Index { get; set; }
        public string? Size { get; set; }

    }
}
