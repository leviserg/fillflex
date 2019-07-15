using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FillFlex
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.TopMost = true;
            LoginText.Focus();
        }

        private void LoginFieldEnter(object sender, EventArgs e)
        {
            LoginText.ForeColor = Color.Black;
            if (LoginText.Text == "Введіть ім'я") {
                LoginText.Text = "";
            }
        }

        private void LoginFieldLeave(object sender, EventArgs e)
        {
            if (LoginText.Text == "")
            {
                LoginText.Text = "Введіть ім'я";
                LoginText.ForeColor = Color.Gray;
            }
        }

        private void PwdFieldEnter(object sender, EventArgs e)
        {
            PwdText.ForeColor = Color.Black;
            if (PwdText.Text == "******") {
                PwdText.Text = "";
            }
        }

        private void PwdFieldLeave(object sender, EventArgs e)
        {
            if (PwdText.Text == "")
            {
                PwdText.Text = "******";
                PwdText.ForeColor = Color.Gray;
            }
        }

        private void submit_Click(object sender, EventArgs e)
        {
            if (LoginText.Text == GVL.MyLogin)
            {
                if (PwdText.Text == GVL.MyPwd)
                {
                    GVL.isAdmin = true;
                    GVL.LoginFormShow = false;
                    this.Close();
                    MessageBox.Show("Вхід виконано.", "Вхід", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //FillFlex.MainForm.loggerInstance.writeData("Виконано вхід в систему", 2);
                    //FillFlex.MainForm.
                }
                else {
                    MessageBox.Show("Введіть вірний пароль.", "Пароль", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PwdText.BackColor = Color.MistyRose;                
                }
            }
            else {
                MessageBox.Show("Введіть вірний логін.", "Логін", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoginText.BackColor = Color.MistyRose;
            }
        }

        private void LoginFormClose(object sender, FormClosingEventArgs e)
        {
            GVL.LoginFormShow = false;
        }
    }
}
