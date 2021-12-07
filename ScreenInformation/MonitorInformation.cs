using System.Drawing;

namespace ScreenInformation
{
    public class MonitorInformation
    {
        public int SourceId { get; set; }

        public uint TargetId { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public Rectangle Area { get; set; }

        public Rectangle WorkArea { get; set; }

        public uint Flags { get; set; }

        public string DeviceName { get; set; }

        public string FriendlyName { get; set; }

        public bool IsPrimary { get; set; }
    }
}
