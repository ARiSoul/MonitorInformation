using System.Collections.Generic;

namespace ScreenInformation
{
    public static class ScreenManager
    {
        public static List<MonitorInformation> GetMonitors()
        {
            return NativeMethods.GetMonitors();
        }

        public static DisplaySource[] GetDetailedMonitors()
        {
            return NativeMethods.GetDisplays();
        } 
    }
}
