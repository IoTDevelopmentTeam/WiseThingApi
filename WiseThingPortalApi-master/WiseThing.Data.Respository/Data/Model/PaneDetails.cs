using System;
using System.Collections.Generic;

#nullable disable

namespace WiseThing.Data.Respository
{
    internal partial class PaneDetail
    {
        public int PaneId { get; set; }
        public int DeviceId { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }

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
