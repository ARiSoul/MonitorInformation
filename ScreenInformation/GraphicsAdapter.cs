using System.Collections.Generic;

namespace ScreenInformation
{
    public class AdapterLUID
    {
        public int LowPart { get; set; }

        public int HightPart { get; set; }
    }

    public class GraphicsAdapter : GenericDevice
    {
        public AdapterLUID LUID { get; set; }

        public List<DisplaySource> Sources { get; set; }

        public GraphicsAdapter(string key, string id, string name) : base(key, id, name)
        {
            Sources = new List<DisplaySource>();
        }

        public void LoadSources()
        {
            
        }
    }
}
