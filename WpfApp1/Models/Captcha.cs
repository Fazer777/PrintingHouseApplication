using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouseApplication.Models
{
    class Captcha
    {
        private string _strCaptcha;
        private string _allowChar;
        private string _allowDigitalChar;
        private Random _random = new Random();

        public Captcha()
        {
            _allowChar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            _allowDigitalChar = "1,2,3,4,5,6,7,8,9,0";
        }

        public string Generate(int N)
        {
            _strCaptcha = string.Empty;
            string temp = _allowChar.ToLower() + "," + _allowDigitalChar;
            string[] separChar = temp.Split(',');
            for (int i = 0; i < N; ++i)
            {
                temp = separChar[(_random.Next(0, separChar.Length))];
                _strCaptcha += temp;
            }
            return _strCaptcha;
        }
    }
}
