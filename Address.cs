
using System;

namespace WindowsFormsApp1
{
    class Addres
    {
        public int Num { get; set; } // 순번0
        public string Name { get; set; } // 이름1
        public string Birthday { get; set; }// 생일2
        public string Mobile { get; set; }//휴대폰3
        public string Email { get; set; }//이메일4
        public string Phone { get; set; }//전화번호5
        public string Zipcode { get; set; }//우편번호6
        public string Address { get; set; }// 집 주소7

        public string CompanyName { get; set; }// 회사명8
        public string Position { get; set; } // 직위10
        public string CompanyNumber { get; set; }//회사번호11
        public string Fax { get; set; }//fax12
        public string CompanyZipcode { get; set; }//우편번호13
        public string CompanyAddress { get; set; }//회사주소14
    }// 값을 저장해두는 클래스. 빼올때는  get, 들어갈 때는  set
}