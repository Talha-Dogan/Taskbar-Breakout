using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace TaskbarBreakout.Core
{
    public enum TaskbarEdge { Left = 0, Top = 1, Right = 2, Bottom = 3 }

    public struct TaskbarInfo
    {
        public RectInt Bounds;
        public TaskbarEdge Edge;
        public float DpiScale;
    }

    public static class TaskbarDetector
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct APPBARDATA
        {
            public uint cbSize;
            public IntPtr hWnd;
            public uint uCallbackMessage;
            public uint uEdge;
            public RECT rc;
            public int lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left, top, right, bottom;
        }

        private const uint ABM_GETTASKBARPOS = 0x00000005;

        [DllImport("shell32.dll")]
        private static extern uint SHAppBarMessage(uint dwMessage, ref APPBARDATA pData);

        [DllImport("user32.dll")]
        private static extern uint GetDpiForWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

#if !UNITY_EDITOR
        public static TaskbarInfo Detect()
        {
            var data = new APPBARDATA();
            data.cbSize = (uint)Marshal.SizeOf(data);

            SHAppBarMessage(ABM_GETTASKBARPOS, ref data);

            IntPtr taskbarHwnd = FindWindow("Shell_TrayWnd", null);
            float dpi = taskbarHwnd != IntPtr.Zero
                ? GetDpiForWindow(taskbarHwnd) / 96f
                : 1f;

            return new TaskbarInfo
            {
                Bounds = new RectInt(
                    data.rc.left,
                    data.rc.top,
                    data.rc.right - data.rc.left,
                    data.rc.bottom - data.rc.top),
                Edge = (TaskbarEdge)data.uEdge,
                DpiScale = dpi
            };
        }
#else
        // Editor'da çalışmaz — sahte veri döner
        public static TaskbarInfo Detect()
        {
            return new TaskbarInfo
            {
                Bounds = new RectInt(0, Screen.currentResolution.height - 48,
                    Screen.currentResolution.width, 48),
                Edge = TaskbarEdge.Bottom,
                DpiScale = 1f
            };
        }
#endif
    }
}
