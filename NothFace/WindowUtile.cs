using System.Runtime.InteropServices;

namespace NothFace
{
    internal class WindowUtile
    {
        [System.Runtime.InteropServices.DllImport("User32", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        internal static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr Parent, IntPtr Child, string lpszClass, string lpszWindows);

        [DllImport("user32.dll")]
        public static extern int BringWindowToTop(IntPtr hwnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        public int Window_Check(String AppPlayerName)
        {
            try
            {
                IntPtr findwindow = FindWindow(null, AppPlayerName);
                if (!findwindow.Equals(IntPtr.Zero))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("wc");
                Console.WriteLine(e.Message, ToString());
                return 1;
            }
        }

        public void Window_Set(String AppPlayerName)
        {
            try
            {
                IntPtr findwindow = FindWindow(null, AppPlayerName);
                if (!findwindow.Equals(IntPtr.Zero))
                {

                    BringWindowToTop(findwindow);
                    ShowWindowAsync(findwindow, SW_SHOWMAXIMIZED);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ws");
                Console.WriteLine(e.Message, ToString());
            }
        }
        public void Window_Force_Set(String AppPlayerName)
        {
            try
            {
                IntPtr findwindow = FindWindow(null, AppPlayerName);
                if (!findwindow.Equals(IntPtr.Zero))
                {

                    BringWindowToTop(findwindow);
                    ShowWindowAsync(findwindow, SW_SHOWMAXIMIZED);
                    SetForegroundWindow(findwindow);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("wfs");
                Console.WriteLine(e.Message, ToString());
            }
        }


    }
}