using System.Runtime.InteropServices;
using System;
using System.Drawing;

namespace NothFace
{
    internal class MouseUtile
    {
        [DllImport("user32.dll")]
    public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

    [DllImport("user32")]
    public static extern int SetCursorPos(int x, int y);

    private const int MouseEV_Move = 0x0001;
    private const int MouseEV_LeftDown = 0x0002;
    private const int MouseEV_LeftUp = 0x0004;
    private const int MouseEV_RightDown = 0x0008;

    private readonly ManualResetEvent stoppeing_event_ = new ManualResetEvent(false);
    TimeSpan interval_;



    private const float MOUSE_SMOOTH = 200f;
    public static void MoveTo(int targetX, int targetY)
    {
        var targetPosition = new System.Drawing.Point(targetX, targetY);
        var curPos = Cursor.Position;

        var diffX = targetPosition.X - curPos.X;
        var diffY = targetPosition.Y - curPos.Y;

        for (int i = 0; i <= MOUSE_SMOOTH; i++)
        {
            float x = curPos.X + (diffX / MOUSE_SMOOTH * i);
            float y = curPos.Y + (diffY / MOUSE_SMOOTH * i);
            Cursor.Position = new System.Drawing.Point((int)x, (int)y);
            Thread.Sleep(1);
        }

        if (Cursor.Position != targetPosition)
        {
            MoveTo(targetPosition.X, targetPosition.Y);
        }
    }
        public void InClick(int x, int y)
        {
            MouseSetPosNclick(x, y);
        }
        public void InClick2(int x, int y, int range_x, int range_y)
        {
            MouseSetPosNclick2(x, y);
        }


        public void MouseSetPosNclick(int x, int y)
        {
            try
            {
                SetCursorPos(x, y);
                stoppeing_event_.WaitOne(interval_);
                MouseClick_now();
            }
            catch (Exception e)
            {
                Console.WriteLine("mspnc");
                Console.WriteLine(e.Message, ToString());
            }
        }

        public void MouseSetPosNclick2(int x, int y)
        {
            try
            {
                SetCursorPos(x, y);
                Thread.Sleep(100);
                stoppeing_event_.WaitOne(interval_);
                MouseClick_now();
            }
            catch (Exception e)
            {
                Console.WriteLine("mspnc2");
                Console.WriteLine(e.Message, ToString());
            }
        }

        public void MouseSetPosNclick3(int x, int y, int range_x, int range_y)
        {
            try
            {
                Random rx = new Random();
                Random ry = new Random();
                int locx = rx.Next(0, range_x);
                int locy = ry.Next(0, range_y);
                SetCursorPos(x + locx, y + locy);

                Thread.Sleep(100);
                stoppeing_event_.WaitOne(interval_);
                MouseClick_now();
            }
            catch (Exception e)
            {
                Console.WriteLine("mspnc3");
                Console.WriteLine(e.Message, ToString());
            }
        }


        public void MouseClick_now()
        {
            try
            {
                mouse_event(MouseEV_LeftDown, 0, 0, 0, 0);
                Thread.Sleep(100);
                mouse_event(MouseEV_LeftUp, 0, 0, 0, 0);
                stoppeing_event_.WaitOne(100);
            }
            catch (Exception e)
            {
                Console.WriteLine("mcn");
                Console.WriteLine(e.Message, ToString());
            }
        }
    }
}