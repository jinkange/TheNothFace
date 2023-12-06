using OpenCvSharp;
using System;
using System.ComponentModel;

namespace NothFace
{
    internal class ImageMatching
    {
        private MouseUtile mouseUtile = new MouseUtile();

        [System.Runtime.InteropServices.DllImport("User32", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        internal static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);
        

        public int Image_Match(string state, string url, string AppPlayerName, int startx, int starty, int width, int height)
        {
            try
            {
                IntPtr findwindow = FindWindow(null, AppPlayerName);
                if (findwindow != IntPtr.Zero)
                {
                    Thread.Sleep(10);
                    Bitmap check_img = null;
                    Bitmap bmp = null;
                    try
                    {
                        check_img = new Bitmap(System.Windows.Forms.Application.StartupPath + @"\img\" + url + state + ".PNG");

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("이미지 경로 찾기 실패");
                        Console.WriteLine(e.Message, ToString());
                        check_img = new Bitmap(10, 10);
                        bmp = new Bitmap(10, 10);
                        check_img.Dispose();
                        bmp.Dispose();
                        return 1;
                    }

                    try
                    {
                        Graphics Graphicsdata = Graphics.FromHwnd(findwindow);
                        Rectangle rect = Rectangle.Round(Graphicsdata.VisibleClipBounds);
                        TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                        bmp = new Bitmap(rect.Width, rect.Height);
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            IntPtr hdc = g.GetHdc();
                            PrintWindow(findwindow, hdc, 0x2);
                            g.ReleaseHdc(hdc);
                        }
                        if (startx != 0 || starty != 0 || width != 0 || height != 0)
                        {
                            Bitmap bmp2 = null;
                            bmp2 = bmp.Clone(new System.Drawing.Rectangle(startx, starty, width, height), bmp.PixelFormat);
                            bmp.Dispose();
                            int result = TrySearch_image(bmp2, check_img);
                            check_img = new Bitmap(10, 10);
                            bmp = new Bitmap(10, 10);
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
                            return result;
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("이미지 매칭 실패");
                        Console.WriteLine(state);
                        Console.WriteLine(e.Message, ToString());
                        check_img = new Bitmap(10, 10);
                        bmp = new Bitmap(10, 10);
                        check_img.Dispose();
                        bmp.Dispose();
                        return 1;
                    }
                }
                else
                {

                    return 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("이미지 매칭 전체 오류");
                Console.WriteLine(e.Message, ToString());
                return 1;
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

                    if (maxval >= 0.8)
                    {
                        mouseUtile.InClick2(maxloc.X + startx, maxloc.Y + starty, range_x, range_y);
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
                Console.WriteLine("이미시 서치 실패");
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

                    if (maxval >= 0.8)
                    {
                        Random Rand_Time = new Random();
                        int Set_Rand_Time = Rand_Time.Next(0, 100);
                        Thread.Sleep(Set_Rand_Time);
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

                    if (maxval >= 0.8)
                    {
                        Random Rand_Time = new Random();
                        int Set_Rand_Time = Rand_Time.Next(0, 100);
                        Thread.Sleep(Set_Rand_Time);
                        mouseUtile.InClick2(x + startx, y + starty, range_x, range_y);
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


    }
}