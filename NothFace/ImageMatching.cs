using OpenCvSharp;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace NothFace
{
    internal class ImageMatching
    {
        public MouseUtile mouseUtile;
        string macroId;
        [System.Runtime.InteropServices.DllImport("User32", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        internal static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);
        public ImageMatching(TextBox id) {
            this.macroId = id.Text;
            mouseUtile = new MouseUtile(id);
        }
        private void errorCheck() {
            if (this.ImageMatch("결제하기", "", "TheNothFace" + macroId, 0, 0, 0, 0)) {
                MessageBox.Show("일시적오류");
            }
        }
        public bool ImageMatchClick(string name, string url, string AppPlayerName, int x, int y, int width, int height)
        {
            try
            {
                IntPtr findwindow = FindWindow(null, AppPlayerName);
                if (findwindow != IntPtr.Zero)
                {
                    Bitmap server_img = null;
                    Bitmap bmp = null;
                    try
                    {
                        server_img = new Bitmap(System.Windows.Forms.Application.StartupPath + @"img\" + url + name + ".PNG");
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("이미지 못찾음");
                        Debug.WriteLine(System.Windows.Forms.Application.StartupPath + @"img\" + url + name + ".PNG");
                        Debug.WriteLine(e.Message, ToString());
                        server_img = new Bitmap(10, 10);
                        bmp = new Bitmap(10, 10);
                        server_img.Dispose();
                        bmp.Dispose();
                        return false;
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
                            if (TrySearch(bmp2, server_img, x, y, server_img.Width, server_img.Height))
                            {
                                server_img = new Bitmap(10, 10);
                                bmp = new Bitmap(10, 10);
                                server_img.Dispose();
                                bmp2.Dispose();
                                return true;
                            }
                            else
                            {
                                server_img = new Bitmap(10, 10);
                                bmp = new Bitmap(10, 10);
                                server_img.Dispose();
                                bmp2.Dispose();
                                return false;
                            }


                        }
                        else
                        {
                            if (TrySearch(bmp, server_img, x, y, server_img.Width, server_img.Height))
                            {
                                server_img = new Bitmap(10, 10);
                                bmp = new Bitmap(10, 10);
                                server_img.Dispose();
                                bmp.Dispose();
                                return true;
                            }
                            else {
                                server_img = new Bitmap(10, 10);
                                bmp = new Bitmap(10, 10);
                                server_img.Dispose();
                                bmp.Dispose();
                                return false;
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(name);
                        Debug.WriteLine("이미지 매칭 전체 오류");
                        Debug.WriteLine(e.Message, ToString());
                        server_img = new Bitmap(10, 10);
                        bmp = new Bitmap(10, 10);
                        server_img.Dispose();
                        bmp.Dispose();
                        return false;

                    }
                }
                else {
                    Debug.WriteLine("핸들 못찾음");
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("이미지 매칭 전체 오류");
                Debug.WriteLine(System.Windows.Forms.Application.StartupPath + @"\img\" + url + name + ".PNG");
                Debug.WriteLine(e.Message, ToString());
                return false;
            }
        }

        public bool ImageMatch(string name, string url, string AppPlayerName, int startx, int starty, int width, int height)
        {
            Debug.WriteLine(name + "찾기");
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
                        check_img = new Bitmap(System.Windows.Forms.Application.StartupPath + @"\img\" + url + name + ".PNG");

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("이미지 경로 찾기 실패");
                        Debug.WriteLine(e.Message, ToString());
                        check_img = new Bitmap(10, 10);
                        bmp = new Bitmap(10, 10);
                        check_img.Dispose();
                        bmp.Dispose();
                        return false;
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
                            bool result = TrySearch_image(bmp2, check_img);
                            check_img = new Bitmap(10, 10);
                            bmp = new Bitmap(10, 10);
                            bmp2.Dispose();
                            check_img.Dispose();
                            return result;
                        }
                        else
                        {
                            bool result = TrySearch_image(bmp, check_img);
                            check_img = new Bitmap(10, 10);
                            bmp = new Bitmap(10, 10);
                            bmp.Dispose();
                            check_img.Dispose();
                            return result;
                        }

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("이미지 매칭 실패");
                        Debug.WriteLine(name);
                        Debug.WriteLine(e.Message, ToString());
                        check_img = new Bitmap(10, 10);
                        bmp = new Bitmap(10, 10);
                        check_img.Dispose();
                        bmp.Dispose();
                        return false;
                    }
                }
                else
                {

                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("이미지 매칭 전체 오류");
                Debug.WriteLine(e.Message, ToString());
                return false;
            }
        }

        public bool TrySearch(Bitmap screen_img, Bitmap find_img, int startx, int starty, int range_x, int range_y)
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
                    Debug.WriteLine("이미시 매칭률 : " + maxval);
                    if (maxval >= 0.9)
                    {
                        mouseUtile.InClick(maxloc.X + startx, maxloc.Y + starty);
                        ScreenMat.Dispose();
                        FindMat.Dispose();
                        res.Dispose();
                        screen_img.Dispose();
                        find_img.Dispose();
                        return true;
                    }
                    else {
                        ScreenMat.Dispose();
                        FindMat.Dispose();
                        res.Dispose();
                        screen_img.Dispose();
                        find_img.Dispose();
                        return false;
                    }

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("이미시 서치 실패");
                Debug.WriteLine(e.Message, ToString());

                screen_img.Dispose();
                find_img.Dispose();
                return false;
            }
            finally
            {
            }
        }

        public bool TrySearch_image(Bitmap screen_img, Bitmap find_img)
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
                    Debug.WriteLine("이미시 매칭률 : " + maxval);
                    if (maxval >= 0.9)
                    {
                        Random Rand_Time = new Random();
                        int Set_Rand_Time = Rand_Time.Next(0, 100);
                        Thread.Sleep(Set_Rand_Time);
                        ScreenMat.Dispose();
                        FindMat.Dispose();
                        res.Dispose();
                        screen_img.Dispose();
                        find_img.Dispose();
                        return true;
                    }
                    else
                    {
                        ScreenMat.Dispose();
                        FindMat.Dispose();
                        res.Dispose();
                        screen_img.Dispose();
                        find_img.Dispose();
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("tsi");
                Debug.WriteLine(e.Message, ToString());

                screen_img.Dispose();
                find_img.Dispose();
                return false;
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

                    if (maxval >= 0.9)
                    {
                        Random Rand_Time = new Random();
                        int Set_Rand_Time = Rand_Time.Next(0, 100);
                        Thread.Sleep(Set_Rand_Time);
                        mouseUtile.InClick(x + startx, y + starty);
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
                Debug.WriteLine("tsm");
                Debug.WriteLine(e.Message, ToString());

                screen_img.Dispose();
                find_img.Dispose();
            }
            finally
            {

            }
        }


    }
}