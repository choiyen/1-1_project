using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<Addres> addr;//메모리상에 보관
        private string dir = System.IO.Path.Combine(Application.StartupPath, "Myaddress.txt"); //경로를 묵을때 편함
        int count = 0;
        public Form1()
        {
            InitializeComponent();
            addr = new List<Addres>(); //처음 생성 될 때 파일 초기화
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(dir)) //파일이 있으면
            {
                LoadData();
            }
            this.sslCount.Text = "등록수" + addr.Count.ToString();

            if (addr.Count > 0)
            {
                ShowRecord(0); //첫번째 데이터를 표시
            }
            btnOK.Text = "추가";
        }
        private void LoadData()
        {
            StreamReader sr = new StreamReader(dir, Encoding.Default);

            while (sr.Peek() >= 0) //-1 : 더이상 읽을 문자가 없을때
            {
                string[] arr = sr.ReadLine().Trim().Split(',');//한줄만 읽기 전체는 ReadEnd, Split는 콤마를 구분으로 나누자

                if (arr[0] != "" && arr[0] != null)
                {
                    Addres a = new Addres();
                    a.Num = Convert.ToInt32(arr[0]);//번호 : 인덱스+1
                    a.Name = arr[1];
                    a.Birthday = arr[2];
                    a.Mobile = arr[3];
                    a.Email = arr[4];
                    a.Phone = arr[5];
                    a.Zipcode = arr[6];
                    a.Address = arr[7];
                    a.CompanyName = arr[8];
                    a.Position = arr[9];
                    a.CompanyNumber = arr[10];
                    a.Fax = arr[11];
                    a.CompanyZipcode = arr[12];
                    a.CompanyAddress = arr[13];
                    addr.Add(a);
                    
                }
                count++;
            }
            
            sr.Close();
            sr.Dispose();
            DisplayData();
        }
        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = Path.GetFileNameWithoutExtension(dir);
            string ext = Path.GetExtension(dir).Replace(".", "");

            string newDir =
                Path.Combine(Application.StartupPath,
                    String.Format("{0}{1}.{2}"
                        , name
                        , String.Format("{0}{1:0#}{2}"
                            , DateTime.Now.Year
                            , DateTime.Now.Month
                            , DateTime.Now.Day.ToString().PadLeft(2, '0')
                          )
                        , ext
                    )
                );

            if (File.Exists(dir))
            {
                File.Copy(dir, newDir, true); // 원본을 복사해서 백업
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "입력" && txtName.Text != ""&& textBox1.Text != ""&& textBox2.Text !=""&& textBox3.Text != ""
                && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox8.Text != "" && textBox7.Text != ""
                && textBox9.Text != "" && textBox11.Text != "" && textBox12.Text != "" && textBox13.Text !="" )

            {
                Addres a = new Addres();

                a.Num = addr.Count + 1;
                a.Name = txtName.Text.Trim();
                a.Birthday = textBox4.Text.Trim();
                a.Mobile = textBox1.Text.Trim();
                a.Email = textBox5.Text.Trim();
                a.Phone = textBox2.Text.Trim();
                a.Zipcode = textBox6.Text.Trim();
                a.Address = textBox3.Text.Trim();


                a.CompanyName = textBox7.Text.Trim();
              
                a.Position = textBox8.Text.Trim();
                a.CompanyNumber = textBox11.Text.Trim();
                a.Fax = textBox9.Text.Trim();
                a.CompanyZipcode = textBox12.Text.Trim();
                a.CompanyAddress = textBox13.Text.Trim();
                addr.Add(a);
                count++;
                DisplayData();//출력
            }//입력된 값이 값을 빼내어 출력을 작동함.
            else if(btnOK.Text != "입력")
            {
                ClerTextBox();
                btnOK.Text = "입력";
            }
            else
            {
                MessageBox.Show("경고 , 값이 비어있어 입력이 불가합니다.");
            }
        }
        private void ClerTextBox()//파일의 내용을 제거하는 함수
        {
            txtName.Text =
            textBox1.Text =
            textBox2.Text =
            textBox3.Text =
            textBox4.Text = 
            textBox5.Text = 
            textBox6.Text = 
            textBox8.Text = 
            textBox7.Text = 
            textBox9.Text = 
            textBox11.Text = 
            textBox12.Text = 
            textBox13.Text = 
            string.Empty;

        }
        private void DisplayData()
        {
            var q = (from a in addr select a).ToList();
            this.dataGridView.DataSource = q;
        }
        private void DisplayData(string query)
        {
            var q = (from a in addr
                     where
                         a.Name == query ||
                         a.Mobile == query ||
                         a.Email == query ||
                         a.Birthday == query ||
                         a.Mobile == query ||
                         a.Email == query ||
                         a.Phone == query ||
                         a.Zipcode == query ||
                         a.Address == query ||
                         a.CompanyName == query ||
                         a.Position == query ||
                         a.CompanyNumber == query ||
                         a.Fax == query ||
                         a.CompanyZipcode == query ||
                         a.CompanyAddress == query

            select a).ToList();
            this.dataGridView.DataSource = q;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (addr.Count > 0)
            {
                SaveData();
            }
        }
        private void SaveData()//txt 파일에다가 저장함.
        {
            //string dir = @"D:\Temp\MyAddress.txt";// 생성
            //string dir = Application.StartupPath + "\\myAddress.txt"; //실행폴더에 텍스트 생성  

            StringBuilder sb = new StringBuilder();

            int index = 0; //인덱스 재정렬

            foreach (Addres a in addr)
            {
                sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}\r\n"
                    , ++index, a.Name,a.Birthday, a.Mobile, a.Email,a.Phone,a.Zipcode,a.Address,a.CompanyName,a.Position,a.CompanyNumber,a.Fax,a.CompanyZipcode,a.CompanyZipcode));
            }
            StreamWriter sw = new StreamWriter(dir, false, Encoding.Default);
            sw.WriteLine(sb.ToString());
            sw.Close();
            sw.Dispose(); // 
            MessageBox.Show("저장되었습니다.");
        }
        private int currentIndex = -1;//현재의 인덱스 넘버 만들기.
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //현재 선택된 인덱스 + 1을 번호 출력
            //순서1
            //this.txtNum.Text = (e.RowIndex + 1).ToString();//필드 사용전

           
            DataGridViewCell dgvc = dataGridView.Rows[e.RowIndex].Cells[0];
            currentIndex = Convert.ToInt32(dgvc.Value.ToString()) - 1;

            if (currentIndex != -1)//순서4
            {
                ShowRecord(currentIndex);
            }
        }
        private void ShowRecord(int index)
        {
            this.txtName.Text = addr[index].Name;
            this.textBox4.Text = addr[index].Birthday;
            this.textBox1.Text = addr[index].Mobile;
            this.textBox5.Text = addr[index].Email;
            this.textBox2.Text = addr[index].Phone;
            this.textBox6.Text = addr[index].Zipcode;
            this.textBox3.Text = addr[index].Address;
            this.textBox7.Text = addr[index].CompanyName;
          
            this.textBox8.Text = addr[index].Position;
            this.textBox11.Text = addr[index].CompanyNumber;
            this.textBox9.Text = addr[index].Fax;
            this.textBox12.Text = addr[index].CompanyZipcode;
            this.textBox13.Text = addr[index].CompanyAddress;

            btnOK.Text = "추가";
            txtGo.Text = (index + 1).ToString(); //현재 선택된 인덱스 값 출력
        }
       
        private void btnModify_Click(object sender, EventArgs e)
        {
            if (currentIndex != -1 &&blnModified == true)
            {
                addr[currentIndex].Num = currentIndex + 1;
                addr[currentIndex].Name = txtName.Text;
                addr[currentIndex].Birthday = textBox4.Text;
                addr[currentIndex].Mobile = textBox1.Text;
                addr[currentIndex].Email = textBox5.Text;
                addr[currentIndex].Phone = textBox2.Text;
                addr[currentIndex].Zipcode = textBox6.Text;
                addr[currentIndex].Address = textBox3.Text;
                addr[currentIndex].CompanyName = textBox7.Text;
                addr[currentIndex].Position = textBox8.Text;
                addr[currentIndex].CompanyNumber = textBox11.Text;
                addr[currentIndex].Fax = textBox9.Text;
                addr[currentIndex].CompanyZipcode = textBox12.Text;
                addr[currentIndex].CompanyAddress = textBox13.Text;

                MessageBox.Show("수정되었습니다.", "수정완료");
                DisplayData();//다시 로드

                blnModified = false;
            }
        }
        private bool blnModified = false;
        private void 종료XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == btnFirst)
            {
                if (currentIndex > 0)
                {
                    currentIndex = 0; //0번째 인덱스로 표시
                }
            }
            else if (btn == btnPrev)
            {
                if (currentIndex > 0)
                {
                    currentIndex--;
                }
            }
            else if (btn == btnNext)
            {
                if (currentIndex < addr.Count - 1)
                {
                    currentIndex++;
                }
            }
            else if (btn == btnLast)
            {
                if (currentIndex != 1)
                {
                    currentIndex = addr.Count - 1;
                }
            }
            ShowRecord(currentIndex);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (txtGo.Text != "" && Convert.ToInt32(txtGo.Text) > 0
                 && Convert.ToInt32(txtGo.Text) < addr.Count)
            {
                ShowRecord(Convert.ToInt32(txtGo.Text) - 1);
            }
        }

        private void btnSerch_Click(object sender, EventArgs e)
        {
            DisplayData(txtSerch.Text);//특정 텍스트를 검색함.
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentIndex != -1) //현재상태
            {
                DialogResult dr = MessageBox.Show("정말로 삭제하시겠습니까?", "삭제확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    //메모리 상에서 현재 레코드 삭제
                    addr.RemoveAt(currentIndex);
                    for(int i = 0; i < addr.Count; i++)
                    {
                        addr[i].Num = i+ 1;
                    }
                    DisplayData();
                    
                }

              
            }
        }//데이터 삭제.
        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtName.Text != "") //데이터가 로드된 상태에서만
            {
                blnModified = true;  //변경되었다.
            }
        }
    }
}
