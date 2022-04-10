using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region SettingsForm
        private bool isClickMouse = false;
        private Point currentPosition = new Point(0, 0);
        //button close application
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }       
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isClickMouse = true;
            currentPosition =  new Point(e.X, e.Y);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isClickMouse= false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isClickMouse) { return; }
            Point buf = new Point(this.Location.X, this.Location.Y);

            buf.X += e.X - currentPosition.X;
            buf.Y += e.Y - currentPosition.Y;
        }
        #endregion
        private bool isPoint = false;
        public bool IsNum2 = false;

        private string num1 = null;
        private string num2 = null;

        private string currentOperation = "";

        public void AddNum(string txt)
        {
            if (IsNum2)
            {
                num2 += txt;
                textResult.Text = num2;
            }
            else
            {
                num1 += txt;
                textResult.Text=num1;
            }
        }
        public void SetNum(string txt)
        {
            if (IsNum2)
            {
                num2 = txt;
                textResult.Text = num2;
            }
            else
            {
                num1 = txt;
                textResult.Text = num1;
            }
        }
        private void buttonNumberClick(object obj, EventArgs e)
        {
            var txt =((Button)obj).Text;
            {
                if (isPoint && txt == ",")  { return; } 
                if  (txt ==",") { isPoint = true; }
            }

            if (txt == "+/-")
            {
                if(textResult.Text.Length > 0)
                if(textResult.Text[0] == '-')
                {
                 textResult.Text =textResult.Text.Substring(1,textResult.Text.Length-1);
                }
                else
                    {
                        textResult.Text = "-" + textResult.Text;
                    }
                SetNum(textResult.Text);
                return;
            }
            AddNum(txt);
        }

        private void buttonOperarionClick(object obj,EventArgs e)
        {
            if(num1 == null) { if (textResult.Text.Length > 0) num1 = textResult.Text; else return; }

            IsNum2 = true;
            currentOperation = ((Button)obj).Text;
            SetResult(currentOperation);
        }
        private void SetResult(string operation)
        {
            string result = null;
            switch (operation)
            {
                case "+": { result = MathOperation.Add(num1,num2); break; }
                case "-": { result = MathOperation.Sub(num1, num2); break; }
                case "x": { result = MathOperation.Mul(num1, num2); break; }
                case "÷": { result = MathOperation.Dev(num1, num2); break; }
                case "%": { result = MathOperation.Proc(num1, num2); break; }
                case "√": { result = MathOperation.Sqr(num1); IsNum2 = false; break; }
                case "x²": { result = MathOperation.Pow(num1); IsNum2 = false; break; }
                case "1/x": { result = MathOperation.OneDev(num1); IsNum2 = false; break; }
                default:break;
            }
            OutputResult(result, operation);
            if (IsNum2) { if(result !=null) num1=result; }else { num1 = null; }
            isPoint = false;
        }
        private void OutputResult(string result,string operation)
        {
            switch (operation)
            {
                case "√": { if (num1 != null) textHistory.Text = "√" + num1 + "=";break; }
                case "x²": { if (num1 != null) textHistory.Text = "² ="; break; }
                case "1/x": { if (num1 != null) textHistory.Text = "1/" + num1 + "="; break; }
               default:
                    {
                        if(num2 !=null)
                        {
                            textHistory.Text = num1 + " " + operation + " " + num2 + "=";
                        }
                        else
                        {
                            if(num1!=null)
                            {
                                textHistory.Text = num1 + " " + operation + " ";
                                break;
                            }
                        }
                    }
                    break;
            }
            num2 = null;
            if (result!=null)
            {
                textResult.Text = result;
            }
        }
        private void buttonClear(object obj, EventArgs e)
        {
            textResult.Text = "";
            textHistory.Text = "";
            IsNum2 = false;
            currentOperation = null;
            num1 = null;
            num2 = null;
            isPoint = false;
        }
        private void buttonResultClick(object obj, EventArgs e)
        {
            SetResult(currentOperation);
            IsNum2 = false;
            num2= null;
            num1 = null;
        }
        private void buttonResetNumber(object obj, EventArgs e)
        {
            if (textResult.Text.Length <= 0) { return; }
            textResult.Text = textResult.Text.Substring(0, textResult.Text.Length - 1);
            SetNum(textResult.Text);
        }
        
    }
}
