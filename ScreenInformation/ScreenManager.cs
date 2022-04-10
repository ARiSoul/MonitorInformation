using System.Collections.Generic;
using System.Linq;

namespace ScreenInformation
{
    public static class ScreenManager
    {
        private static List<DisplaySource> _allScreens;
        public static List<DisplaySource> AllScreens
        {
            get
            {
                if (_allScreens == null)
                    _allScreens = GetDetailedMonitors().ToList();

                return _allScreens;
            }
        }

        public static List<MonitorInformation> GetMonitors()
        {
            return NativeMethods.GetMonitors();
        }

        public static DisplaySource[] GetDetailedMonitors()
        {
            return NativeMethods.GetDisplays();
        } 

        public static DisplaySource GetDetailedMonitorByFriendlyName(string friendlyName)
        {
            var monitor = AllScreens.FirstOrDefault(m => m.MonitorInformation.FriendlyName == friendlyName);

            return monitor;
        }
    }
}
