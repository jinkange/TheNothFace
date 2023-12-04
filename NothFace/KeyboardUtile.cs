using System.Runtime.InteropServices;

namespace NothFace
{
    internal class KeyboardUtile
    {
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte vk, byte scan, int flags, ref int extrainfo);
    }
}