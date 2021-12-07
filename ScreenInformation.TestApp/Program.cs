using System;

namespace ScreenInformation.TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var screens = ScreenManager.GetMonitors();
            var detailedScreens = ScreenManager.GetDetailedMonitors();

            Console.WriteLine("Screens Found:");
            foreach (var screen in detailedScreens)
            {
                Console.WriteLine(screen.MonitorInformation.FriendlyName);
                Console.WriteLine(screen.MonitorInformation.Area.ToString());
                Console.WriteLine($"Is Primary: {screen.MonitorInformation.IsPrimary}");
            }
        }
    }
}
