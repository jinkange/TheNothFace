/*
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using OpenCvSharp;
using MySql.Data.MySqlClient;
using System.Net;
using System.Diagnostics;
using System.ComponentModel;
using OpenQA.Selenium.Chrome;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Text;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Discord
{

    public partial class Discord : Form
    {


        string ABS_Id;
        string ABS_Pw;
        string ABS_2pw;
        string ABS_Name;
        string ABS_Game_Name;
        string ABS_Game_Server;
        string ABS_Login_Type;
        string ABS_Work_Date;
        string ABS_Work_Min;
        string ABS_Work_Hour;
        string Timer_Finish;
        string Pc_Num;
        string myRealIp;
        string Chrome_Version_Path;
        string top_class;
        string coupon;
        string subs_desc;
        string team_name;
        string Web_Url;
        string Web_Btn_Total;
        string WindowHandler;
        string token;
        string memberNick;
        Queue Web_Btn = new Queue();

        //Stack Web_Btn = new Stack();

        Int64 ABS_Order_Num;
        int ABS_Try_Count;
        int DB_Switch = 0;
        int ABS_Work_Time;
        int ABS_Play_Time = 0;
        int Watiting_Switch = 0;
        int Now_Time;
        int Start_Check_Switch = 0;
        int Ready_Check_Switch = 0;
        int Voice_Login_Switch = 0;
        int League_Count;
        int League_Count_Switch = 0;
        int Voice_Play_Switch = 0;
        int Timer_Play_Check_Switch = 0;
        int ABS_Off = 0;
        int ABS_Off_Count = 0;
        int Web_Reward_Switch = 0;
        int Web_Reward_Page_Login_Switch = 0;
        int Web_Reward_Page_Login_Check = 0;
        int Launch_Error_switch = 0;
        int windowSizeCheck = 0;
        int Error_Code;

        int First_2PW;
        int Second_2PW;
        int Third_2PW;
        int Fourth_2PW;
        int First_2PW_Switch;
        int Second_2PW_Switch;
        int Third_2PW_Switch;
        int Fourth_2PW_Switch;
        int Matching_2PW_Reset_Switch;

        int Window_Force_Set_Switch = 0;
        int Login_Try = 0;
        int Info = 0;
        int Error_Check_Switch = 0;
        int Pc_number_check = 0;
        //매치모드
        int Director_Mode = 0;
        int targetIp = 0;
        string targetIpInfo;

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        private const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        private static extern IntPtr ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        public Bitmap CaptureApplication(string procName)
        {
            Process proc;

            // Cater for cases when the process can't be located.
            try
            {
                proc = Process.GetProcessesByName(procName)[0];
            }
            catch (IndexOutOfRangeException e)
            {
                return null;
            }

            // You need to focus on the application
            SetForegroundWindow(proc.MainWindowHandle);
            ShowWindow(proc.MainWindowHandle, SW_RESTORE);

            // You need some amount of delay, but 1 second may be overkill
            Thread.Sleep(1000);

            Rect rect = new Rect();
            IntPtr error = GetWindowRect(proc.MainWindowHandle, ref rect);

            // sometimes it gives error.
            while (error == (IntPtr)0)
            {
                error = GetWindowRect(proc.MainWindowHandle, ref rect);
            }

            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics.FromImage(bmp).CopyFromScreen(rect.left,
                                                   rect.top,
                                                   0,
                                                   0,
                                                   new System.Drawing.Size(width, height),
                                                   CopyPixelOperation.SourceCopy);
            return bmp;
        }
        protected ChromeDriverService _driverService = null;
        protected ChromeOptions _options = null;
        protected ChromeDriver _driver = null;

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);

        private const int MouseEV_Move = 0x0001;
        private const int MouseEV_LeftDown = 0x0002;
        private const int MouseEV_LeftUp = 0x0004;
        private const int MouseEV_RightDown = 0x0008;
        private const int MouseEV_RightUp = 0x0010;

        private readonly ManualResetEvent stoppeing_event_ = new ManualResetEvent(false);
        TimeSpan interval_;



        private const float MOUSE_SMOOTH = 50f;
        public static void MoveTo(int targetX, int targetY)
        {
            Random rx = new Random();
            Random ry = new Random();
            int locx = rx.Next(-100, 100);
            int locy = rx.Next(-100, 100);
            targetX += locx;
            targetY += locy;
            var targetPosition = new System.Drawing.Point(targetX, targetY);
            var curPos = Cursor.Position;

            var diffX = targetPosition.X - curPos.X;
            var diffY = targetPosition.Y - curPos.Y;

            for (int i = 0; i <= MOUSE_SMOOTH; i++)
            {
                float x = curPos.X + (diffX / MOUSE_SMOOTH * i);
                float y = curPos.Y + (diffY / MOUSE_SMOOTH * i);
                Cursor.Position = new System.Drawing.Point((int)x, (int)y);
                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(3, 10); //new
                Thread.Sleep(Set_Rand_Time);
            }

            if (Cursor.Position != targetPosition)
            {
                MoveTo(targetPosition.X, targetPosition.Y);
            }
        }

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte vk, byte scan, int flags, ref int extrainfo);


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


        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        System.Threading.Thread TrackWorkthread;

        System.Threading.Thread TrackWorkthread_FIFA;

        System.Threading.Thread TrackWorkthread_Timer;
        System.Threading.Thread TrackWorkthread_Get_User_Data;
        System.Threading.Thread TrackWorkthread_Force_Reboot;
        System.Threading.Thread TrackWorkthread_DB_Delete;
        System.Threading.Thread TrackWorkthread_Web_Reward;

        //사용 함수
        static int Get_Com_Num2()
        {
            IPHostEntry IPHost = Dns.GetHostByName(Dns.GetHostName());
            string myip = IPHost.AddressList[0].ToString();

            if (myip.Contains("114.202.76."))
            {
                return 1;
            }
            else if (myip.Contains("123.212.128."))
            {
                return 2;
            }
            else if (myip.Contains("58.224.41."))
            {
                return 2;
            }
            else if (myip.Contains("121.183.127."))
            {
                return 3; // 3호점인데 1호점과 경로가 같음
            }
            else
            {
                return 4;
            }


        }
        public void InClick(int x, int y)
        {
            MouseSetPosNclick(x, y);
        }
        public void InClick_right(int x, int y)
        {
            MouseSetPosNclick_right(x, y);
        }
        public void InClick_scroll(int x, int y)
        {
            MouseSetPosNclick_scroll(x, y);
        }
        public void InClick_x(int x, int y)
        {
            MouseSetPosNclick_x(x, y);
        }
        public void InClick_large(int x, int y)
        {
            MouseSetPosNclick_large(x, y);
        }

        public void MouseSetPosNclick_large(int x, int y)
        {
            try
            {
                Random rx = new Random();
                Random ry = new Random();
                int locx = rx.Next(-80, 80);
                int locy = rx.Next(-400, 400);

                SetCursorPos(x + locx, y + locy);
                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(50, 150); //new
                Thread.Sleep(Set_Rand_Time);


                stoppeing_event_.WaitOne(interval_);
                MouseClick_now();
            }
            catch (Exception e)
            {
                Console.WriteLine("mspnc");
                Console.WriteLine(e.Message, ToString());
            }

        }

        public void MouseSetPosNclick_x(int x, int y)
        {
            try
            {
                Random rx = new Random();
                Random ry = new Random();
                int locx = rx.Next(-15, 15);
                int locy = rx.Next(-5, 5);

                SetCursorPos(x + locx, y + locy);
                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(50, 150); //new
                Thread.Sleep(Set_Rand_Time);


                stoppeing_event_.WaitOne(interval_);
                MouseClick_now();
            }
            catch (Exception e)
            {
                Console.WriteLine("mspnc");
                Console.WriteLine(e.Message, ToString());
            }
        }

        public void InClick2(int x, int y, int range_x, int range_y)
        {
            MouseSetPosNclick3(x, y, range_x, range_y);
        }
        public void InClick20(int x, int y)
        {
            MouseSetPosNclick20(x, y);
        }
        public void InClick30(int x, int y)
        {
            MouseSetPosNclick30(x, y);
        }
        public void MouseSetPosNclick(int x, int y)
        {
            try
            {
                Random rx = new Random();
                Random ry = new Random();
                int locx = rx.Next(-10, 10);
                int locy = ry.Next(-5, 5);
                SetCursorPos(x + locx, y + locy);
                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(50, 150); //new
                Thread.Sleep(Set_Rand_Time);


                stoppeing_event_.WaitOne(interval_);
                MouseClick_now();
            }
            catch (Exception e)
            {
                Console.WriteLine("mspnc");
                Console.WriteLine(e.Message, ToString());
            }
        }
        public void MouseSetPosNclick_right(int x, int y)
        {
            try
            {

                SetCursorPos(x, y);
                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(50, 150); //new
                Thread.Sleep(Set_Rand_Time);


                stoppeing_event_.WaitOne(interval_);
                MouseClick_now_right();
            }
            catch (Exception e)
            {
                Console.WriteLine("mspnc");
                Console.WriteLine(e.Message, ToString());
            }
        }
        public void MouseSetPosNclick_scroll(int x, int y)
        {
            try
            {
                Random rx = new Random();
                Random ry = new Random();
                int locx = rx.Next(-2, 2);
                int locy = ry.Next(-5, 15);
                SetCursorPos(x + locx, y + locy);
                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(50, 150); //new
                Thread.Sleep(Set_Rand_Time);


                stoppeing_event_.WaitOne(interval_);
                MouseClick_now();
            }
            catch (Exception e)
            {
                Console.WriteLine("mspnc");
                Console.WriteLine(e.Message, ToString());
            }
        }
        public void InClick10(int x, int y)
        {
            MouseSetPosNclick10(x, y);
        }
        public void MouseSetPosNclick10(int x, int y)
        {
            try
            {
                Random rx = new Random();
                Random ry = new Random();
                int locx = rx.Next(-10, 10);
                int locy = ry.Next(-10, 10);
                SetCursorPos(x + locx, y + locy);

                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(50, 150); //new
                Thread.Sleep(Set_Rand_Time);


                stoppeing_event_.WaitOne(interval_);
                MouseClick_now10();
            }
            catch (Exception e)
            {
                Console.WriteLine("mspnc");
                Console.WriteLine(e.Message, ToString());
            }
        }
        public void MouseSetPosNclick20(int x, int y)
        {
            try
            {
                Random rx = new Random();
                Random ry = new Random();
                int locx = rx.Next(-20, 20);
                int locy = ry.Next(-9, 9);
                SetCursorPos(x + locx, y + locy);

                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(50, 150); //new
                Thread.Sleep(Set_Rand_Time);


                stoppeing_event_.WaitOne(interval_);
                MouseClick_now10();
            }
            catch (Exception e)
            {
                Console.WriteLine("mspnc");
                Console.WriteLine(e.Message, ToString());
            }
        }
        public void MouseSetPosNclick30(int x, int y)
        {
            try
            {
                Random rx = new Random();
                Random ry = new Random();
                int locx = rx.Next(0, 10);
                int locy = ry.Next(0, 10);
                SetCursorPos(x + locx, y + locy);

                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(50, 150); //new
                Thread.Sleep(Set_Rand_Time);


                stoppeing_event_.WaitOne(interval_);
                MouseClick_now30();
            }
            catch (Exception e)
            {
                Console.WriteLine("mspnc");
                Console.WriteLine(e.Message, ToString());
            }
        }
        public void MouseClick_now10()
        {
            try
            {

                Random Rand_Time2 = new Random();
                int Set_Rand_Time2 = Rand_Time2.Next(33, 100); //new
                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(0, Set_Rand_Time2); //new

                int set_time;

                if (Set_Rand_Time == 0)
                {
                    Random Rand_Time3 = new Random();
                    set_time = Rand_Time3.Next(1000, 2000); //new
                }
                else
                {
                    set_time = Rand_Time.Next(150, 250); //new
                }
                mouse_event(MouseEV_LeftDown, 0, 0, 0, 0);

                Thread.Sleep(set_time);
                mouse_event(MouseEV_LeftUp, 0, 0, 0, 0);
                stoppeing_event_.WaitOne(100);
            }
            catch (Exception e)
            {
                Console.WriteLine("mcn");
                Console.WriteLine(e.Message, ToString());
            }
        }
        public void MouseClick_now30()
        {
            try
            {

                Random Rand_Time = new Random();


                int set_time;
                set_time = Rand_Time.Next(150, 250); //new
                mouse_event(MouseEV_LeftDown, 0, 0, 0, 0);

                Thread.Sleep(set_time);
                mouse_event(MouseEV_LeftUp, 0, 0, 0, 0);
                stoppeing_event_.WaitOne(100);
            }
            catch (Exception e)
            {
                Console.WriteLine("mcn");
                Console.WriteLine(e.Message, ToString());
            }
        }
        public void MouseSetPosNclick2(int x, int y)
        {
            try
            {
                Random rx = new Random();
                Random ry = new Random();
                int locx = rx.Next(-15, 15);
                int locy = ry.Next(-15, 15);
                SetCursorPos(x + locx, y + locy);
                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(50, 150); //new
                Thread.Sleep(Set_Rand_Time);

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
                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(50, 150); //new
                Thread.Sleep(Set_Rand_Time);
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
                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(100, 150); //new
                Thread.Sleep(Set_Rand_Time);
                mouse_event(MouseEV_LeftUp, 0, 0, 0, 0);
                stoppeing_event_.WaitOne(100);
            }
            catch (Exception e)
            {
                Console.WriteLine("mcn");
                Console.WriteLine(e.Message, ToString());
            }
        }
        public void MouseClick_now_right()
        {
            try
            {
                mouse_event(MouseEV_RightDown, 0, 0, 0, 0);
                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(100, 150); //new
                Thread.Sleep(Set_Rand_Time);
                mouse_event(MouseEV_RightUp, 0, 0, 0, 0);
                stoppeing_event_.WaitOne(100);
            }
            catch (Exception e)
            {
                Console.WriteLine("mcn");
                Console.WriteLine(e.Message, ToString());
            }
        }
        public void MouseClick_right_now()
        {
            try
            {
                mouse_event(MouseEV_LeftDown, 0, 0, 0, 0);
                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(100, 150); //new
                Thread.Sleep(Set_Rand_Time);
                mouse_event(MouseEV_LeftUp, 0, 0, 0, 0);
                stoppeing_event_.WaitOne(100);
            }
            catch (Exception e)
            {
                Console.WriteLine("mcn");
                Console.WriteLine(e.Message, ToString());
            }
        }
        public void Process_Kill(String pro)
        {
            Process[] processList = Process.GetProcessesByName(pro);
            try
            {
                if (processList.Length > 1)
                {
                    processList[1].Kill();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("pk");

                Console.WriteLine(e.Message, ToString());
            }
        }
        public void Process_Kill2(String pro)
        {
            Process[] processList = Process.GetProcessesByName(pro);
            try
            {
                if (processList.Length > 0)
                {
                    for (int i = 0; i < processList.Length; i++)
                    {
                        processList[i].Kill();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("pk");
                Console.WriteLine(e.Message, ToString());
            }
        }
        public void Set_Com_Num()
        {
            IPHostEntry IPHost = Dns.GetHostByName(Dns.GetHostName());
            string myip = IPHost.AddressList[0].ToString();

            if (myip.Contains("114.202.76."))
            {
                myip = myip.Replace("114.202.76.", "");
                myRealIp = myip;
                //myip = "1-" + myip;
                //Pc_Num = myip;
                Pc_number_check = Convert.ToInt16(myRealIp);
                int tempmyip = Convert.ToInt16(myip);

                string[] targetPcnum;
                try
                {
                    targetPcnum = File.ReadAllLines(@"d:\target_pc_num.txt");
                    targetIpInfo = targetPcnum[0];
                    WindowHandler = "114.202.76." + targetPcnum[0] + ":17233: 원격 데스크톱 연결";
                    myip = "1-" + targetPcnum[0];
                    Pc_Num = myip;
                }
                catch (Exception e)
                {
                    MessageBox.Show("D 드라이브에 target_pc_num을 가진 txt 파일을 생성후 조종할 컴퓨터를 적어주세요. ex)62번컴 조종시 62 입력");
                    targetIp = tempmyip;
                    WindowHandler = "114.202.76." + tempmyip + ":17233: 원격 데스크톱 연결";
                    myip = "1-" + tempmyip;
                    Pc_Num = myip;
                }

            }
            else if (myip.Contains("123.212.128."))
            {
                myip = myip.Replace("123.212.128.", "");
                myRealIp = myip;
                myip = "2-" + myip;
                Pc_Num = myip;
            }
            else if (myip.Contains("58.224.41."))
            {
                myip = myip.Replace("58.224.41.", "");
                myRealIp = myip;
                myip = "2-" + myip;
                Pc_Num = myip;
            }
            else if (myip.Contains("121.183.127."))
            {


                myip = myip.Replace("121.183.127.", "");
                myRealIp = myip;
                Pc_number_check = Convert.ToInt16(myRealIp);
                int tempmyip = Convert.ToInt16(myip);

                string[] targetPcnum;
                try
                {
                    targetPcnum = File.ReadAllLines(@"d:\target_pc_num.txt");
                    WindowHandler = "121.183.127." + targetPcnum[0] + ":17233: 원격 데스크톱 연결";
                    myip = "3-" + targetPcnum[0];
                    Pc_Num = myip;
                }
                catch (Exception e)
                {
                    MessageBox.Show("D 드라이브에 target_pc_num을 가진 txt 파일을 생성후 조종할 컴퓨터를 적어주세요. ex)62번컴 조종시 62 입력");
                    targetIp = tempmyip;
                    WindowHandler = "121.183.127." + tempmyip + ":17233: 원격 데스크톱 연결";
                    myip = "3-" + tempmyip;
                    Pc_Num = myip;
                }
            }
            else
            {
                WindowHandler = myip + ":17233: 원격 데스크톱 연결";
                Console.WriteLine(WindowHandler);
                myip = "1-300"; Pc_Num = "1-300";
            }
        }
        public int Get_Com_Num()
        {
            IPHostEntry IPHost = Dns.GetHostByName(Dns.GetHostName());
            string myip = IPHost.AddressList[0].ToString();

            if (myip.Contains("114.202.76."))
            {
                return 1;
            }
            else if (myip.Contains("123.212.128."))
            {
                return 2;
            }
            else if (myip.Contains("58.224.41."))
            {
                return 2;
            }
            else if (myip.Contains("121.183.127."))
            {
                return 1; // 3호점인데 1호점과 경로가 같음
            }
            else
            {
                return 3;
            }
        }

        // DB 시작
        public void Get_ABS_DB(string table_name)
        {
            try
            {
                MySqlConnection conn;
                string strconn = "Server = 121.150.162.200; database = maruplaydb;uid = maru; pwd = 1234;";
                Set_Com_Num();
                conn = new MySqlConnection(strconn);
                conn.Open();
                string select_query = "select game_server,id,pw,login_type,2pw,cus_code,work_time,ord_num,work_hour,work_min,work_date,try_count,web_reward,director_mode,top_class,coupon,subs_desc,team_name,token,memberNick  from " + table_name + " where pc_num = '" + Pc_Num + "'";
                MySqlCommand command = new MySqlCommand(select_query, conn); MySqlDataReader table = command.ExecuteReader();
                while (table.Read())
                {
                    ABS_Game_Server = (String)table["game_server"];
                    ABS_Name = (String)table["cus_code"];
                    ABS_Id = (String)table["id"];
                    ABS_Pw = (String)table["pw"];
                    ABS_Login_Type = (String)table["login_type"];
                    ABS_2pw = (String)table["2pw"];
                    ABS_Work_Time = Convert.ToInt32(table["work_time"]);
                    ABS_Work_Hour = (String)(table["work_hour"]);
                    ABS_Work_Min = (String)(table["work_min"]);
                    ABS_Work_Date = (String)(table["work_date"]);
                    ABS_Order_Num = Convert.ToInt64(table["ord_num"]);
                    ABS_Try_Count = Convert.ToInt32(table["try_count"]);
                    Web_Reward_Switch = Convert.ToInt32(table["web_reward"]);
                    Director_Mode = Convert.ToInt32(table["director_mode"]);
                    top_class = (String)(table["top_class"]);
                    coupon = (String)(table["coupon"]);
                    subs_desc = (String)(table["subs_desc"]);
                    team_name = (String)(table["team_name"]);
                    token = (String)(table["token"]);
                    memberNick = (String)(table["memberNick"]);
                    Console.WriteLine(ABS_Name);
                    Console.WriteLine(ABS_Id);
                    Console.WriteLine(ABS_Pw);
                    Console.WriteLine(ABS_2pw);
                }
                //ABS_Pw = ABS_Pw.Replace("^", "{^}");
                //ABS_Pw = ABS_Pw.Replace("%", "{%}");
                //ABS_Pw = ABS_Pw.Replace("+", "{+}");

                table.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("gdd");
                Console.WriteLine(ex.Message, ToString());
            }
        }
        public void Send_Msg_DB(string msg, char state)
        {
            try
            {
                MySqlConnection conn;
                string strconn = "Server = 121.150.162.200; database = maruplaydb;uid = maru; pwd = 1234;;";
                conn = new MySqlConnection(strconn);
                conn.Open();
                string select_query = "UPDATE msg_new SET stamp = now(), cus_code='" + ABS_Name + "', id='" + ABS_Id + "',state='" + state + "',msg='" + msg + "' WHERE pc_num = '" + Pc_Num + "';";
                MySqlCommand command = new MySqlCommand(select_query, conn); MySqlDataReader table = command.ExecuteReader(); conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("smd");
                Console.WriteLine(ex.Message, ToString());
            }
        }
        public void Send_Msg_State_DB(string msg)
        {
            try
            {
                MySqlConnection conn;
                string strconn = "Server = 121.150.162.200; database = maruplaydb;uid = maru; pwd = 1234;";
                conn = new MySqlConnection(strconn);
                conn.Open();
                string select_query = "UPDATE msg_new SET stamp = now(), cus_code='" + ABS_Name + "', id='" + ABS_Id + "'," + msg + " WHERE pc_num = '" + Pc_Num + "';";
                MySqlCommand command = new MySqlCommand(select_query, conn); MySqlDataReader table = command.ExecuteReader(); conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("smsd");
                Console.WriteLine(ex.Message, ToString());
            }
        }
        public void Delete_DB(string query)
        {
            try
            {
                MySqlConnection conn;
                string strconn = "Server = 121.150.162.200; database = maruplaydb;uid = maru; pwd = 1234;";
                conn = new MySqlConnection(strconn);
                conn.Open();
                string select_query = query;
                MySqlCommand command = new MySqlCommand(select_query, conn); MySqlDataReader table = command.ExecuteReader(); conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("dd");
                Console.WriteLine(ex.Message, ToString());
            }
        }
        public void Update_DB(string query)
        {
            try
            {
                MySqlConnection conn;
                string strconn = "Server = 121.150.162.200; database = maruplaydb;uid = maru; pwd = 1234;";
                conn = new MySqlConnection(strconn);
                conn.Open();
                string select_query = query;
                MySqlCommand command = new MySqlCommand(select_query, conn); MySqlDataReader table = command.ExecuteReader(); conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ud");
                Console.WriteLine(ex.Message, ToString());
            }
        }
        public void Get_Ready_DB()
        {
            try
            {
                MySqlConnection conn;
                string strconn = "Server = 121.150.162.200; database = maruplaydb;uid = maru; pwd = 1234;;";

                conn = new MySqlConnection(strconn);
                conn.Open();
                string select_query = "select switch,game_type,ord_num from ready_user_table where pc_num ='" + Pc_Num + "'";
                MySqlCommand command = new MySqlCommand(select_query, conn); MySqlDataReader table = command.ExecuteReader();
                while (table.Read())
                {
                    ABS_Game_Name = (String)table["game_type"];
                    DB_Switch = Convert.ToInt32(table["switch"]);
                    ABS_Order_Num = Convert.ToInt64(table["ord_num"]);
                }
                table.Close();
                conn.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine("grd");
                Console.WriteLine(ex.Message, ToString());
            }
        }
        public void Get_Start_DB()
        {
            try
            {
                MySqlConnection conn;
                string strconn = "Server = 121.150.162.200; database = maruplaydb;uid = maru; pwd = 1234;;";
                conn = new MySqlConnection(strconn);
                conn.Open();
                string select_query = "select switch,game_type,ord_num from start_user_table where pc_num ='" + Pc_Num + "'";
                MySqlCommand command = new MySqlCommand(select_query, conn); MySqlDataReader table = command.ExecuteReader();
                while (table.Read())
                {
                    ABS_Game_Name = (String)table["game_type"];
                    DB_Switch = Convert.ToInt32(table["switch"]);
                    ABS_Order_Num = Convert.ToInt64(table["ord_num"]);
                }
                table.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("gsd");
                Console.WriteLine(ex.Message, ToString());
            }
        }
        public void Get_Waiting_DB(string query)
        {
            try
            {
                MySqlConnection conn;
                string strconn = "Server = 121.150.162.200; database = maruplaydb;uid = maru; pwd = 1234;;";
                conn = new MySqlConnection(strconn);
                conn.Open();
                string select_query = query;
                MySqlCommand command = new MySqlCommand(select_query, conn); MySqlDataReader table = command.ExecuteReader();
                while (table.Read())
                {
                    ABS_Order_Num = Convert.ToInt64(table["ord_num"]);
                }
                table.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("gwd");
                Console.WriteLine(ex.Message, ToString());
            }
        }
        public void Get_Waiting_DB2(string query)
        {
            try
            {
                MySqlConnection conn;
                string strconn = "Server = 121.150.162.200; database = maruplaydb;uid = maru; pwd = 1234;;";
                conn = new MySqlConnection(strconn);
                conn.Open();
                string select_query = query;
                MySqlCommand command = new MySqlCommand(select_query, conn); MySqlDataReader table = command.ExecuteReader();
                while (table.Read())
                {
                    ABS_Work_Date = (String)table["work_date"];
                }
                table.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("gwd2");
                Console.WriteLine(ex.Message, ToString());
            }
        }
        public void ProccessFifaKill()
        {
            Console.WriteLine("원격 프로세스 시작");
            try
            {
                ProcessStartInfo cmd = new ProcessStartInfo();
                Process process = new Process();
                cmd.FileName = @"cmd";
                //cmd.WindowStyle = ProcessWindowStyle.Hidden;             // cmd창이 숨겨지도록 하기
                cmd.CreateNoWindow = true;                               // cmd창을 띄우지 안도록 하기

                cmd.UseShellExecute = false;
                cmd.RedirectStandardOutput = true;        // cmd창에서 데이터를 가져오기
                cmd.RedirectStandardInput = true;          // cmd창으로 데이터 보내기
                cmd.RedirectStandardError = true;          // cmd창에서 오류 내용 가져오기

                process.StartInfo = cmd;
                process.Start();

                //process.StandardInput.Write(@"psexec -i 1 -d -accepteula \\114.202.76.71 -u Administrator -p withmaru!@34 d:\subs\subs.exe" + Environment.NewLine);
                //process.StandardInput.Write(@"PsList \\114.202.76.71  -u Administrator -p withmaru!@34" + Environment.NewLine);
                process.StandardInput.Write(@"PsKill \\114.202.76." + targetIpInfo + " -accepteula -u Administrator -p withmaru!@34 fifa4zf" + Environment.NewLine);
                // 명령어를 보낼때는 꼭 마무리를 해줘야 한다. 그래서 마지막에 NewLine가 필요하다
                process.StandardInput.Close();

                process.WaitForExit();
                process.Close();


                //fifa4zf
                //fifa4launcher
                //BlackCipher64.aes
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("원격 프로세스 완료");
        }
        public void ProccessSubs()
        {
            try
            {
                ProcessStartInfo cmd = new ProcessStartInfo();
                Process process = new Process();
                cmd.FileName = @"cmd";
                //cmd.WindowStyle = ProcessWindowStyle.Hidden;             // cmd창이 숨겨지도록 하기
                cmd.CreateNoWindow = true;                               // cmd창을 띄우지 안도록 하기

                cmd.UseShellExecute = false;
                cmd.RedirectStandardOutput = true;        // cmd창에서 데이터를 가져오기
                cmd.RedirectStandardInput = true;          // cmd창으로 데이터 보내기
                cmd.RedirectStandardError = true;          // cmd창에서 오류 내용 가져오기

                process.StartInfo = cmd;
                process.Start();

                process.StandardInput.Write(@"psexec -i 1 -d -accepteula \\114.202.76." + targetIpInfo + " -u Administrator -p withmaru!@34 d:\\subs\\subs.exe" + Environment.NewLine);
                //process.StandardInput.Write(@"PsList \\114.202.76.71  -u Administrator -p withmaru!@34" + Environment.NewLine);
                //process.StandardInput.Write(@"PsKill \\114.202.76.71 -accepteula -u Administrator -p withmaru!@34 fifa4zf" + Environment.NewLine);
                // 명령어를 보낼때는 꼭 마무리를 해줘야 한다. 그래서 마지막에 NewLine가 필요하다
                process.StandardInput.Close();

                process.WaitForExit();
                process.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("원격 프로세스 완료");

        }
        public int ProccessCheck()
        {
            try
            {
                ProcessStartInfo cmd = new ProcessStartInfo();
                Process process = new Process();
                cmd.FileName = @"cmd";
                //cmd.WindowStyle = ProcessWindowStyle.Hidden;             // cmd창이 숨겨지도록 하기
                cmd.CreateNoWindow = true;                               // cmd창을 띄우지 안도록 하기

                cmd.UseShellExecute = false;
                cmd.RedirectStandardOutput = true;        // cmd창에서 데이터를 가져오기
                cmd.RedirectStandardInput = true;          // cmd창으로 데이터 보내기
                cmd.RedirectStandardError = true;          // cmd창에서 오류 내용 가져오기

                process.StartInfo = cmd;
                process.Start();

                //process.StandardInput.Write(@"psexec -i 1 -d -accepteula \\114.202.76." + targetIpInfo + " -u Administrator -p withmaru!@34 d:\\subs\\subs.exe" + Environment.NewLine);
                process.StandardInput.Write(@"PsList \\114.202.76." + targetIpInfo + "  -u Administrator -p withmaru!@34" + Environment.NewLine);
                //process.StandardInput.Write(@"PsKill \\114.202.76.71 -accepteula -u Administrator -p withmaru!@34 fifa4zf" + Environment.NewLine);
                // 명령어를 보낼때는 꼭 마무리를 해줘야 한다. 그래서 마지막에 NewLine가 필요하다
                process.StandardInput.Close();
                String result = process.StandardOutput.ReadToEnd();

                process.WaitForExit();
                process.Close();
                Console.WriteLine(result);
                return result.IndexOf("fifa4zf");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return -1;
            }


        }

        public Discord()
        {

            rand_watitng(100);
            Process_Kill("subsStart");

            InitializeComponent();
            rand_watitng(100);
            Set_Com_Num();
            try
            {
                IntPtr findwindow2 = FindWindow(null, WindowHandler);
                BringWindowToTop(findwindow2);
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
            TrackWorkthread_Get_User_Data = new Thread(new ThreadStart(Get_User_Data));
            TrackWorkthread_Get_User_Data.Start();
        }
        public void Get_User_Data() //시작
        {
        Watiting:
            Start_Check();


            if (Start_Check_Switch == 1)
            {
                goto subsStart;
            }
            Thread.Sleep(20000);
            //



            goto Watiting;

        subsStart:

            Get_ABS_DB("start_user_table");

            gt_fls();
        }

        public void Start_Check()
        {
            Get_Start_DB();
            if (ABS_Game_Name == "피파")
            {
                Start_Check_Switch = 1;
                return;
            }
        }


        public void gt_fls() // 매크로, 타이머 작동시작
        {

            TrackWorkthread_FIFA = new Thread(new ThreadStart(gt_fms));
            TrackWorkthread_FIFA.ApartmentState = ApartmentState.STA;
            TrackWorkthread_FIFA.Start();
        }
        public void gt_fms() //접속 시작
        {
            var x = Screen.PrimaryScreen.Bounds.Width;
            var y = Screen.PrimaryScreen.Bounds.Height;
            if (x != 1920 && y != 1080)
            {
                //Send_Msg_DB("3", 'B');
                MessageBox.Show("화면문제");
            }
            Random Rand_Time = new Random();
            int Set_Rand_Time = Rand_Time.Next(0, 1000);
            Thread.Sleep(Set_Rand_Time);

        ABS_On:
            Voice_Login();
            Voice_Play();


        }
        public void Voice_Login()
        {
        Voice_Login:
            //SetCursorPos(5, 35);
            Thread.Sleep(200);

            Ingame_Check();

            Thread.Sleep(200);

            Access_Done();
            if (Voice_Login_Switch == 1)
            {
                return;
            }
            Matching_2pw();

            Thread.Sleep(200);

            Launch_Check();
            goto Voice_Login;
        }
        public void Voice_Play()
        {
        //Send_Msg_DB("4", 'D');
        Voice_Play_Check:
            Thread.Sleep(1000);
            League_Check();
            Select_Team();
            goto Voice_Play_Check;
        }

        public void League_Check() // 대기실 최종
        {
            Send_Msg_DB("접속 완료", 'D');
            Thread.Sleep(1000);
            Image_Match_Click("탑클 취소", "\\subs\\", WindowHandler, 0, 0, 0, 0);
            int League_Check1 = Image_Match("vplc1", "\\Voice_Play\\League_Check\\", WindowHandler, 0, 0, 0, 0);
            int League_Check2 = Image_Match("vplc2", "\\Voice_Play\\League_Check\\", WindowHandler, 0, 0, 0, 0);
            int League_Check3 = Image_Match("vplc3", "\\Voice_Play\\League_Check\\", WindowHandler, 0, 0, 0, 0);
            if (League_Check1 == 44 || League_Check2 == 44 || League_Check3 == 44)
            {
                if (coupon == "ok" && subs_desc == "<<== 쿠폰 비싼 순서대로 모두 사용 ==>>")
                {
                    Thread.Sleep(1000);
                    InClick(810, 1012);
                    Thread.Sleep(2000);
                    InClick(989, 190);
                    Thread.Sleep(1000);

                    Send_Msg_DB("자동대낙 실행중", 'D');
                    autoSubsMain();
                    if (finalCheck == 1)
                    {
                        Send_Msg_DB("수령완료", 'D');
                        subsOkRequest(ABS_Order_Num.ToString(), memberNick, token);
                        ProccessFifaKill();
                    }

                }
                else if (subs_desc == "===>>  1분 접속만  <<===" || ABS_Name.IndexOf("1분") != -1)
                {
                    Send_Msg_DB("1분접속 대기중", 'D');
                    Thread.Sleep(65000);
                    Send_Msg_DB("1분접속 완료", 'D');
                    if (subs_desc == "===>>  1분 접속만  <<===")
                    {
                        subsOkRequest(ABS_Order_Num.ToString(), memberNick, token);
                    }
                    ProccessFifaKill();

                }
                else if (ABS_Name.IndexOf("누적") != -1)
                {
                    int time = 0;
                loop1:
                    Send_Msg_DB(time + "분 접속중 / 총 접속시간" + ABS_Work_Time + "분", 'D');
                    if (ProccessCheck() == -1)
                    {
                        Send_Msg_DB("피파 꺼짐 발견", 'A');
                        MessageBox.Show("피파 꺼짐 발견");
                    }

                    else
                    {
                        Thread.Sleep(60000);
                        time += 1;
                        Console.WriteLine(time + "분 접속중 / 총 접속시간" + ABS_Work_Time + "분");
                        if (time >= ABS_Work_Time)
                        {
                            Send_Msg_DB("접속 완료 및 피파 종료", 'D');
                            ProccessFifaKill();
                            goto subsStartEnd;
                        }
                    }
                    goto loop1;
                }
                else if (ABS_Name.IndexOf("감독") != -1)
                {
                    int time = 0;
                    //감독모드 실행
                    Thread.Sleep(1000);
                    InClick(462, 355);
                    Thread.Sleep(1000);
                    InClick(1539, 925);
                    Thread.Sleep(2000);
                    InClick(1349, 995);
                gameCheck:
                    Thread.Sleep(2000);
                    if (Image_Match("임대선수", "\\subs\\", WindowHandler, 0, 0, 0, 0) == 44)
                    {
                        Send_Msg_DB("임대선수 문제", 'A');
                        MessageBox.Show("임대선수 문제");
                    }

                    if (Image_Match("진행", "\\subs\\", WindowHandler, 0, 0, 0, 0) == 44)//1425 912 1689 1021
                    {
                        InClick(1349, 995);
                        Thread.Sleep(2000);
                        goto gameCheck;
                    }


                loop1:
                    Send_Msg_DB(time + "분 접속중 / 총 접속시간 /" + ABS_Work_Time + "분 / 감독모드 진행중", 'D');
                    if (ProccessCheck() == -1)
                    {
                        Send_Msg_DB("피파 꺼짐 발견", 'A');
                        MessageBox.Show("피파 꺼짐 발견");
                    }

                    else
                    {
                        Thread.Sleep(60000);
                        time += 1;
                        Console.WriteLine(time + "분 접속중 / 총 접속시간" + ABS_Work_Time + "분");
                        if (time >= ABS_Work_Time)
                        {
                            Send_Msg_DB("접속 완료 및 피파 종료", 'D');
                            ProccessFifaKill();
                            goto subsStartEnd;
                        }
                    }
                    goto loop1;
                }
                else if (coupon == "no" && subs_desc != "===>>  1분 접속만  <<===" && subs_desc != "<<== 쿠폰 비싼 순서대로 모두 사용 ==>>" && subs_desc == "")
                {
                    Thread.Sleep(1000);
                    InClick(810, 1012);
                    Thread.Sleep(2000);
                    InClick(989, 190);
                    Thread.Sleep(1000);

                    Thread.Sleep(1000);
                    Image_Match_Click("모두받기", "\\subs\\", WindowHandler, 0, 0, 0, 0);
                    Thread.Sleep(1000);
                lastCheck:
                    Image_Match_Click("최종수령_확인버튼", "\\subs\\", WindowHandler, 1201, 850, 205, 60);
                    Thread.Sleep(1000);
                    if (Image_Match("최종수령_확인버튼", "\\subs\\", WindowHandler, 1201, 850, 205, 60) == 44)
                    {
                        Thread.Sleep(1000);
                        goto lastCheck;
                    }
                    // ajax로 fifaro에 완료 보내고
                    subsOkRequest(ABS_Order_Num.ToString(), memberNick, token);
                    ProccessFifaKill();

                }
                else
                {
                    Thread.Sleep(1000);
                    InClick(810, 1012);
                    Thread.Sleep(2000);
                    InClick(989, 190);
                    Thread.Sleep(1000);
                }

            subsStartEnd:

                DB_Delete();
                ProccessSubs();
                Process.Start("C:/Start_WMM1.exe");
                Process_Kill2("subsStart");
            }
        }
        public void Select_Team() // 감독선택
        {
            int Select_Team1 = Image_Match("vpst1", "\\Voice_Play\\Select_Team\\", WindowHandler, 279, 66, 39, 67);
            int Select_Team2 = Image_Match("vpst2", "\\Voice_Play\\Select_Team\\", WindowHandler, 329, 539, 88, 58);
            if (Select_Team1 == 44 || Select_Team2 == 44)
            {
                Process_Kill2("msedge");

                Random Rand_Time = new Random();
                int Set_Rand_Time = Rand_Time.Next(100, 300); //new
                Thread.Sleep(Set_Rand_Time);

                Random Rand_Act = new Random();
                int Set_Rand_Act = Rand_Act.Next(3, 10); //new
                for (int i = 0; i < Set_Rand_Act; i++)
                {
                    Rand_Click_select();
                    Set_Rand_Time = Rand_Time.Next(30, 200); //new
                    Thread.Sleep(Set_Rand_Time);
                }

                InClick(1300, 740);
            }
        }
        public void Ingame_Check()
        {
            int Ingame_Check;
            Ingame_Check = Image_Match("vplc1", "\\Voice_Play\\League_Check\\", WindowHandler, 0, 0, 0, 0);
            if (Ingame_Check == 44) { Voice_Login_Switch = 1; Timer_Play_Check_Switch = 1; return; }
            Ingame_Check = Image_Match("vplc2", "\\Voice_Play\\League_Check\\", WindowHandler, 0, 0, 0, 0);
            if (Ingame_Check == 44) { Voice_Login_Switch = 1; Timer_Play_Check_Switch = 1; return; }
            Ingame_Check = Image_Match("vplc3", "\\Voice_Play\\League_Check\\", WindowHandler, 0, 0, 0, 0);
            if (Ingame_Check == 44) { Voice_Login_Switch = 1; Timer_Play_Check_Switch = 1; return; }
            Ingame_Check = Image_Match("vpst1", "\\Voice_Play\\Select_Team\\", WindowHandler, 279, 66, 39, 67);
            if (Ingame_Check == 44) { Voice_Login_Switch = 1; Timer_Play_Check_Switch = 1; return; }
            Ingame_Check = Image_Match("vpst2", "\\Voice_Play\\Select_Team\\", WindowHandler, 329, 539, 88, 58);
            if (Ingame_Check == 44) { Voice_Login_Switch = 1; Timer_Play_Check_Switch = 1; return; }
            Ingame_Check = Image_Match("vpst2", "\\Voice_Play\\Select_Team\\", WindowHandler, 329, 539, 88, 58);
            if (Ingame_Check == 44) { Voice_Login_Switch = 1; Timer_Play_Check_Switch = 1; return; }
        }
        public void Access_Done()// 공지사항
        {
            if (Image_Match("acdo1", "\\Access_Done\\", WindowHandler, 0, 0, 0, 0) == 44)
            {
                InClick(1380, 150);
                Send_Msg_DB("12", 'D');
                Voice_Login_Switch = 1;
            }
        }
        public void Matching_2pw()
        {
            if (Image_Match("mase1", "\\Matching_2pw\\", WindowHandler, 0, 0, 0, 0) == 44)
            {
                Send_Msg_DB("13", 'C');
                Thread.Sleep(1000);

                try
                {
                    First_2PW = Convert.ToInt32(ABS_2pw.Substring(0, 1));
                    Second_2PW = Convert.ToInt32(ABS_2pw.Substring(1, 1));
                    Third_2PW = Convert.ToInt32(ABS_2pw.Substring(2, 1));
                    Fourth_2PW = Convert.ToInt32(ABS_2pw.Substring(3, 1));
                Matching_2PW_Start:
                    Matching_2PW_Start();
                    if (Matching_2PW_Reset_Switch == 0)
                    {
                        Image_Match_Click("mase5", "\\Matching_2PW\\", WindowHandler, 0, 0, 0, 0);
                        rand_watitng(500);
                        int Matching_2pw1 = Image_Match("mase4", "\\Matching_2PW\\", WindowHandler, 0, 0, 0, 0);
                        int Matching_2pw2 = Image_Match("mase3", "\\Matching_2PW\\", WindowHandler, 0, 0, 0, 0);
                        int Matching_2pw3 = Image_Match("mase6", "\\Matching_2PW\\", WindowHandler, 0, 0, 0, 0);
                        if (Matching_2pw1 == 44 || Matching_2pw2 == 44)
                        {
                            Send_Msg_DB("14", 'A');
                            MessageBox.Show("2PEX");
                            return;
                        }
                        if (Matching_2pw3 == 44)
                        {
                            Send_Msg_DB("5", 'B');
                            MessageBox.Show("중복로그인...");


                            // 크롬, 피파, 등등 다 끄고 접속매크로 다시 동작

                            return;
                        }
                        return;
                    }
                    else if (Matching_2PW_Reset_Switch == 1)
                    {
                        Reset_2PW();
                        Matching_2PW_Reset_Switch = 0;
                    }

                    goto Matching_2PW_Start;
                }
                catch (Exception e)
                {
                    Console.WriteLine("asd");
                    Console.WriteLine(e);
                }


            }
        }
        public void Matching_2PW_Start()
        {

            for (int w = 1; w < 12; w++)
            {
                First_2PW_Switch = Image_Match_2pw(First_2PW + " 소 " + w, "\\Matching_2PW\\" + First_2PW + "\\" + First_2PW + " 소\\", WindowHandler);
                if (First_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                First_2PW_Switch = Image_Match_2pw(First_2PW + " 중 " + w, "\\Matching_2PW\\" + First_2PW + "\\" + First_2PW + " 중\\", WindowHandler);
                if (First_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                First_2PW_Switch = Image_Match_2pw(First_2PW + " 대 " + w, "\\Matching_2PW\\" + First_2PW + "\\" + First_2PW + " 대\\", WindowHandler);
                if (First_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                First_2PW_Switch = Image_Match_2pw(First_2PW + " 특대 " + w, "\\Matching_2PW\\" + First_2PW + "\\" + First_2PW + " 특대\\", WindowHandler);
                if (First_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                First_2PW_Switch = Image_Match_2pw(First_2PW + " 중대 " + w, "\\Matching_2PW\\" + First_2PW + "\\" + First_2PW + " 중대\\", WindowHandler);
                if (First_2PW_Switch == 77) { SetCursorPos(5, 35); break; }


            }
            if (First_2PW_Switch == 21)
            {
                Matching_2PW_Reset_Switch = 1;
                return;
            }

            for (int q = 1; q < 12; q++)
            {
                Second_2PW_Switch = Image_Match_2pw(Second_2PW + " 소 " + q, "\\Matching_2PW\\" + Second_2PW + "\\" + Second_2PW + " 소\\", WindowHandler);
                if (Second_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                Second_2PW_Switch = Image_Match_2pw(Second_2PW + " 중 " + q, "\\Matching_2PW\\" + Second_2PW + "\\" + Second_2PW + " 중\\", WindowHandler);
                if (Second_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                Second_2PW_Switch = Image_Match_2pw(Second_2PW + " 대 " + q, "\\Matching_2PW\\" + Second_2PW + "\\" + Second_2PW + " 대\\", WindowHandler);
                if (Second_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                Second_2PW_Switch = Image_Match_2pw(Second_2PW + " 특대 " + q, "\\Matching_2PW\\" + Second_2PW + "\\" + Second_2PW + " 특대\\", WindowHandler);
                if (Second_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                Second_2PW_Switch = Image_Match_2pw(Second_2PW + " 중대 " + q, "\\Matching_2PW\\" + Second_2PW + "\\" + Second_2PW + " 중대\\", WindowHandler);
                if (Second_2PW_Switch == 77) { SetCursorPos(5, 35); break; }

            }
            if (Second_2PW_Switch == 21)
            {
                Matching_2PW_Reset_Switch = 1;
                return;
            }

            for (int e = 1; e < 12; e++)
            {
                Third_2PW_Switch = Image_Match_2pw(Third_2PW + " 소 " + e, "\\Matching_2PW\\" + Third_2PW + "\\" + Third_2PW + " 소\\", WindowHandler);
                if (Third_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                Third_2PW_Switch = Image_Match_2pw(Third_2PW + " 중 " + e, "\\Matching_2PW\\" + Third_2PW + "\\" + Third_2PW + " 중\\", WindowHandler);
                if (Third_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                Third_2PW_Switch = Image_Match_2pw(Third_2PW + " 대 " + e, "\\Matching_2PW\\" + Third_2PW + "\\" + Third_2PW + " 대\\", WindowHandler);
                if (Third_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                Third_2PW_Switch = Image_Match_2pw(Third_2PW + " 특대 " + e, "\\Matching_2PW\\" + Third_2PW + "\\" + Third_2PW + " 특대\\", WindowHandler);
                if (Third_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                Third_2PW_Switch = Image_Match_2pw(Third_2PW + " 중대 " + e, "\\Matching_2PW\\" + Third_2PW + "\\" + Third_2PW + " 중대\\", WindowHandler);
                if (Third_2PW_Switch == 77) { SetCursorPos(5, 35); break; }

            }
            if (Third_2PW_Switch == 21)
            {
                Matching_2PW_Reset_Switch = 1;
                return;
            }

            for (int r = 1; r < 12; r++)
            {
                Fourth_2PW_Switch = Image_Match_2pw(Fourth_2PW + " 소 " + r, "\\Matching_2PW\\" + Fourth_2PW + "\\" + Fourth_2PW + " 소\\", WindowHandler);
                if (Fourth_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                Fourth_2PW_Switch = Image_Match_2pw(Fourth_2PW + " 중 " + r, "\\Matching_2PW\\" + Fourth_2PW + "\\" + Fourth_2PW + " 중\\", WindowHandler);
                if (Fourth_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                Fourth_2PW_Switch = Image_Match_2pw(Fourth_2PW + " 대 " + r, "\\Matching_2PW\\" + Fourth_2PW + "\\" + Fourth_2PW + " 대\\", WindowHandler);
                if (Fourth_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                Fourth_2PW_Switch = Image_Match_2pw(Fourth_2PW + " 특대 " + r, "\\Matching_2PW\\" + Fourth_2PW + "\\" + Fourth_2PW + " 특대\\", WindowHandler);
                if (Fourth_2PW_Switch == 77) { SetCursorPos(5, 35); break; }
                Fourth_2PW_Switch = Image_Match_2pw(Fourth_2PW + " 중대 " + r, "\\Matching_2PW\\" + Fourth_2PW + "\\" + Fourth_2PW + " 중대\\", WindowHandler);
                if (Fourth_2PW_Switch == 77) { SetCursorPos(5, 35); break; }

            }
            if (Fourth_2PW_Switch == 21)
            {
                Matching_2PW_Reset_Switch = 1;
                return;
            }
            Matching_2PW_Reset_Switch = 0;
        }
        public void Reset_2PW()
        {
            //Image_Match("mase2", "\\"+fn1_6+"\\", WindowHandler, 0, 0, 0, 0); // 매개변수가 잘못되어서 이미지를 못참음
            Random Rand_Time = new Random();
            int Set_Rand_Time = Rand_Time.Next(1000, 2000); //new
            Thread.Sleep(Set_Rand_Time);

            InClick(1277, 464);
            SetCursorPos(5, 35);
        }
        public void Launch_Check()//
        {

            if (Image_Match("fifa_check2", "\\Access_Check\\", WindowHandler, 0, 0, 0, 0) == 44 && Image_Match("fifa_check_size", "\\Access_Check\\", WindowHandler, 0, 0, 0, 0) == 44)
            {

            }
            else
            {
                if (Image_Match("fifa_check1", "\\Access_Check\\", WindowHandler, 0, 0, 0, 0) == 44 && Image_Match("fifa_check_size", "\\Access_Check\\", WindowHandler, 0, 0, 0, 0) == 44)
                {
                    Image_Match_Click("fifa_size_target", "\\Access_Check\\", WindowHandler, 0, 0, 0, 0);
                }
            }




            if (Image_Match("acch1", "\\Access_Check\\", WindowHandler, 0, 0, 0, 0) == 44)
            {

                rand_watitng(100);

                InClick(700, 700);
            }

            if (Image_Match("acch2", "\\Access_Check\\", WindowHandler, 0, 0, 0, 0) == 44)
            {
                if (Launch_Error_switch == 1)
                {
                }
                else
                {
                    Launch_Error_switch = 1;
                    Image_Match_Click("erch3", "\\Error_Check\\", WindowHandler, 0, 0, 0, 0);
                    rand_watitng(5000);
                }
            }

        }

        // 자동 대낙 수령
        int slotCheck1, slotCheck2, slotCheck3, slotCheck4, slotCheck5, slotCheck6, slotCheck7, slotCheck8 = 0;

        int[] slideCheckArr = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] slideCheckTemp = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] slideCheckX = new int[] { 260, 317, 373, 432, 484, 546, 600, 685 };
        int[] slideCheckY = new int[] { 50, 50, 50, 50, 50, 50, 50, 50 };

        int finalCheck = 0;
        int subsErrorCount = 0;
        int subsCheck1, subsCheck2, subsCheck3, subsCheck4 = 0;
        int subsReadyCheck1 = 0;
        int subsReadyCheck2 = 0;
        int subsEndCheck = 0;

        void subsOkRequest(string subsNumber, string memberNick, string token)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://withmaru-api.com/admin/stead/ok");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Headers.Add("Authorization:" + token);

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"subsNumber\":\"" + subsNumber + "\"," +
                        "\"memberNickname\":\"" + memberNick + "\"}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Console.WriteLine(result.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }



        public void autoSubsMain()
        {



            Console.WriteLine("자동 대낙 시작");
        firstSetting:
            Image_Match_Click("모두받기", "\\subs\\", WindowHandler, 1395, 885, 243, 95);//1395 885 1638 980
            Thread.Sleep(500);
            Image_Match_Click("거래목록", "\\subs\\", WindowHandler, 0, 0, 0, 0);
            Thread.Sleep(500);
            var check1 = Image_Match("하단화살표", "\\subs\\", WindowHandler, 880, 230, 30, 25);
            var check2 = Image_Match("판매금액화살표", "\\subs\\", WindowHandler, 880, 230, 30, 25);
            var check4 = Image_Match("판매금액화살표2", "\\subs\\", WindowHandler, 880, 230, 30, 25);
            var check3 = Image_Match("화살표", "\\subs\\", WindowHandler, 880, 230, 30, 25);
            if (check1 == 44 || check2 == 44 || check3 == 44 || check4 == 44)
            {
                subsReadyCheck1 = 1;
            }
            else
            {
                Console.WriteLine("판매금액 정렬 확인 실패 클릭");
                InClick_x(860, 244);
            }
            Thread.Sleep(1000);

            if (Image_Match("밝은+0", "\\subs\\", WindowHandler, 1244, 259, 69, 452) == 44 || Image_Match("어두운+0", "\\subs\\", WindowHandler, 1244, 259, 69, 452) == 44) //1244 259 1313 711 탑클확인
            {
                Console.WriteLine("탑클 없음");
                if (top_class == "ok")
                {
                    Console.WriteLine("사용자 탑클 있음 표시되어있어서 문제 발생 작동 정지");
                    MessageBox.Show("사용자 탑클 있음 표시되어있어서 문제 발생 작동 정지");
                    Send_Msg_DB("탑클 없음", 'B');
                    goto subsFinish;
                }
                else
                {
                    subsReadyCheck2 = 1;
                }
            }
            else
            {
                subsReadyCheck2 = 1;
                Console.WriteLine("탑클 있음");
            }
            Thread.Sleep(1000);

            if (subsReadyCheck1 == 1 && subsReadyCheck2 == 1)
            {
                Console.WriteLine("동작 준비 완료");
                Random rx1 = new Random();
                Random ry1 = new Random();
                int locx1 = rx1.Next(-100, 100);
                int locy1 = rx1.Next(-100, 100);
                InClick(1770 + locx1, 431 + locy1);

                if (Image_Match("버튼_개_수정", "\\subs\\", WindowHandler, 1330, 244, 128, 495) != 44 && Image_Match("쿠폰개", "\\subs\\", WindowHandler, 1330, 244, 128, 495) != 44 && Image_Match("버튼_개_수정3", "\\subs\\", WindowHandler, 1330, 244, 128, 495) != 44)
                {
                    Send_Msg_DB("쿠폰이 존재하지 않음", 'B');
                    MessageBox.Show("쿠폰 대낙인데 쿠폰이 존재하지 않음");
                    return;
                }

                goto couponSetting;
            }
            goto firstSetting;

        //시작
        couponSetting:

            if (subsEndCheck == 1)
            {
                goto subsFinish;
            }
            Thread.Sleep(100);
            couponStart();
            Random rx = new Random();
            Random ry = new Random();
            int locx = rx.Next(-100, 100);
            int locy = rx.Next(-100, 100);


            InClick(1770 + locx, 431 + locy);


            if (Image_Match("버튼_개_수정", "\\subs\\", WindowHandler, 1330, 244, 128, 495) == 44 || Image_Match("쿠폰개", "\\subs\\", WindowHandler, 1330, 244, 128, 495) == 44 || Image_Match("버튼_개_수정3", "\\subs\\", WindowHandler, 1330, 244, 128, 495) == 44) //1330 244 1458 739
            {
                Console.WriteLine("남은 쿠폰 확인됨. 재검토");
                goto couponSetting;
            }
            slideCheck();

            goto couponSetting;

        subsFinish:
            subsEnd();


        }
        public void couponStart()// 쿠폰적용
        {
            Random rx = new Random();
            Random ry = new Random();
            int locx = rx.Next(-100, 100);
            int locy = rx.Next(-100, 100);
            InClick(1770 + locx, 431 + locy);


            Thread.Sleep(100);
            if (Image_Match("버튼_흑", "\\subs\\", WindowHandler, 1390, 255, 55, 60) == 44 || Image_Match("버튼_백", "\\subs\\", WindowHandler, 1390, 255, 55, 60) == 44 || Image_Match("쿠폰개", "\\subs\\", WindowHandler, 1385, 270, 40, 30) == 44)
            {
                Console.WriteLine("1번슬롯 발견, 쿠폰적용 실행");
                couponCheck(1386, 287);

            }
            Thread.Sleep(100);
            if (slotCheck1 == 1)
            {
                if (Image_Match("버튼_흑", "\\subs\\", WindowHandler, 1390, 314, 55, 60) == 44 || Image_Match("버튼_백", "\\subs\\", WindowHandler, 1385, 314, 55, 60) == 44 || Image_Match("쿠폰개", "\\subs\\", WindowHandler, 1385, 325, 40, 30) == 44)
                {
                    Console.WriteLine("2번슬롯 발견, 쿠폰적용 실행");
                    couponCheck(1386, 345);
                }
            }
            Thread.Sleep(100);

            if (slotCheck1 == 1 && slotCheck2 == 1)
            {
                if (Image_Match("버튼_흑", "\\subs\\", WindowHandler, 1385, 370, 55, 60) == 44 || Image_Match("버튼_백", "\\subs\\", WindowHandler, 1385, 370, 55, 60) == 44 || Image_Match("쿠폰개", "\\subs\\", WindowHandler, 1385, 382, 40, 30) == 44)
                {
                    Console.WriteLine("3번슬롯 발견, 쿠폰적용 실행");
                    couponCheck(1386, 401);
                }
            }

            Thread.Sleep(100);

            if (slotCheck1 == 1 && slotCheck2 == 1 && slotCheck3 == 1)
            {
                if (Image_Match("버튼_흑", "\\subs\\", WindowHandler, 1385, 424, 55, 60) == 44 || Image_Match("버튼_백", "\\subs\\", WindowHandler, 1385, 424, 55, 60) == 44 || Image_Match("쿠폰개", "\\subs\\", WindowHandler, 1385, 438, 40, 30) == 44)
                {
                    Console.WriteLine("4번슬롯 발견, 쿠폰적용 실행");
                    couponCheck(1386, 458);
                }
            }

            Thread.Sleep(100);

            if (slotCheck1 == 1 && slotCheck2 == 1 && slotCheck3 == 1 && slotCheck4 == 1)
            {
                if (Image_Match("버튼_흑", "\\subs\\", WindowHandler, 1385, 481, 55, 60) == 44 || Image_Match("버튼_백", "\\subs\\", WindowHandler, 1385, 481, 55, 60) == 44 || Image_Match("쿠폰개", "\\subs\\", WindowHandler, 1385, 495, 40, 30) == 44)
                {
                    Console.WriteLine("5번슬롯 발견, 쿠폰적용 실행");
                    couponCheck(1386, 511);
                }
            }

            Thread.Sleep(100);

            if (slotCheck1 == 1 && slotCheck2 == 1 && slotCheck3 == 1 && slotCheck4 == 1 && slotCheck5 == 1)
            {
                if (Image_Match("버튼_흑", "\\subs\\", WindowHandler, 1385, 540, 55, 60) == 44 || Image_Match("버튼_백", "\\subs\\", WindowHandler, 1385, 540, 55, 60) == 44 || Image_Match("쿠폰개", "\\subs\\", WindowHandler, 1385, 551, 40, 30) == 44)
                {
                    Console.WriteLine("6번슬롯 발견, 쿠폰적용 실행");
                    couponCheck(1386, 567);
                }
            }
            Thread.Sleep(100);

            if (slotCheck1 == 1 && slotCheck2 == 1 && slotCheck3 == 1 && slotCheck4 == 1 && slotCheck5 == 1 && slotCheck6 == 1)
            {
                if (Image_Match("버튼_흑", "\\subs\\", WindowHandler, 1385, 595, 55, 60) == 44 || Image_Match("버튼_백", "\\subs\\", WindowHandler, 1385, 595, 55, 60) == 44 || Image_Match("쿠폰개", "\\subs\\", WindowHandler, 1385, 608, 40, 30) == 44)
                {
                    Console.WriteLine("7번슬롯 발견, 쿠폰적용 실행");
                    couponCheck(1386, 627);
                }
            }

            Thread.Sleep(100);

            if (slotCheck1 == 1 && slotCheck2 == 1 && slotCheck3 == 1 && slotCheck4 == 1 && slotCheck5 == 1 && slotCheck6 == 1 && slotCheck7 == 1)
            {
                if (Image_Match("버튼_흑", "\\subs\\", WindowHandler, 1385, 651, 55, 60) == 44 || Image_Match("버튼_백", "\\subs\\", WindowHandler, 1385, 651, 55, 60) == 44 || Image_Match("쿠폰개", "\\subs\\", WindowHandler, 1385, 665, 40, 30) == 44)
                {
                    Console.WriteLine("8번슬롯 발견, 쿠폰적용 실행");
                    couponCheck(1386, 683);
                }
            }
            Thread.Sleep(100);
        }

        public void slideCheck()// 슬라이드 체크
        {
            // 맨밑에 +0을 찾기
            if (Image_Match("8번슬롯_+0", "\\subs\\", WindowHandler, 1326, 645, 126, 81) == 44)
            {
                Console.WriteLine("슬라이드 체크 실패 완료 체크");
                //완료 로직
                Random rx = new Random();
                Random ry = new Random();
                int locx = rx.Next(-100, 100);
                int locy = rx.Next(-100, 100);
                InClick(1770 + locx, 431 + locy);
                if (Image_Match("버튼_개_수정", "\\subs\\", WindowHandler, 1333, 228, 120, 488) == 44 || Image_Match("쿠폰개", "\\subs\\", WindowHandler, 1333, 228, 120, 488) == 44 || Image_Match("버튼_개_수정3", "\\subs\\", WindowHandler, 1333, 228, 120, 488) == 44) //
                {
                    Console.WriteLine("남은 쿠폰 발견 슬라이드 체크 작동");
                }
                else
                {
                    //InClick(1597, 271);
                    Console.WriteLine("남은 쿠폰 미발견 쿠폰수령 실행");
                    subsEndCheck = 1;
                    return;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    if (Image_Match("강화_" + j + "강", "\\subs\\", WindowHandler, 700, slideCheckX[i], 90, slideCheckY[i]) == 44)
                    {
                        slideCheckArr[i] = j;
                        break;
                    }
                }
            }
            for (int j = 0; j < 8; j++)
            {
                Console.WriteLine(slideCheckArr[j]);
            }
            InClick_scroll(1592, 688); // 내려갔는지 체크...
            for (int i = 0; i < 8; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    if (Image_Match("강화_" + j + "강", "\\subs\\", WindowHandler, 700, slideCheckX[i], 90, slideCheckY[i]) == 44)
                    {
                        slideCheckTemp[i] = j;
                        break;
                    }
                }
            }

            for (int j = 0; j < 8; j++)
            {
                Console.WriteLine(slideCheckTemp[j]);
            }

            if (slideCheckArr.SequenceEqual(slideCheckTemp))
            {
                Console.WriteLine("슬라이드 체크 실패 완료 체크");
                //완료 로직

                Random rx = new Random();
                Random ry = new Random();
                int locx = rx.Next(-100, 100);
                int locy = rx.Next(-100, 100);
                InClick(1770 + locx, 431 + locy);


                if (Image_Match("버튼_개_수정", "\\subs\\", WindowHandler, 1333, 228, 120, 488) == 44 || Image_Match("쿠폰개", "\\subs\\", WindowHandler, 1333, 228, 120, 488) == 44 || Image_Match("버튼_개_수정2", "\\subs\\", WindowHandler, 1333, 228, 120, 488) == 44) //
                {
                    Console.WriteLine("남은 쿠폰 발견 슬라이드 체크 작동");
                    return;
                }
                else
                {
                    //InClick(1597, 271);
                    Console.WriteLine("남은 쿠폰 미발견 쿠폰수령 실행");
                    subsEndCheck = 1;
                    return;

                }

            }
            else
            {
                Console.WriteLine("쿠폰선택 재실행");
            }
        }

        public void couponCheck(int x, int y)// 쿠폰처리이벤트
        {
            MoveTo(1770, 431);
            int couponErrorCheck = 0;
        couponCheckStart:
            InClick(x, y);
            Thread.Sleep(500);
            MoveTo(1770, 431);
            if (Image_Match("쿠폰선택", "\\subs\\", WindowHandler, 417, 249, 100, 47) == 44 || Image_Match("쿠폰선택2", "\\subs\\", WindowHandler, 572, 386, 76, 41) == 44)
            {
                Console.WriteLine("쿠폰 선택창 발견");
                InClick(847, 431);
                InClick(847, 431);
                Thread.Sleep(500);
                MoveTo(1770, 431);

                if (Image_Match("쿠폰선택", "\\subs\\", WindowHandler, 417, 249, 100, 47) != 44 || Image_Match("쿠폰선택2", "\\subs\\", WindowHandler, 572, 386, 76, 41) != 44)
                {
                    Console.WriteLine("쿠폰 적용완료");
                    if (x == 1386 && y == 287) slotCheck1 = 1;
                    else if (x == 1386 && y == 345) slotCheck2 = 1;
                    else if (x == 1386 && y == 401) slotCheck3 = 1;
                    else if (x == 1386 && y == 458) slotCheck4 = 1;
                    else if (x == 1386 && y == 511) slotCheck5 = 1;
                    else if (x == 1386 && y == 567) slotCheck6 = 1;
                    else if (x == 1386 && y == 627) slotCheck7 = 1;
                    else if (x == 1386 && y == 683) slotCheck8 = 1;
                    return;
                }

            }
            else
            {
                couponErrorCheck++;
                Console.WriteLine("쿠폰 선택창 발견 못함 시도 횟수 :" + couponErrorCheck + "회");
                if (couponErrorCheck > 3)
                {
                    Console.WriteLine("에러발생");
                    Send_Msg_DB("쿠폰 선택창 발견 못함", 'B');
                    MessageBox.Show("에러발생 쿠폰 선택창 발견 못함");
                    return;
                }
            }
            goto couponCheckStart;
        }

        public void subsEnd()// 쿠폰처리이벤트
        {
        lastCheck:
            //최고 상단으로
            for (int i = 0; i < 15; i++)
            {
                InClick_scroll(1592, 276);
                Thread.Sleep(100);
            }

            //InClick_large(1770,540);
            MoveTo(1770, 431);
            //최종 검토

            int subsReadyCheck1 = 0;
            int subsReadyCheck2 = 0;
            int subsReadyCheck3 = 0;

            var check1 = Image_Match("하단화살표", "\\subs\\", WindowHandler, 880, 230, 30, 25);
            var check2 = Image_Match("판매금액화살표", "\\subs\\", WindowHandler, 880, 230, 30, 25);
            var check3 = Image_Match("화살표", "\\subs\\", WindowHandler, 880, 230, 30, 25);
            if (check1 == 44 || check2 == 44 && check3 == 44)
            {
                subsReadyCheck1 = 1;
            }
            MoveTo(1770, 431);
            if (Image_Match("밝은+0", "\\subs\\", WindowHandler, 1244, 259, 69, 452) == 44 || Image_Match("어두운+0", "\\subs\\", WindowHandler, 1244, 259, 69, 452) == 44) //1244 259 1313 711 탑클확인
            {
                Console.WriteLine("탑클 없음");
                if (top_class == "ok")
                {
                    Send_Msg_DB("탑클 없음", 'B');
                    MessageBox.Show("탑클 없음");
                    goto subsFinish;
                }
                else
                {
                    subsReadyCheck2 = 1;
                }
            }
            else
            {
                subsReadyCheck2 = 1;
                Console.WriteLine("탑클 있음");
            }

            Random rx = new Random();
            Random ry = new Random();
            int locx = rx.Next(-100, 100);
            int locy = rx.Next(-100, 100);
            InClick(1770 + locx, 431 + locy);
            if (Image_Match("버튼_개_수정", "\\subs\\", WindowHandler, 1330, 244, 128, 495) != 44 && Image_Match("쿠폰개", "\\subs\\", WindowHandler, 1330, 244, 128, 495) != 44 && Image_Match("버튼_개_수정3", "\\subs\\", WindowHandler, 1330, 244, 128, 495) != 44)
            {
                subsReadyCheck3 = 1;
            }

            Thread.Sleep(1000);
            if (subsReadyCheck1 == 1 && subsReadyCheck2 == 1 && subsReadyCheck3 == 1)
            {
                goto subsDone;
            }
            else
            {
                goto lastCheck;
            }

        subsDone:
            //스샷 찰영
            try
            {

                try
                {
                    Bitmap bmpTmp;
                    bmpTmp = CaptureApplication("mstsc");
                    // 이름 : 진민강 / ID : chrre13
                    var subsName = ABS_Name.Split('/')[0]; //이름: 진민강
                    subsName = subsName.Split(':')[1];
                    var fifaroId = ABS_Name.Split('/')[1];//ID : chrre13
                    fifaroId = fifaroId.Split(':')[1];

                    bmpTmp.Save("c:\\" + subsName + "_" + fifaroId + "_" + ABS_Order_Num + ".png", ImageFormat.Png);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }


            //100이상인지 확인... 이하면 수령 아니라면 점검 필요
            MoveTo(1770, 431);
            var billionCheck1 = 0;
            var billionCheck2 = 0;

            for (var i = 0; i < 10; i++)
            {
                if (Image_Match(i.ToString(), "\\subs\\", WindowHandler, 1350, 755, 45, 40) == 44) //1368 755 1390 796
                {
                    billionCheck1 = 1;
                }
            }
            /*
            if (Image_Match("100억", "\\subs\\", WindowHandler, 1340, 755, 40, 45) == 44) //1368 755 1390 796
            {
                //찾았다면 공백이 아니기 떄문에 99억 미만
            }
            else {
                //찾지 못한다면 100억이상임.
                billionCheck1 = 1;
            }
            */
