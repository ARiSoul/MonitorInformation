namespace ScreenInformation
{
    public class DisplaySource : GenericDevice
    {
        public MonitorInformation MonitorInformation { get; set; }

        public string SourceName { get; set; }

        public DisplaySource(string key, string id, string name, string sourceName) : base(key, id, name)
        {
            SourceName = sourceName;
        }

        public override bool Equals(object obj)
        {
            DisplaySource ds = obj as DisplaySource;

            if (ds == null)
            {
                return false;
            }

            return Equals(ds);
        }

        public bool Equals(DisplaySource displaySource)
        {
            return Key == displaySource.Key;
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }
}
