using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace ScreenInformation
{
    internal enum QUERY_DEVICE_CONFIG_FLAGS : uint
    {
        QDC_ALL_PATHS = 0x00000001,
        QDC_ONLY_ACTIVE_PATHS = 0x00000002,
        QDC_DATABASE_CURRENT = 0x00000004
    }

    internal enum DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY : uint
    {
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_OTHER = 0xFFFFFFFF,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HD15 = 0,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SVIDEO = 1,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPOSITE_VIDEO = 2,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPONENT_VIDEO = 3,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DVI = 4,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HDMI = 5,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_LVDS = 6,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_D_JPN = 8,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDI = 9,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EXTERNAL = 10,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EMBEDDED = 11,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EXTERNAL = 12,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EMBEDDED = 13,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDTVDONGLE = 14,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_MIRACAST = 15,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_INTERNAL = 0x80000000,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_FORCE_UINT32 = 0xFFFFFFFF
    }

    internal enum DISPLAYCONFIG_SCANLINE_ORDERING : uint
    {
        DISPLAYCONFIG_SCANLINE_ORDERING_UNSPECIFIED = 0,
        DISPLAYCONFIG_SCANLINE_ORDERING_PROGRESSIVE = 1,
        DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED = 2,
        DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED_UPPERFIELDFIRST = DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED,
        DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED_LOWERFIELDFIRST = 3,
        DISPLAYCONFIG_SCANLINE_ORDERING_FORCE_UINT32 = 0xFFFFFFFF
    }

    internal enum DISPLAYCONFIG_ROTATION : uint
    {
        DISPLAYCONFIG_ROTATION_IDENTITY = 1,
        DISPLAYCONFIG_ROTATION_ROTATE90 = 2,
        DISPLAYCONFIG_ROTATION_ROTATE180 = 3,
        DISPLAYCONFIG_ROTATION_ROTATE270 = 4,
        DISPLAYCONFIG_ROTATION_FORCE_UINT32 = 0xFFFFFFFF
    }

    internal enum DISPLAYCONFIG_SCALING : uint
    {
        DISPLAYCONFIG_SCALING_IDENTITY = 1,
        DISPLAYCONFIG_SCALING_CENTERED = 2,
        DISPLAYCONFIG_SCALING_STRETCHED = 3,
        DISPLAYCONFIG_SCALING_ASPECTRATIOCENTEREDMAX = 4,
        DISPLAYCONFIG_SCALING_CUSTOM = 5,
        DISPLAYCONFIG_SCALING_PREFERRED = 128,
        DISPLAYCONFIG_SCALING_FORCE_UINT32 = 0xFFFFFFFF
    }

    internal enum DISPLAYCONFIG_PIXELFORMAT : uint
    {
        DISPLAYCONFIG_PIXELFORMAT_8BPP = 1,
        DISPLAYCONFIG_PIXELFORMAT_16BPP = 2,
        DISPLAYCONFIG_PIXELFORMAT_24BPP = 3,
        DISPLAYCONFIG_PIXELFORMAT_32BPP = 4,
        DISPLAYCONFIG_PIXELFORMAT_NONGDI = 5,
        DISPLAYCONFIG_PIXELFORMAT_FORCE_UINT32 = 0xffffffff
    }

    internal enum DISPLAYCONFIG_MODE_INFO_TYPE : uint
    {
        DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE = 1,
        DISPLAYCONFIG_MODE_INFO_TYPE_TARGET = 2,
        DISPLAYCONFIG_MODE_INFO_TYPE_FORCE_UINT32 = 0xFFFFFFFF
    }

    internal enum DISPLAYCONFIG_DEVICE_INFO_TYPE : uint
    {
        DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME = 1,
        DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME = 2,
        DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_PREFERRED_MODE = 3,
        DISPLAYCONFIG_DEVICE_INFO_GET_ADAPTER_NAME = 4,
        DISPLAYCONFIG_DEVICE_INFO_SET_TARGET_PERSISTENCE = 5,
        DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_BASE_TYPE = 6,
        DISPLAYCONFIG_DEVICE_INFO_FORCE_UINT32 = 0xFFFFFFFF
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LUID
    {
        public uint LowPart;
        public int HighPart;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DISPLAYCONFIG_PATH_SOURCE_INFO
    {
        public LUID adapterId;
        public uint id;
        public uint modeInfoIdx;
        public uint statusFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DISPLAYCONFIG_PATH_TARGET_INFO
    {
        public LUID adapterId;
        public uint id;
        public uint modeInfoIdx;
        DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY outputTechnology;
        DISPLAYCONFIG_ROTATION rotation;
        DISPLAYCONFIG_SCALING scaling;
        DISPLAYCONFIG_RATIONAL refreshRate;
        DISPLAYCONFIG_SCANLINE_ORDERING scanLineOrdering;
        public bool targetAvailable;
        public uint statusFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DISPLAYCONFIG_RATIONAL
    {
        public uint Numerator;
        public uint Denominator;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DISPLAYCONFIG_PATH_INFO
    {
        public DISPLAYCONFIG_PATH_SOURCE_INFO sourceInfo;
        public DISPLAYCONFIG_PATH_TARGET_INFO targetInfo;
        public uint flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DISPLAYCONFIG_2DREGION
    {
        public uint cx;
        public uint cy;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DISPLAYCONFIG_VIDEO_SIGNAL_INFO
    {
        public ulong pixelRate;
        public DISPLAYCONFIG_RATIONAL hSyncFreq;
        public DISPLAYCONFIG_RATIONAL vSyncFreq;
        public DISPLAYCONFIG_2DREGION activeSize;
        public DISPLAYCONFIG_2DREGION totalSize;
        public uint videoStandard;
        public DISPLAYCONFIG_SCANLINE_ORDERING scanLineOrdering;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DISPLAYCONFIG_TARGET_MODE
    {
        public DISPLAYCONFIG_VIDEO_SIGNAL_INFO targetVideoSignalInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct POINTL
    {
        int x;
        int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DISPLAYCONFIG_SOURCE_MODE
    {
        public uint width;
        public uint height;
        public DISPLAYCONFIG_PIXELFORMAT pixelFormat;
        public POINTL position;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct DISPLAYCONFIG_MODE_INFO_UNION
    {
        [FieldOffset(0)]
        public DISPLAYCONFIG_TARGET_MODE targetMode;
        [FieldOffset(0)]
        public DISPLAYCONFIG_SOURCE_MODE sourceMode;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DISPLAYCONFIG_MODE_INFO
    {
        public DISPLAYCONFIG_MODE_INFO_TYPE infoType;
        public uint id;
        public LUID adapterId;
        public DISPLAYCONFIG_MODE_INFO_UNION modeInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS
    {
        public uint value;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DISPLAYCONFIG_DEVICE_INFO_HEADER
    {
        public DISPLAYCONFIG_DEVICE_INFO_TYPE type;
        public uint size;
        public LUID adapterId;
        public uint id;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct DISPLAYCONFIG_TARGET_DEVICE_NAME
    {
        public DISPLAYCONFIG_DEVICE_INFO_HEADER header;
        public DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS flags;
        public DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY outputTechnology;
        public ushort edidManufactureId;
        public ushort edidProductCodeId;
        public uint connectorInstance;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string monitorFriendlyDeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string monitorDevicePath;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct MONITORINFOEX
    {
        public int Size;
        public RECT Monitor;
        public RECT WorkArea;
        public uint Flags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [Flags()]
    internal enum DisplayDeviceStateFlags : int
    {
        /// <summary>The device is part of the desktop.</summary>
        AttachedToDesktop = 0x1,
        MultiDriver = 0x2,
        /// <summary>The device is part of the desktop.</summary>
        PrimaryDevice = 0x4,
        /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
        MirroringDriver = 0x8,
        /// <summary>The device is VGA compatible.</summary>
        VGACompatible = 0x10,
        /// <summary>The device is removable; it cannot be the primary display.</summary>
        Removable = 0x20,
        /// <summary>The device has more display modes than its output devices support.</summary>
        ModesPruned = 0x8000000,
        Remote = 0x4000000,
        Disconnect = 0x2000000
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct DISPLAY_DEVICE
    {
        [MarshalAs(UnmanagedType.U4)]
        public int cb;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceString;
        [MarshalAs(UnmanagedType.U4)]
        public DisplayDeviceStateFlags StateFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceKey;
    }

    [Flags]
    internal enum DevModeFlags
    {
        Zero = 0x0,

        Orientation = 0x1,
        PaperSize = 0x2,
        PaperLength = 0x4,
        PaperWidth = 0x8,
        Scale = 0x10,
        Position = 0x20,
        Nup = 0x40,
        DisplayOrientation = 0x80,
        Copies = 0x100,
        DefaultSource = 0x200,
        PrintQuality = 0x400,
        Color = 0x800,
        Duplex = 0x1000,
        YResolution = 0x2000,
        TtOption = 0x4000,
        Collate = 0x8000,
        FormName = 0x10000,
        LogPixels = 0x20000,
        BitsPerPixel = 0x40000,
        PelsWidth = 0x80000,
        PelsHeight = 0x100000,
        DisplayFlags = 0x200000,
        DisplayFrequency = 0x400000,
        IcmMethod = 0x800000,
        IcmIntent = 0x1000000,
        MediaType = 0x2000000,
        DitherType = 0x4000000,
        PanningWidth = 0x8000000,
        PanningHeight = 0x10000000,
        DisplayFixedOutput = 0x20000000
    }

    internal static class NativeMethods
    {
        public const int ERROR_SUCCESS = 0;
        delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

        [DllImport("user32.dll")]
        static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);
        
        [DllImport("user32.dll")]
        public static extern int GetDisplayConfigBufferSizes(QUERY_DEVICE_CONFIG_FLAGS Flags, out uint NumPathArrayElements, out uint NumModeInfoArrayElements);

        [DllImport("user32.dll")]
        public static extern int QueryDisplayConfig(QUERY_DEVICE_CONFIG_FLAGS Flags, ref uint NumPathArrayElements, [Out] DISPLAYCONFIG_PATH_INFO[] PathInfoArray, ref uint NumModeInfoArrayElements, [Out] DISPLAYCONFIG_MODE_INFO[] ModeInfoArray, IntPtr CurrentTopologyId);

        [DllImport("user32.dll")]
        public static extern int DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_TARGET_DEVICE_NAME deviceName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFOEX lpmi);

        [DllImport("user32.dll")]
        static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);
        
        public static DISPLAY_DEVICE GetDisplayDevice()
        {
            return new DISPLAY_DEVICE()
            {
                cb = Marshal.SizeOf(typeof(DISPLAY_DEVICE))
            };
        }


        private static List<MonitorInformation> Monitors
        {
            get;
            set;
        } 

        private static int MonitorCounter
        {
            get;
            set;
        }

        private static bool MonitorEnum(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
        {
            MONITORINFOEX mi = new MONITORINFOEX();
            mi.Size = Marshal.SizeOf(typeof(MONITORINFOEX));
            bool success = GetMonitorInfo(hMonitor, ref mi);

            if (success)
            {
                MonitorInformation di = new MonitorInformation();
                di.Width = (mi.Monitor.Right - mi.Monitor.Left);
                di.Height = (mi.Monitor.Bottom - mi.Monitor.Top);
                di.Area = GetRectFromNative(mi.Monitor);
                di.WorkArea = GetRectFromNative(mi.WorkArea);
                di.Flags = mi.Flags;
                di.SourceId = MonitorCounter;
                di.DeviceName = mi.DeviceName;
                MonitorCounter++;
                Monitors.Add(di);
            }

            return true;
        }

        private static Rectangle GetRectFromNative(RECT rect)
        {
            return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }
        

        internal static List<MonitorInformation> GetMonitors()
        {
            Monitors = new List<MonitorInformation>();
            MonitorCounter = 0;
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MonitorEnum, IntPtr.Zero);
            return Monitors;
        }

        internal static DISPLAYCONFIG_TARGET_DEVICE_NAME MonitorDeviceInfo(LUID adapterId, uint targetId)
        {
            DISPLAYCONFIG_TARGET_DEVICE_NAME deviceName = new DISPLAYCONFIG_TARGET_DEVICE_NAME();
            deviceName.header.size = (uint)Marshal.SizeOf(typeof(DISPLAYCONFIG_TARGET_DEVICE_NAME));
            deviceName.header.adapterId = adapterId;
            deviceName.header.id = targetId;
            deviceName.header.type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME;
            int error = DisplayConfigGetDeviceInfo(ref deviceName);
            if (error != ERROR_SUCCESS)
                throw new Win32Exception(error);
            return deviceName;
        }

        public static byte[] ToLPTStr(string str)
        {
            var lptArray = new byte[str.Length + 1];

            var index = 0;
            foreach (char c in str.ToCharArray())
                lptArray[index++] = Convert.ToByte(c);

            lptArray[index] = Convert.ToByte('\0');

            return lptArray;
        }

        internal static GraphicsAdapter[] GetDisplayAdapters()
        {
            //Get Device infos
            uint PathCount, ModeCount;
            int error = NativeMethods.GetDisplayConfigBufferSizes(QUERY_DEVICE_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS, out PathCount, out ModeCount);
            if (error != NativeMethods.ERROR_SUCCESS)
            {
                throw new Win32Exception(error);
            }

            DISPLAYCONFIG_PATH_INFO[] displayPaths = new DISPLAYCONFIG_PATH_INFO[PathCount];
            DISPLAYCONFIG_MODE_INFO[] displayModes = new DISPLAYCONFIG_MODE_INFO[ModeCount];
            error = NativeMethods.QueryDisplayConfig(QUERY_DEVICE_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS, ref PathCount, displayPaths, ref ModeCount, displayModes, IntPtr.Zero);
            if (error != NativeMethods.ERROR_SUCCESS)
            {
                throw new Win32Exception(error);
            }

            Dictionary<uint, DISPLAYCONFIG_TARGET_DEVICE_NAME> targetDevices = new Dictionary<uint, DISPLAYCONFIG_TARGET_DEVICE_NAME>();
            for (int i = 0; i < ModeCount; i++)
            {
                if (displayModes[i].infoType == DISPLAYCONFIG_MODE_INFO_TYPE.DISPLAYCONFIG_MODE_INFO_TYPE_TARGET)
                {
                    targetDevices.Add(displayModes[i].id, MonitorDeviceInfo(displayModes[i].adapterId, displayModes[i].id));
                }
            }

            DISPLAY_DEVICE device = GetDisplayDevice();
            List<GraphicsAdapter> adapters = new List<GraphicsAdapter>();
            Dictionary<string, GraphicsAdapter> ids = new Dictionary<string, GraphicsAdapter>();
            List<MonitorInformation> monitors = GetMonitors();
            for (uint deviceId = 0; EnumDisplayDevices(null, deviceId, ref device, 0); deviceId++)
            {
                if (!ids.ContainsKey(device.DeviceID))
                {
                    ids.Add(device.DeviceID, new GraphicsAdapter(device.DeviceKey, device.DeviceID, device.DeviceString));
                    adapters.Add(ids[device.DeviceID]);
                }

                GraphicsAdapter adapter = ids[device.DeviceID];
                DISPLAY_DEVICE displayDevice = GetDisplayDevice();
                for (uint displayId = 0; EnumDisplayDevices(device.DeviceName, displayId, ref displayDevice, 0); displayId++)
                {
                    DisplaySource display = new DisplaySource(displayDevice.DeviceKey, displayDevice.DeviceID, displayDevice.DeviceName, device.DeviceName);
                    
                    MonitorInformation monitorInfo = monitors.FirstOrDefault(x => x.DeviceName == display.SourceName);

                    if (monitorInfo != null)
                    {
                        display.MonitorInformation = monitorInfo;
                        DISPLAYCONFIG_PATH_INFO displayPath = displayPaths.FirstOrDefault(x => x.sourceInfo.id == monitorInfo.SourceId);
                        monitorInfo.TargetId = displayPath.targetInfo.id;
                        DISPLAYCONFIG_TARGET_DEVICE_NAME targetDeviceInfo = targetDevices[monitorInfo.TargetId];
                        monitorInfo.FriendlyName = targetDeviceInfo.monitorFriendlyDeviceName;
                    }
                    
                    adapter.Sources.Add(display);

                    displayDevice = GetDisplayDevice();
                }
                
                device = GetDisplayDevice();
            }

            return adapters.ToArray();
        }

        internal static DisplaySource[] GetDisplays()
        {
            //Get Device infos
            uint PathCount, ModeCount;
            int error = NativeMethods.GetDisplayConfigBufferSizes(QUERY_DEVICE_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS, out PathCount, out ModeCount);
            if (error != NativeMethods.ERROR_SUCCESS)
            {
                throw new Win32Exception(error);
            }

            DISPLAYCONFIG_PATH_INFO[] displayPaths = new DISPLAYCONFIG_PATH_INFO[PathCount];
            DISPLAYCONFIG_MODE_INFO[] displayModes = new DISPLAYCONFIG_MODE_INFO[ModeCount];
            error = NativeMethods.QueryDisplayConfig(QUERY_DEVICE_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS, ref PathCount, displayPaths, ref ModeCount, displayModes, IntPtr.Zero);
            if (error != NativeMethods.ERROR_SUCCESS)
            {
                throw new Win32Exception(error);
            }

            Dictionary<uint, DISPLAYCONFIG_TARGET_DEVICE_NAME> targetDevices = new Dictionary<uint, DISPLAYCONFIG_TARGET_DEVICE_NAME>();
            for (int i = 0; i < ModeCount; i++)
            {
                if (displayModes[i].infoType == DISPLAYCONFIG_MODE_INFO_TYPE.DISPLAYCONFIG_MODE_INFO_TYPE_TARGET)
                {
                    targetDevices.Add(displayModes[i].id, MonitorDeviceInfo(displayModes[i].adapterId, displayModes[i].id));
                }
            }

            DISPLAY_DEVICE device = GetDisplayDevice();
            List<GraphicsAdapter> adapters = new List<GraphicsAdapter>();
            Dictionary<string, GraphicsAdapter> ids = new Dictionary<string, GraphicsAdapter>();
            List<MonitorInformation> monitors = GetMonitors();
            for (uint deviceId = 0; EnumDisplayDevices(null, deviceId, ref device, 0); deviceId++)
            {
                if (!ids.ContainsKey(device.DeviceID))
                {
                    ids.Add(device.DeviceID, new GraphicsAdapter(device.DeviceKey, device.DeviceID, device.DeviceString));
                    adapters.Add(ids[device.DeviceID]);
                }

                bool isPrimary = device.StateFlags.HasFlag(DisplayDeviceStateFlags.PrimaryDevice);

                GraphicsAdapter adapter = ids[device.DeviceID];
                DISPLAY_DEVICE displayDevice = GetDisplayDevice();
                for (uint displayId = 0; EnumDisplayDevices(device.DeviceName, displayId, ref displayDevice, 0); displayId++)
                {
                    DisplaySource display = new DisplaySource(displayDevice.DeviceKey, displayDevice.DeviceID, displayDevice.DeviceName, device.DeviceName);

                    MonitorInformation monitorInfo = monitors.FirstOrDefault(x => x.DeviceName == display.SourceName);

                    if (monitorInfo != null)
                    {
                        display.MonitorInformation = monitorInfo;
                        DISPLAYCONFIG_PATH_INFO displayPath = displayPaths.FirstOrDefault(x => x.sourceInfo.id == monitorInfo.SourceId);
                        monitorInfo.TargetId = displayPath.targetInfo.id;
                        DISPLAYCONFIG_TARGET_DEVICE_NAME targetDeviceInfo = targetDevices[monitorInfo.TargetId];
                        monitorInfo.FriendlyName = targetDeviceInfo.monitorFriendlyDeviceName;
                        monitorInfo.IsPrimary = isPrimary;
                    }
                    
                    adapter.Sources.Add(display);

                    displayDevice = GetDisplayDevice();
                }

                device = GetDisplayDevice();
            }

            return adapters.SelectMany(x => x.Sources).ToArray();
        }
    }
}