/*
            if (Image_Match("100억콤마", "\\subs\\", WindowHandler, 1393, 748, 43, 60) == 44)
            {
                //여기에 콤마가 있으면 10억이상
                billionCheck2 = 1;
            }

            if (billionCheck1 == 1 && billionCheck2 == 1)//100억 이상 검토 필요
            {
                Send_Msg_DB("100억이상 검토 필요", 'B');
                MessageBox.Show("100억이상 검토 필요");
                finalCheck = 1;
            }
            else
            { // 이하
                Console.WriteLine("100억이하 미검토 완료 최종수령 진행");
            lastCheck2:
                Image_Match_Click("최종수령_확인버튼", "\\subs\\", WindowHandler, 1201, 850, 205, 60);
                Thread.Sleep(1000);
                if (Image_Match("최종수령_확인버튼", "\\subs\\", WindowHandler, 1201, 850, 205, 60) == 44)
                {
                    Thread.Sleep(1000);
                    goto lastCheck2;
                }

                // ajax로 fifaro에 완료 보내고
                finalCheck = 1;
            }

        //확인버튼 클릭
        //Image_Match_Click("최종수령_확인버튼", "\\subs\\", WindowHandler, 0, 0, 0, 0);

        //디비 삭제
        subsFinish:
            Console.WriteLine("동작 완료");
            Console.WriteLine("DB 삭제 및 다음 회원 대기");
        }



        public void Timer_reboot()
        {
            string Timer_Query = "delete from start_user_table where pc_num = '" + Pc_Num + "';";
            Delete_DB(Timer_Query);
            Update_DB("접속완료");
        }
        public void Force_Reboot()
        {
            string Timer_Query = "delete from start_user_table where pc_num = '" + Pc_Num + "';";
            Delete_DB(Timer_Query);
            Process.Start("c:/Start_WMM1.exe");
            Process_Kill2("subsStart");
        }
        public void DB_Delete()
        {
            string Timer_Query = "delete from start_user_table where pc_num = '" + Pc_Num + "' AND ord_num =" + ABS_Order_Num + ";";
            Delete_DB(Timer_Query);
            Timer_Query = "delete from msg_new where pc_num = '" + Pc_Num + "';";
            Delete_DB(Timer_Query);
        }

        public void rand_watitng(int a)
        {
            Random Rand_Time = new Random();
            int Set_Rand_Time = Rand_Time.Next(a / 2, a + a / 2);
            Thread.Sleep(Set_Rand_Time);
        }
        public void Rand_Click_select()
        {
            Random x = new Random();
            Random y = new Random();
            int locx = x.Next(770, 1100);
            int locy = y.Next(60, 350);
            InClick(locx, locy);
        }
        public void Image_Match_Click(string name, string url, string AppPlayerName, int x, int y, int width, int height)
        {

            try
            {
                IntPtr findwindow = FindWindow(null, AppPlayerName);
                if (findwindow != IntPtr.Zero)
                {
                    rand_watitng(20);
                    Bitmap server_img = null;
                    Bitmap bmp = null;
                    try
                    {
                        server_img = new Bitmap(System.Windows.Forms.Application.StartupPath + @"\img" + url + name + ".PNG");
                        Console.WriteLine(System.Windows.Forms.Application.StartupPath + @"\img" + url + name + ".PNG");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("imc1");
                        Console.WriteLine(System.Windows.Forms.Application.StartupPath + @"\img" + url + name + ".PNG");
                        Console.WriteLine(e.Message, ToString());
                        server_img = new Bitmap(10, 10);
                        bmp = new Bitmap(10, 10);
                        server_img.Dispose();
                        bmp.Dispose();
                        return;
                    }

                    try
                    {
                        Graphics Graphicsdata = Graphics.FromHwnd(findwindow);
                        Rectangle rect = Rectangle.Round(Graphicsdata.VisibleClipBounds);
                        bmp = new Bitmap(rect.Width, rect.Height);
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            IntPtr hdc = g.GetHdc();
                            PrintWindow(findwindow, hdc, 0x2);
                            g.ReleaseHdc(hdc);
                        }
                        if (x != 0 || y != 0 || width != 0 || height != 0)
                        {
                            Bitmap bmp2 = null;
                            bmp2 = bmp.Clone(new System.Drawing.Rectangle(x, y, width, height), bmp.PixelFormat);
                            bmp.Dispose();
                            TrySearch(bmp2, server_img, x, y, server_img.Width, server_img.Height);

                            server_img = new Bitmap(10, 10);
                            bmp = new Bitmap(10, 10);
                            server_img.Dispose();
                            bmp2.Dispose();
                        }
                        else
                        {
                            TrySearch(bmp, server_img, x, y, server_img.Width, server_img.Height);

                            server_img = new Bitmap(10, 10);
                            bmp = new Bitmap(10, 10);
                            server_img.Dispose();
                            bmp.Dispose();
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(name);
                        Console.WriteLine("imc2");
                        Console.WriteLine(e.Message, ToString());
                        server_img = new Bitmap(10, 10);
                        bmp = new Bitmap(10, 10);
                        server_img.Dispose();
                        bmp.Dispose();

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("imc3");
                Console.WriteLine(e.Message, ToString());
            }
        }
        public void Image_Match_Click_right(string name, string url, string AppPlayerName, int x, int y, int width, int height)
        {

            try
            {
                IntPtr findwindow = FindWindow(null, AppPlayerName);
                if (findwindow != IntPtr.Zero)
                {
                    rand_watitng(20);
                    Bitmap server_img = null;
                    Bitmap bmp = null;
                    try
                    {
                        server_img = new Bitmap(System.Windows.Forms.Application.StartupPath + @"\img" + url + name + ".PNG");
                        Console.WriteLine(System.Windows.Forms.Application.StartupPath + @"\img" + url + name + ".PNG");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("imc1");
                        Console.WriteLine(System.Windows.Forms.Application.StartupPath + @"\img" + url + name + ".PNG");
                        Console.WriteLine(e.Message, ToString());
                        server_img = new Bitmap(10, 10);
                        bmp = new Bitmap(10, 10);
                        server_img.Dispose();
                        bmp.Dispose();
                        return;
                    }

                    try
                    {
                        Graphics Graphicsdata = Graphics.FromHwnd(findwindow);
                        Rectangle rect = Rectangle.Round(Graphicsdata.VisibleClipBounds);
                        bmp = new Bitmap(rect.Width, rect.Height);
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            IntPtr hdc = g.GetHdc();
                            PrintWindow(findwindow, hdc, 0x2);
                            g.ReleaseHdc(hdc);
                        }
                        if (x != 0 || y != 0 || width != 0 || height != 0)
                        {
                            Bitmap bmp2 = null;
                            bmp2 = bmp.Clone(new System.Drawing.Rectangle(x, y, width, height), bmp.PixelFormat);
                            bmp.Dispose();
                            TrySearch_right(bmp2, server_img, x, y, server_img.Width, server_img.Height);

                            server_img = new Bitmap(10, 10);
                            bmp = new Bitmap(10, 10);
                            server_img.Dispose();
                            bmp2.Dispose();
                        }
                        else
                        {
                            TrySearch_right(bmp, server_img, x, y, server_img.Width, server_img.Height);

                            server_img = new Bitmap(10, 10);
                            bmp = new Bitmap(10, 10);
                            server_img.Dispose();
                            bmp.Dispose();
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(name);
                        Console.WriteLine("imc2");
                        Console.WriteLine(e.Message, ToString());
                        server_img = new Bitmap(10, 10);
                        bmp = new Bitmap(10, 10);
                        server_img.Dispose();
                        bmp.Dispose();

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("imc3");
                Console.WriteLine(e.Message, ToString());
            }
        }
        Bitmap CropImage(Bitmap source, Rectangle section)
        {
            var bitmap = new Bitmap(section.Width, section.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
                return bitmap;
            }
        }
        public int Image_Match_2pw(string name, string url, string AppPlayerName)
        {
            IntPtr findwindow = FindWindow(null, AppPlayerName);
            if (findwindow != IntPtr.Zero)
            {
                int x = 913, y = 488, width = 420, height = 210;

                Bitmap bmp = null;

                Bitmap server_img = new Bitmap(System.Windows.Forms.Application.StartupPath + @"\img" + url + name + ".png");

                try
                {
                    Graphics Graphicsdata = Graphics.FromHwnd(findwindow);
                    Rectangle rect = Rectangle.Round(Graphicsdata.VisibleClipBounds);
                    //Console.WriteLine(rect.Width + " " + rect.Height);
                    bmp = new Bitmap(rect.Width, rect.Height); // 여기서 터짐
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        IntPtr hdc = g.GetHdc();
                        PrintWindow(findwindow, hdc, 0x2);
                        g.ReleaseHdc(hdc);
                    }
                    Bitmap bmp2 = null;
                    bmp2 = bmp.Clone(new System.Drawing.Rectangle(x, y, width, height), bmp.PixelFormat);
                    bmp.Dispose();
                    int rerult = TrySearch_2pw(bmp2, server_img, x, y);
                    server_img = new Bitmap(10, 10);
                    bmp = new Bitmap(10, 10);
                    server_img.Dispose();
                    bmp2.Dispose();

                    return rerult;
                }
                catch (Exception e)
                {
                    Console.WriteLine(name);
                    Console.WriteLine("im22");
                    Console.WriteLine(e.Message, ToString());
                    server_img = new Bitmap(10, 10);
                    server_img.Dispose();
                    bmp.Dispose();
                    return 21;
                }
                finally
                {
                    bmp.Dispose();
                    server_img.Dispose();

                }
            }
            else
            {
                return 21;
            }

        }
        public void Image_Match_Diff_Click(string etc, string url, int x, int y, string AppPlayerName, int startx, int starty, int width, int height)
        {

            try
            {
                IntPtr findwindow = FindWindow(null, AppPlayerName);
                if (findwindow != IntPtr.Zero)
                {
                    rand_watitng(20);
                    Bitmap etc_img = null;
                    Bitmap bmp = null;
                    try
                    {
                        etc_img = new Bitmap(System.Windows.Forms.Application.StartupPath + @"\img\" + url + etc + ".PNG");

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("imdc1");
                        Console.WriteLine(e.Message, ToString());
                        etc_img = new Bitmap(10, 10);
                        bmp = new Bitmap(10, 10);
                        etc_img.Dispose();
                        bmp.Dispose();
                        return;
                    }

                    try
                    {
                        Graphics Graphicsdata = Graphics.FromHwnd(findwindow);
                        Rectangle rect = Rectangle.Round(Graphicsdata.VisibleClipBounds);
                        bmp = new Bitmap(rect.Width, rect.Height + 50);
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            IntPtr hdc = g.GetHdc();
                            PrintWindow(findwindow, hdc, 0x2);
                            g.ReleaseHdc(hdc);
                        }

                        if (x != 0 || y != 0 || width != 0 || height != 0)
                        {
                            Bitmap bmp2 = null;
                            bmp2 = bmp.Clone(new System.Drawing.Rectangle(x, y, width, height), bmp.PixelFormat);
                            bmp.Dispose();
                            TrySearch_mouse(bmp2, etc_img, x, y, startx, starty, etc_img.Width, etc_img.Height);
                            etc_img = new Bitmap(10, 10);
                            etc_img.Dispose();
                        }
                        else
                        {
                            TrySearch_mouse(bmp, etc_img, x, y, startx, starty, etc_img.Width, etc_img.Height);
                            etc_img = new Bitmap(10, 10);
                            bmp = new Bitmap(10, 10);
                            bmp.Dispose();
                            etc_img.Dispose();
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(etc);
                        Console.WriteLine("imdc2");
                        Console.WriteLine(e.Message, ToString());
                        etc_img = new Bitmap(10, 10);
                        bmp = new Bitmap(10, 10);
                        etc_img.Dispose();
                        bmp.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("imdc3");
                Console.WriteLine(e.Message, ToString());
            }
        }
        public int Image_Match(string state, string url, string AppPlayerName, int startx, int starty, int width, int height)
        {

            Console.WriteLine("image_match :" + state);
            try
            {
                IntPtr findwindow = FindWindow(null, AppPlayerName);
                Console.WriteLine(findwindow);
                if (findwindow != IntPtr.Zero)
                {
                    rand_watitng(20);
                    Bitmap check_img = null;
                    Bitmap bmp = null;
                    Bitmap bmp2 = null;
                    try
                    {
                        check_img = new Bitmap(System.Windows.Forms.Application.StartupPath + @"\img\" + url + state + ".PNG");
                        Console.WriteLine(System.Windows.Forms.Application.StartupPath + @"\img\" + url + state + ".PNG");

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(System.Windows.Forms.Application.StartupPath + @"\img\" + url + state + ".PNG");
                        Console.WriteLine("im1");
                        Console.WriteLine(e.Message, ToString());
                        check_img = new Bitmap(10, 10);
                        bmp = new Bitmap(10, 10);
                        check_img.Dispose();
                        bmp.Dispose();
                        return 11;
                    }

                    try
                    {

                        Graphics Graphicsdata = Graphics.FromHwnd(findwindow);
                        Rectangle rect = Rectangle.Round(Graphicsdata.VisibleClipBounds);

                        bmp = new Bitmap(rect.Width, rect.Height);
                        Console.WriteLine(rect.Width + " " + rect.Height);


                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            IntPtr hdc = g.GetHdc();
                            PrintWindow(findwindow, hdc, 0x2);
                            g.ReleaseHdc(hdc);
                        }
                        if (startx != 0 || starty != 0 || width != 0 || height != 0)
                        {

                            bmp2 = bmp.Clone(new System.Drawing.Rectangle(startx, starty, width, height), bmp.PixelFormat);
                            Console.WriteLine("7");
                            bmp.Dispose();
                            int result = TrySearch_image(bmp2, check_img);
                            check_img = new Bitmap(10, 10);
                            bmp = new Bitmap(10, 10);
                            bmp2 = new Bitmap(10, 10);
                            bmp2.Dispose();
                            check_img.Dispose();



                            return result;
                        }
                        else
                        {
                            int result = TrySearch_image(bmp, check_img);
                            check_img = new Bitmap(10, 10);
                            bmp = new Bitmap(10, 10);
                            bmp.Dispose();
                            check_img.Dispose();
                            bmp2 = new Bitmap(10, 10);
                            bmp2.Dispose();
                            return result;
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("im2");
                        Console.WriteLine(state);
                        Console.WriteLine(e.Message, ToString());
                        check_img = new Bitmap(10, 10);
                        bmp = new Bitmap(10, 10);
                        check_img.Dispose();
                        bmp.Dispose();
                        bmp2 = new Bitmap(10, 10);
                        bmp2.Dispose();

                        return 11;
                    }
                }
                else
                {

                    return 11;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("im3");
                Console.WriteLine(e.Message, ToString());
                return 11;
            }
        }
        public void TrySearch(Bitmap screen_img, Bitmap find_img, int startx, int starty, int range_x, int range_y)
        {

            try
            {
                using (Mat ScreenMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(screen_img))
                using (Mat FindMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(find_img))
                using (Mat res = ScreenMat.MatchTemplate(FindMat, TemplateMatchModes.CCoeffNormed))
                {
                    double minval, maxval = 0;
                    OpenCvSharp.Point minloc, maxloc;
                    Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);
                    Console.WriteLine("이미지 매칭률:" + maxval);
                    if (maxval >= 0.8)
                    {
                        InClick2(maxloc.X + startx, maxloc.Y + starty, range_x, range_y);
                    }
                    ScreenMat.Dispose();
                    FindMat.Dispose();
                    res.Dispose();
                    screen_img.Dispose();
                    find_img.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ts1");
                Console.WriteLine(e.Message, ToString());

                screen_img.Dispose();
                find_img.Dispose();
            }
            finally
            {
            }
        }
        public void TrySearch_right(Bitmap screen_img, Bitmap find_img, int startx, int starty, int range_x, int range_y)
        {

            try
            {
                using (Mat ScreenMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(screen_img))
                using (Mat FindMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(find_img))
                using (Mat res = ScreenMat.MatchTemplate(FindMat, TemplateMatchModes.CCoeffNormed))
                {
                    double minval, maxval = 0;
                    OpenCvSharp.Point minloc, maxloc;
                    Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);
                    Console.WriteLine("이미지 매칭률:" + maxval);
                    if (maxval >= 0.8)
                    {
                        Console.WriteLine("우클릭 실행 좌표 :" + maxloc.X + " ," + maxloc.Y);
                        InClick_right(maxloc.X + startx, maxloc.Y + starty);
                    }
                    ScreenMat.Dispose();
                    FindMat.Dispose();
                    res.Dispose();
                    screen_img.Dispose();
                    find_img.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ts1");
                Console.WriteLine(e.Message, ToString());

                screen_img.Dispose();
                find_img.Dispose();
            }
            finally
            {
            }
        }
        public int TrySearch_2pw(Bitmap screen_img, Bitmap find_img, int x, int y)
        {

            try
            {
                using (Mat ScreenMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(screen_img))
                using (Mat FindMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(find_img))
                using (Mat res = ScreenMat.MatchTemplate(FindMat, TemplateMatchModes.CCoeffNormed))
                {
                    double minval, maxval = 0;
                    OpenCvSharp.Point minloc, maxloc;
                    Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);
                    Console.WriteLine("이미지 매칭률:" + maxval);
                    if (maxval >= 0.8)
                    {

                        InClick30(maxloc.X + x, maxloc.Y + y);
                        screen_img.Dispose();
                        find_img.Dispose();
                        ScreenMat.Dispose();
                        FindMat.Dispose();
                        res.Dispose();
                        return 77;
                    }
                    else
                    {
                        ScreenMat.Dispose();
                        FindMat.Dispose();
                        res.Dispose();
                        screen_img.Dispose();
                        find_img.Dispose();
                        return 21;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ts2");
                Console.WriteLine(e.Message, ToString());

                screen_img.Dispose();
                find_img.Dispose();
                return 21;
            }
            finally
            {

            }
        }
        public void TrySearch_mouse(Bitmap screen_img, Bitmap find_img, int x, int y, int startx, int starty, int range_x, int range_y)
        {

            try
            {
                using (Mat ScreenMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(screen_img))
                using (Mat FindMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(find_img))
                using (Mat res = ScreenMat.MatchTemplate(FindMat, TemplateMatchModes.CCoeffNormed))
                {
                    double minval, maxval = 0;
                    OpenCvSharp.Point minloc, maxloc;
                    Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);
                    Console.WriteLine("이미지 매칭률:" + maxval);
                    if (maxval >= 0.8)
                    {

                        InClick2(x + startx, y + starty, range_x, range_y);
                    }
                    ScreenMat.Dispose();
                    FindMat.Dispose();
                    res.Dispose();
                    screen_img.Dispose();
                    find_img.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("tsm");
                Console.WriteLine(e.Message, ToString());

                screen_img.Dispose();
                find_img.Dispose();
            }
            finally
            {

            }
        }
        public int TrySearch_image(Bitmap screen_img, Bitmap find_img)
        {

            try
            {
                using (Mat ScreenMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(screen_img))
                using (Mat FindMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(find_img))
                using (Mat res = ScreenMat.MatchTemplate(FindMat, TemplateMatchModes.CCoeffNormed))
                {
                    double minval, maxval = 0;
                    OpenCvSharp.Point minloc, maxloc;
                    Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);

                    Console.WriteLine("이미지 매칭률:" + maxval);
                    if (maxval >= 0.88)
                    {

                        ScreenMat.Dispose();
                        FindMat.Dispose();
                        res.Dispose();
                        screen_img.Dispose();
                        find_img.Dispose();
                        return 44;
                    }
                    else
                    {
                        ScreenMat.Dispose();
                        FindMat.Dispose();
                        res.Dispose();
                        screen_img.Dispose();
                        find_img.Dispose();
                        return 11;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("tsi");
                Console.WriteLine(e.Message, ToString());

                screen_img.Dispose();
                find_img.Dispose();
                return 11;
            }
            finally
            {

            }
        }
        public int Window_Check(String AppPlayerName)
        {
            try
            {
                IntPtr findwindow = FindWindow(null, AppPlayerName);
                if (!findwindow.Equals(IntPtr.Zero))
                {
                    return 55;
                }
                else
                {
                    return 12;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("wc");
                Console.WriteLine(e.Message, ToString());
                return 12;
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
        public void Rand_Click(int start_x, int start_y)
        {
            Random x = new Random();
            Random y = new Random();
            int locx = x.Next(0, 20);
            int locy = y.Next(0, 20);
            InClick(start_x + locx, start_y + locy);
        }
        public void Discord_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process[] mProcess = System.Diagnostics.Process.GetProcessesByName(Application.ProductName);
            foreach (System.Diagnostics.Process p in mProcess)
                p.Kill();
            try
            {
                Process_Kill2("subsStart");

            }
            catch
            {
            }


        }
        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TrackWorkthread_Get_User_Data.Resume();
            }
            catch (Exception err)
            {
                Console.WriteLine("b1c1");
                Console.WriteLine(err.Message, ToString());
            }
            try
            {
                TrackWorkthread_FIFA.Resume();
            }
            catch (Exception err)
            {
                Console.WriteLine("b1c2");
                Console.WriteLine(err.Message, ToString());
            }
            try
            {
                TrackWorkthread_Timer.Resume();
            }
            catch (Exception err)
            {
                Console.WriteLine("b1c3");
                Console.WriteLine(err.Message, ToString());
            }
        }
        public void button2_Click(object sender, EventArgs e)
        {
            try
            {
                TrackWorkthread_FIFA.Suspend();
            }
            catch (Exception err)
            {
                Console.WriteLine("b2c1");
                Console.WriteLine(err.Message, ToString());
            }
            try
            {
                TrackWorkthread_Timer.Suspend();
            }
            catch (Exception err)
            {
                Console.WriteLine("b2c2");
                Console.WriteLine(err.Message, ToString());
            }
            try
            {
                TrackWorkthread_Get_User_Data.Suspend();
            }
            catch (Exception err)
            {
                Console.WriteLine("b2c3");
                Console.WriteLine(err.Message, ToString());
            }
        }
        public void button3_Click(object sender, EventArgs e)
        {
            TrackWorkthread_Force_Reboot = new Thread(new ThreadStart(Force_Reboot));
            TrackWorkthread_Force_Reboot.Start();
        }
        public void button3_Click_1(object sender, EventArgs e)
        {
            Get_ABS_DB("start_user_table");
            TrackWorkthread_FIFA = new Thread(new ThreadStart(gt_fms));
            TrackWorkthread_FIFA.ApartmentState = ApartmentState.STA;
            TrackWorkthread_FIFA.Start();
        }
        public void button4_Click(object sender, EventArgs e) // 디비 삭제후 재부팅x
        {
            TrackWorkthread_DB_Delete = new Thread(new ThreadStart(DB_Delete));
            TrackWorkthread_DB_Delete.Start();
        }

    }
}
*/

