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
                MessageBox.Show("1~3까지의 숫자를 입력 해주세요");
                return;
            }
            if (password == "")
            {
                MessageBox.Show("네이버 페이 비밀번호 6자리를 입력해주세요");
                return;
            }
            if (count <= 0)
            {
                MessageBox.Show("구매 수량을 선택 해주세요");
                return;
            }
            if (size == "")
            {
                MessageBox.Show("xs, s, m, l,lx,lxx 중하나를 죽어주세요(대소문자 주의)");
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
        //품절, 바로구매 가 확인될떄 까지 대기
        Watitng:
            Thread.Sleep(100);
            if (imageMatching.ImageMatch("바로구매", "", handleName, 0, 0, 0, 0))
            {
                imageMatching.ImageMatchClick("바로구매", "", handleName, 0, 0, 0, 0);
                //화면 변경 확인(옵션선택)
                while (!imageMatching.ImageMatch("옵션선택해주세요", "", handleName, 0, 0, 0, 0))
                {
                    Thread.Sleep(500);
                }
                Thread.Sleep(1000);
                //옵션 클릭 // 1초 대기
                imageMatching.ImageMatchClick("옵션선택해주세요", "", handleName, 0, 0, 0, 0);
                Thread.Sleep(1000);
                //사이즈 클릭 // 1초 대기
                imageMatching.ImageMatchClick(size, "", handleName, 0, 0, 0, 0);

                Thread.Sleep(1000);
                //바로구매 재클릭 //
                imageMatching.ImageMatchClick("바로구매2", "", handleName, 0, 0, 0, 0);
                Thread.Sleep(1000);
                if (imageMatching.ImageMatch("사이즈없음", "", handleName, 0, 0, 0, 0))
                {
                    imageMatching.ImageMatchClick("사이즈없음닫기", "", handleName, 0, 0, 0, 0);
                    Thread.Sleep(500);
                    imageMatching.ImageMatchClick("옵션닫기", "", handleName, 0, 0, 0, 0);
                    Thread.Sleep(500);
                    goto Newfix;
                }
                else { 
                    Credit();
                }
                

            }
            if (imageMatching.ImageMatch("품절", "", handleName, 0, 0, 0, 0))
            {
                goto Newfix;
            }
            goto Watitng;
        Newfix://새로고침
            mouseUtile.InClick(472, 865); // 최근본상품클릭
            //화면 변경 확인(전체삭제) 확인될떄까지 대기
            while (!imageMatching.ImageMatch("전체삭제", "", handleName, 0, 0, 0, 0))
            {
                Thread.Sleep(1000);
            }
            Thread.Sleep(2000);
            mouseUtile.InClick(130, 300);//첫번째상품클릭
            goto Watitng;
        }

        private void Credit()
        {
            //주문 결제창 확인(배송비안내)
            while (!imageMatching.ImageMatch("배송비안내", "", handleName, 0, 0, 0, 0))
                Thread.Sleep(1000);
            Scroll1:
            // 마우스 해당 화면 중간으로 이동, 휠 내리기
            mouseUtile.MoveTo(width / 2 + x, height / 2);
            mouseUtile.wheelDown();
            Thread.Sleep(2000);
            mouseUtile.wheelDown();
            if (imageMatching.ImageMatch("배송비안내", "", handleName, 0, 0, 0, 0))
            {
                goto Scroll1;
                
            }
            else if(imageMatching.ImageMatch("결제하기1", "", handleName, 0, 0, 0, 0)){
                imageMatching.ImageMatchClick("결제하기1", "", handleName, 0, 0, 0, 0);
            }


            
            // 결제하기 버튼 확인하면 클릭 이 3가지 반복
            while (!imageMatching.ImageMatch("쿠폰", "", handleName, 0, 0, 0, 0))
                Thread.Sleep(1000);
            Scroll2:
            mouseUtile.MoveTo(width / 2 + x, height / 2);
            mouseUtile.wheelDown();
            Thread.Sleep(2000);
            mouseUtile.wheelDown();
            if (imageMatching.ImageMatch("쿠폰", "", handleName, 0, 0, 0, 0))
            {
                goto Scroll2;
            }
            else {
                if (imageMatching.ImageMatch("동의합니다", "", handleName, 0, 0, 0, 0)) {
                    imageMatching.ImageMatchClick("동의합니다", "", handleName, 0, 0, 0, 0);
                }
            }
            imageMatching.ImageMatchClick("주문하기", "", handleName, 0, 0, 0, 0);


            
            while (!imageMatching.ImageMatch("페이확인", "", handleName, 0, 0, 0, 0))
                Thread.Sleep(1000);
            Scroll3:
            mouseUtile.MoveTo(width / 2 + x, height / 2);
            mouseUtile.wheelDown();
            Thread.Sleep(2000);
            mouseUtile.wheelDown();
            if (imageMatching.ImageMatch("페이확인", "", handleName, 0, 0, 0, 0))
            {
                goto Scroll3;
            }
            else
            {
                if (imageMatching.ImageMatch("네이버결제하기", "", handleName, 0, 0, 0, 0))
                {
                    imageMatching.ImageMatchClick("네이버결제하기", "", handleName, 0, 0, 0, 0);
                }
            }

            while (!imageMatching.ImageMatch("네이버비밀번호창확인", "", handleName, 0, 0, 0, 0))
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
            while (!imageMatching.ImageMatch("정상결제확인", "", handleName, 0, 0, 0, 0)) {
                Thread.Sleep(1000);
                check += 1;
                if (check > 20) {
                    MessageBox.Show("구매실패");
                }
            }
            MessageBox.Show("구매성공");


            //결제하기 클릭했으면
            // 마우스 해당 화면 중간으로 이동, 휠 내리기
            //동의합니다 확인되면 특정 좌표 클릭
            // 주문하기 클릭

            // 마우스 해당 화면 중간으로 이동, 휠 내리기
            //네이버페이 결제하기 클릭

            //결제 비밀번호창 확인
            //비밀번호 6자리 클릭

            //결제완료최대 20초간 확인 이후 결제 완료 / 결제 실패 띄우기
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