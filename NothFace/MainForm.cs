namespace NothFace
{
    public partial class MainForm : Form
    {
        private readonly MouseUtile mouseUtile;
        private readonly KeyboardUtile keyboardUtile;
        private readonly WindowUtile windowUtile;
        private readonly ImageMatching imageMatching;
        System.Threading.Thread TrackWorkthread;

        public MainForm()
        {
            InitializeComponent();
            Main();
        }

        private void Main() {
            Setting();
            Watiting();
            Credit();
        }
        private void Setting() { 
            
        }

        private void Watiting() { 
        
        }

        private void Credit() { 
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Start_Click(object sender, EventArgs e)
        {

        }

        private void Stop_Click(object sender, EventArgs e)
        {

        }
    }
}