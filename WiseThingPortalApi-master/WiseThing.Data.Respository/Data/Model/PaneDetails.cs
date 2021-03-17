using System;
using System.Collections.Generic;

#nullable disable

namespace WiseThing.Data.Respository
{
    internal partial class PaneDetail
    {
        public int PaneId { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int? Index { get; set; }
        public string? Size { get; set; }

        public virtual Device Device { get; set; }

    }
    internal partial class ConfigDetail
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int PaneId { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }

    }
}
