using System.Diagnostics;

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

        System.Threading.Thread StartMacro;
        
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
        //ǰ��, �ٷα��� �� Ȯ�εɋ� ���� ���
        Watitng:
            Thread.Sleep(100);
            if (imageMatching.ImageMatch("�ٷα���", "", handleName, 0, 0, 0, 0))
            {
                imageMatching.ImageMatchClick("�ٷα���", "", handleName, 0, 0, 0, 0);
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
                    goto Newfix;
                }
                else { 
                    Credit();
                }
                

            }
            if (imageMatching.ImageMatch("ǰ��", "", handleName, 0, 0, 0, 0))
            {
                goto Newfix;
            }
            goto Watitng;
        Newfix://���ΰ�ħ
            mouseUtile.InClick(472, 865); // �ֱٺ���ǰŬ��
            //ȭ�� ���� Ȯ��(��ü����) Ȯ�εɋ����� ���
            while (!imageMatching.ImageMatch("��ü����", "", handleName, 0, 0, 0, 0))
            {
                Thread.Sleep(1000);
            }
            Thread.Sleep(2000);
            mouseUtile.InClick(130, 300);//ù��°��ǰŬ��
            goto Watitng;
        }

        private void Credit()
        {
            //�ֹ� ����â Ȯ��(��ۺ�ȳ�)
            while (!imageMatching.ImageMatch("��ۺ�ȳ�", "", handleName, 0, 0, 0, 0))
                Thread.Sleep(1000);
            Scroll1:
            // ���콺 �ش� ȭ�� �߰����� �̵�, �� ������
            mouseUtile.MoveTo(width / 2 + x, height / 2);
            mouseUtile.wheelDown();
            Thread.Sleep(2000);
            mouseUtile.wheelDown();
            if (imageMatching.ImageMatch("��ۺ�ȳ�", "", handleName, 0, 0, 0, 0))
            {
                goto Scroll1;
                
            }
            else if(imageMatching.ImageMatch("�����ϱ�1", "", handleName, 0, 0, 0, 0)){
                imageMatching.ImageMatchClick("�����ϱ�1", "", handleName, 0, 0, 0, 0);
            }


            
            // �����ϱ� ��ư Ȯ���ϸ� Ŭ�� �� 3���� �ݺ�
            while (!imageMatching.ImageMatch("����", "", handleName, 0, 0, 0, 0))
                Thread.Sleep(1000);
            Scroll2:
            mouseUtile.MoveTo(width / 2 + x, height / 2);
            mouseUtile.wheelDown();
            Thread.Sleep(2000);
            mouseUtile.wheelDown();
            if (imageMatching.ImageMatch("����", "", handleName, 0, 0, 0, 0))
            {
                goto Scroll2;
            }
            else {
                if (imageMatching.ImageMatch("�����մϴ�", "", handleName, 0, 0, 0, 0)) {
                    imageMatching.ImageMatchClick("�����մϴ�", "", handleName, 0, 0, 0, 0);
                }
            }
            imageMatching.ImageMatchClick("�ֹ��ϱ�", "", handleName, 0, 0, 0, 0);


            
            while (!imageMatching.ImageMatch("����Ȯ��", "", handleName, 0, 0, 0, 0))
                Thread.Sleep(1000);
            Scroll3:
            mouseUtile.MoveTo(width / 2 + x, height / 2);
            mouseUtile.wheelDown();
            Thread.Sleep(2000);
            mouseUtile.wheelDown();
            if (imageMatching.ImageMatch("����Ȯ��", "", handleName, 0, 0, 0, 0))
            {
                goto Scroll3;
            }
            else
            {
                if (imageMatching.ImageMatch("���̹������ϱ�", "", handleName, 0, 0, 0, 0))
                {
                    imageMatching.ImageMatchClick("���̹������ϱ�", "", handleName, 0, 0, 0, 0);
                }
            }

            while (!imageMatching.ImageMatch("���̹���й�ȣâȮ��", "", handleName, 0, 0, 0, 0))
                Thread.Sleep(1000);

            Thread.Sleep(500);
            imageMatching.ImageMatchClick(password.Substring(0,1), "", handleName, 0, 0, 0, 0);
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

            int check = 0;
            while (!imageMatching.ImageMatch("�������Ȯ��", "", handleName, 0, 0, 0, 0)) {
                Thread.Sleep(1000);
                check += 1;
                if (check > 20) {
                    MessageBox.Show("���Ž���");
                }
            }
            MessageBox.Show("���ż���");


            //�����ϱ� Ŭ��������
            // ���콺 �ش� ȭ�� �߰����� �̵�, �� ������
            //�����մϴ� Ȯ�εǸ� Ư�� ��ǥ Ŭ��
            // �ֹ��ϱ� Ŭ��

            // ���콺 �ش� ȭ�� �߰����� �̵�, �� ������
            //���̹����� �����ϱ� Ŭ��

            //���� ��й�ȣâ Ȯ��
            //��й�ȣ 6�ڸ� Ŭ��

            //�����Ϸ��ִ� 20�ʰ� Ȯ�� ���� ���� �Ϸ� / ���� ���� ����
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Start_Click(object sender, EventArgs e)
        {
            StartMacro = new Thread(new ThreadStart(Main));
            StartMacro.ApartmentState = ApartmentState.STA;
            StartMacro.Start();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            try
            {
                StartMacro.Suspend();
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
    }
}