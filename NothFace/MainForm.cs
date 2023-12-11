using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Threading;

namespace NothFace
{
    public partial class MainForm : Form
    {

        [System.Runtime.InteropServices.DllImport("User32", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        // ���� �÷���
        private static bool isThreadRunning = true;
        System.Threading.Thread StartMacro1;
        System.Threading.Thread StartMacro2;
        System.Threading.Thread StartMacro3;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Setting(string id)
        {

            string macroId = id;
            string password = "";
            string size = "";
            int x = 0, y = 0, width = 0, height = 0;
            macroId = id;
            string handleName = "TheNothFace" + macroId;
            MouseUtile mouseUtile = new MouseUtile(id);
            KeyboardUtile keyboardUtile;
            WindowUtile windowUtile;
            ImageMatching imageMatching = new ImageMatching(id);
            keyboardUtile = new KeyboardUtile();
            windowUtile = new WindowUtile();

            if (id == "1")
            {
                password = passwordBox1.Text;
                size = sizeBox1.Text;
            }
            else if (id == "2")
            {
                password = passwordBox2.Text;
                size = sizeBox2.Text;
            }
            else if (id == "3")
            {
                password = passwordBox3.Text;
                size = sizeBox3.Text;
            }
            Debug.WriteLine(id);
            Debug.WriteLine(password);
            Debug.WriteLine(size);
            Thread.Sleep(10);
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
            if (size == "")
            {
                MessageBox.Show("xs, s, m, l,lx,lxx ���ϳ��� �׾��ּ���(��ҹ��� ����)");
                return;
            }



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

            Watiting(handleName, password, height, width, size, x, imageMatching, mouseUtile);
        }

        private void Watiting(string handleName, string password, int height, int width, string size, int x, ImageMatching imageMatching, MouseUtile mouseUtile)
        {
        Watitng:
            Thread.Sleep(100);
            imageMatching.ImageMatchClick("������", "", handleName, 0, 0, 0, 0);
            imageMatching.ImageMatchClick("�۴ݱ�", "", handleName, 0, 0, 0, 0);
            imageMatching.ImageMatchClick("����ó��", "", handleName, 0, 0, 0, 0);
            imageMatching.ImageMatchClick("�۴ٽÿ���", "", handleName, 0, 0, 0, 0);
            imageMatching.ImageMatchClick("����������", "", handleName, 0, 0, 0, 0);
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
                imageMatching.ImageMatch("��ũ��4", "", handleName, 0, 0, 0, 0) ||
                imageMatching.ImageMatch("��ũ��5", "", handleName, 0, 0, 0, 0))
            {
                // ���콺 �ش� ȭ�� �߰����� �̵�, �� ������
                mouseUtile.MoveTo(width / 2 + x, height / 2);
                mouseUtile.wheelDown();
                Thread.Sleep(500);
                mouseUtile.wheelDown();
                Thread.Sleep(500);
            }
            imageMatching.ImageMatchClick("�����ϱ�1", "", handleName, 0, 0, 0, 0);
            imageMatching.ImageMatchClick("�����ϱ�2", "", handleName, 0, 0, 0, 0);
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Start1_Click(object sender, EventArgs e)
        {
            string handleName = "TheNothFace1";
            IntPtr findwindow = FindWindow(null, handleName);
            if (!findwindow.Equals(IntPtr.Zero))
            {
                StartMacro1 = new Thread(new ThreadStart(() => Setting("1")));
                StartMacro1.Start();
            }
            Thread.Sleep(1000);
            handleName = "TheNothFace2";
            findwindow = FindWindow(null, handleName);
            if (!findwindow.Equals(IntPtr.Zero))
            {
                StartMacro2 = new Thread(new ThreadStart(() => Setting("2")));
                StartMacro2.Start();
            }
            Thread.Sleep(1000);
            handleName = "TheNothFace3";
            findwindow = FindWindow(null, handleName);
            if (!findwindow.Equals(IntPtr.Zero))
            {
                StartMacro3 = new Thread(new ThreadStart(() => Setting("3")));
                StartMacro3.Start();
            }
            //StartMacro1 = new Thread(new ThreadStart(() => Setting("1")));
            //StartMacro1.Start();
        }
        private void Start2_Click(object sender, EventArgs e)
        {
            StartMacro2 = new Thread(new ThreadStart(() => Setting("2")));
            StartMacro2.Start();
        }
        private void Start3_Click(object sender, EventArgs e)
        {
            StartMacro3 = new Thread(new ThreadStart(() => Setting("3")));
            StartMacro3.Start();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void passwordBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void passwordBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}