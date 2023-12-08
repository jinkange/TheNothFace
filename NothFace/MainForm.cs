using System.Diagnostics;
using System.Threading;

namespace NothFace
{
    public partial class MainForm : Form
    {

        [System.Runtime.InteropServices.DllImport("User32", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private MouseUtile mouseUtile;
        private KeyboardUtile keyboardUtile;
        private WindowUtile windowUtile;
        private ImageMatching imageMatching;
        string handleName;
        string macroId;
        string password;
        string size;
        int count;
        int x = 0, y = 0, width = 0, height = 0;
        // ���� �÷���
        private static bool isThreadRunning = true;
        Thread StartMacro;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Main()
        {
            Setting();
        }
        private void Setting()
        {
            this.mouseUtile = new MouseUtile(macroIdBox);
            this.keyboardUtile = new KeyboardUtile();
            this.windowUtile = new WindowUtile();
            this.imageMatching = new ImageMatching(macroIdBox);
            macroId = macroIdBox.Text;
            password = passwordBox.Text;
            count = int.Parse(countBox.Text);
            size = sizeBox.Text;

            Debug.WriteLine(macroId);
            Debug.WriteLine(password);
            Debug.WriteLine(count);
            Debug.WriteLine(size);
            if (macroId == "")
            {
                MessageBox.Show("1~3������ ���ڸ� �Է� ���ּ���");
                return;
            }
            if (password == "")
            {
                MessageBox.Show("���̹� ���� ��й�ȣ 6�ڸ��� �Է����ּ���");
                return;
            }
            if (count <= 0)
            {
                MessageBox.Show("���� ������ ���� ���ּ���");
                return;
            }
            if (size == "")
            {
                MessageBox.Show("xs, s, m, l,lx,lxx ���ϳ��� �׾��ּ���(��ҹ��� ����)");
                return;
            }

            handleName = "TheNothFace" + macroId;

            if (macroId == "1")
            {
                x = 540 * (int.Parse(macroId) - 1);
                width = 540;
                height = 960;
            }
            else if (macroId == "2")
            {
                x = 540 * (int.Parse(macroId) - 1);
                width = 540;
                height = 960;
            }
            else if (macroId == "3")
            {
                x = 540 * (int.Parse(macroId) - 1);
                width = 540;
                height = 960;
            }
            IntPtr findwindow = FindWindow(null, handleName);
            WindowUtile.MoveWindow(findwindow, x, y, width, height, false);

            Watiting();
        }

        private void Watiting()
        {
            Watitng:
                Thread.Sleep(100);
                imageMatching.ImageMatchClick("������", "", handleName, 0, 0, 0, 0);
                if (imageMatching.ImageMatch("����Ȩ", "", handleName, 0, 0, 0, 0))
                {
                    mouseUtile.InClick(472, 865); // �ֱٺ���ǰŬ��
                }
                if (imageMatching.ImageMatch("�߸�Ŭ��", "", handleName, 0, 0, 0, 0))
                {
                    mouseUtile.InClick(472, 865); // �ֱٺ���ǰŬ��
                }
                if (imageMatching.ImageMatch("����", "", handleName, 0, 0, 0, 0))
                {
                    imageMatching.ImageMatchClick("����ȸ��", "", handleName, 0, 0, 0, 0);
                    MessageBox.Show("����");
                }
                if (imageMatching.ImageMatch("�ٷα���", "", handleName, 0, 0, 0, 0))
                {
                    imageMatching.ImageMatchClick("�ٷα���2", "", handleName, 0, 0, 0, 0);
                    //ȭ�� ���� Ȯ��(�ɼǼ���)
                    while (!imageMatching.ImageMatch("�ɼǼ������ּ���", "", handleName, 0, 0, 0, 0))
                    {
                        Thread.Sleep(500);
                    }
                    Thread.Sleep(1000);
                    //�ɼ� Ŭ�� // 1�� ���
                    imageMatching.ImageMatchClick("�ɼǼ������ּ���", "", handleName, 0, 0, 0, 0);
                    Thread.Sleep(1000);
                    //������ Ŭ�� // 1�� ���
                    imageMatching.ImageMatchClick(size, "", handleName, 0, 0, 0, 0);

                    Thread.Sleep(1000);
                    //�ٷα��� ��Ŭ�� //
                    imageMatching.ImageMatchClick("�ٷα���2", "", handleName, 0, 0, 0, 0);
                    Thread.Sleep(1000);
                    if (imageMatching.ImageMatch("���������", "", handleName, 0, 0, 0, 0))
                    {
                        imageMatching.ImageMatchClick("����������ݱ�", "", handleName, 0, 0, 0, 0);
                        Thread.Sleep(500);
                        imageMatching.ImageMatchClick("�ɼǴݱ�", "", handleName, 0, 0, 0, 0);
                        Thread.Sleep(500);
                        mouseUtile.InClick(472, 865);
                    }
                }
                if (imageMatching.ImageMatch("ǰ��", "", handleName, 0, 0, 0, 0))
                {
                    mouseUtile.InClick(472, 865);
                }
                if (imageMatching.ImageMatch("��ü����", "", handleName, 0, 0, 0, 0))
                {
                    Thread.Sleep(2000);
                    mouseUtile.InClick(130, 300);//ù��°��ǰŬ��
                }
                if (imageMatching.ImageMatch("��ۺ�ȳ�", "", handleName, 0, 0, 0, 0) ||
                    imageMatching.ImageMatch("�����ڵ��", "", handleName, 0, 0, 0, 0) ||
                    imageMatching.ImageMatch("��ũ��1", "", handleName, 0, 0, 0, 0) ||
                    imageMatching.ImageMatch("��ũ��2", "", handleName, 0, 0, 0, 0) ||
                    imageMatching.ImageMatch("��ũ��3", "", handleName, 0, 0, 0, 0) ||
                    imageMatching.ImageMatch("����", "", handleName, 0, 0, 0, 0) ||
                    imageMatching.ImageMatch("����Ȯ��", "", handleName, 0, 0, 0, 0) ||
                    imageMatching.ImageMatch("��ũ��4", "", handleName, 0, 0, 0, 0))
                {
                    // ���콺 �ش� ȭ�� �߰����� �̵�, �� ������
                    mouseUtile.MoveTo(width / 2 + x, height / 2);
                    mouseUtile.wheelDown();
                    Thread.Sleep(500);
                    mouseUtile.wheelDown();
                    Thread.Sleep(500);
                }
                imageMatching.ImageMatchClick("�����ϱ�1", "", handleName, 0, 0, 0, 0);
                imageMatching.ImageMatchClick("�����մϴ�", "", handleName, 0, 0, 0, 0);
                if (imageMatching.ImageMatch("���Ǿ���", "", handleName, 0, 0, 0, 0))
                {
                    imageMatching.ImageMatchClick("���Ǿ���ok", "", handleName, 0, 0, 0, 0);
                }
                imageMatching.ImageMatchClick("�ֹ��ϱ�", "", handleName, 0, 0, 0, 0);
                imageMatching.ImageMatchClick("���̹������ϱ�", "", handleName, 0, 0, 0, 0);
                if (imageMatching.ImageMatch("���̹���й�ȣâȮ��", "", handleName, 0, 0, 0, 0))
                {
                    Thread.Sleep(500);
                    imageMatching.ImageMatchClick(password.Substring(0, 1), "", handleName, 0, 0, 0, 0);
                    Thread.Sleep(500);
                    imageMatching.ImageMatchClick(password.Substring(1, 1), "", handleName, 0, 0, 0, 0);
                    Thread.Sleep(500);
                    imageMatching.ImageMatchClick(password.Substring(2, 1), "", handleName, 0, 0, 0, 0);
                    Thread.Sleep(500);
                    imageMatching.ImageMatchClick(password.Substring(3, 1), "", handleName, 0, 0, 0, 0);
                    Thread.Sleep(500);
                    imageMatching.ImageMatchClick(password.Substring(4, 1), "", handleName, 0, 0, 0, 0);
                    Thread.Sleep(500);
                    imageMatching.ImageMatchClick(password.Substring(5, 1), "", handleName, 0, 0, 0, 0);
                    Thread.Sleep(500);
                }
                if (imageMatching.ImageMatch("�������Ȯ��", "", handleName, 0, 0, 0, 0))
                {
                    MessageBox.Show("���ż���");
                }
                goto Watitng;
            }


            private void Credit()
        {
        Credit:
            //�ֹ� ����â Ȯ��(��ۺ�ȳ�)

            goto Credit;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Start_Click(object sender, EventArgs e)
        {
            StartMacro = new Thread(Main);
            StartMacro.Start();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            try
            {
                StartMacro.Abort();
                StartMacro.Join();
            }
            catch (Exception err)
            {
                Console.WriteLine("b1c1");
                Console.WriteLine(err.Message, ToString());
            }

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

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        public void TNF_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process[] mProcess = System.Diagnostics.Process.GetProcessesByName(Application.ProductName);
            foreach (System.Diagnostics.Process p in mProcess)
                p.Kill();
        }
    }
}