namespace ScreenInformation
{
    public abstract class GenericDevice
    {
        public string Key { get; protected set; }

        public string Id { get; protected set; }

        public string Name { get; protected set; }

        protected GenericDevice()
        {
            
        }

        protected GenericDevice(string key, string id, string name)
        {
            Key = key;
            Id = id;
            Name = name;
        }
    }
}
