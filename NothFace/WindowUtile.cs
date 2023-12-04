namespace NothFace
{
    internal class WindowUtile
    {
        public void TNF_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process[] mProcess = System.Diagnostics.Process.GetProcessesByName(Application.ProductName);
            foreach (System.Diagnostics.Process p in mProcess)
                p.Kill();
        }
    }
}