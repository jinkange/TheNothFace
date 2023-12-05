using System.Diagnostics;

namespace NothFace
{
    public partial class MainForm : Form
    {
        [System.Runtime.InteropServices.DllImport("User32", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private readonly MouseUtile mouseUtile;
        private readonly KeyboardUtile keyboardUtile;
        private readonly WindowUtile windowUtile;

        System.Threading.Thread TrackWorkthread;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Main()
        {
            Setting();
            Watiting();
            Credit();
        }
        private void Setting()
        {
            string macroId = macroIdBox.Text;
            int password = int.Parse(passwordBox.Text);
            int count = int.Parse(countBox.Text);

            //WindowUtile.Window_Set("TheNothFace" + macroId);
            IntPtr findwindow = FindWindow(null, "TheNothFace1");
            WindowUtile.MoveWindow(findwindow,0,0,900, 960,false);
            Debug.WriteLine(WindowUtile.Window_Check("TheNothFace" + macroId));
            Debug.WriteLine("TheNothFace" + macroId);
            Debug.WriteLine(macroId);
            Debug.WriteLine(password);
            Debug.WriteLine(count);

        }

        private void Watiting()
        {

        }

        private void Credit()
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Start_Click(object sender, EventArgs e)
        {
            Main();
        }

        private void Stop_Click(object sender, EventArgs e)
        {

        }

        private void count_Click(object sender, EventArgs e)
        {

        }

        private void password_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void id_Click(object sender, EventArgs e)
        {

        }

        private void macroId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}