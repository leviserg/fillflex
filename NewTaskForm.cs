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
    public partial class NewTaskForm : Form
    {
        //private database LocalDatabase = new database();
        public NewTaskForm()
        {
            InitializeComponent();
            setptooltip.Text = "0..." + GVL.FillMaxLimit.ToString() + " кг";
            this.CenterToScreen();
            this.TopMost = true;
            SourceTankBox.Items.Add("...");
            for(int i = 1; i <= GVL.TankNum;i++){
                SourceTankBox.Items.Add(i.ToString());
            }
            SourceTankBox.SelectedIndex = 0;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            if (this.SetpVal.Value == 0 || this.SetpVal.Value > GVL.FillMaxLimit)
            {
                MessageBox.Show("Введіть значення від 0 до " + GVL.FillMaxLimit.ToString(), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetpVal.BackColor = Color.Yellow;
            }
            else if (this.IdLabel.Text.Length == 0
                || this.IdLabel.Text.IndexOf(".")!=-1
                || this.IdLabel.Text.IndexOf(",")!=-1
                || this.IdLabel.Text.IndexOf(".") != -1
                || this.IdLabel.Text.IndexOf("-") != -1
                || this.IdLabel.Text.IndexOf(";") != -1
                || this.IdLabel.Text.IndexOf("&") != -1
                || this.IdLabel.Text.IndexOf("`") != -1
                || this.IdLabel.Text.IndexOf("?") != -1
                || this.IdLabel.Text.IndexOf("!") != -1
                || this.IdLabel.Text.IndexOf("|") != -1
                || this.IdLabel.Text.IndexOf("'") != -1)
            {
                MessageBox.Show("Введіть код наливу (без символів -,',:,;,.,`,&,?,!,|).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IdLabel.BackColor = Color.Yellow;
            }
            else if (this.tankIdLabel.Text.Length == 0
                || this.tankIdLabel.Text.IndexOf(".") != -1
                || this.tankIdLabel.Text.IndexOf(",") != -1
                || this.tankIdLabel.Text.IndexOf(".") != -1
                || this.tankIdLabel.Text.IndexOf("-") != -1
                || this.tankIdLabel.Text.IndexOf(";") != -1
                || this.tankIdLabel.Text.IndexOf("&") != -1
                || this.tankIdLabel.Text.IndexOf("`") != -1
                || this.tankIdLabel.Text.IndexOf("?") != -1
                || this.tankIdLabel.Text.IndexOf("!") != -1
                || this.tankIdLabel.Text.IndexOf("|") != -1
                || this.tankIdLabel.Text.IndexOf("'") != -1)
            {
                MessageBox.Show("Введіть код флексітанку (без символів -,',:,;,.,`,&,?,!,|).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tankIdLabel.BackColor = Color.Yellow;
            }
            else if (this.SourceTankBox.SelectedIndex == 0)
            {
                MessageBox.Show("Виберіть номер баку", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SourceTankBox.BackColor = Color.Yellow;
            }
            else{
                string sMessage = "Перевірте введений код наливу:" + Environment.NewLine;
                sMessage += IdLabel.Text + Environment.NewLine;
                sMessage += "    Код флексітанку:" + Environment.NewLine;
                sMessage += tankIdLabel.Text + Environment.NewLine;
                sMessage += "    Бак №: " + SourceTankBox.SelectedIndex + Environment.NewLine;
                sMessage += "    Завдання: " + SetpVal.Value.ToString() + " кг." + Environment.NewLine +"      Вірно?";
                DialogResult res = MessageBox.Show(sMessage, "Перевірка", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    double dSetp = double.Parse(SetpVal.Value.ToString());
                    string sTransp = transpField.Text;
                    if (transpField.Text == "введіть номер транспорту") {
                        sTransp = "";
                    }
                    string sDriver = driverField.Text;
                    if (driverField.Text == "введіть П.І.Б водія")
                    {
                        sDriver = "";
                    }
                    string sCustomer = customerField.Text;
                    if (customerField.Text == "введіть назву Замовника")
                    {
                        sCustomer = "";
                    }
                    FillFlex.MainForm.dbInstance.InsRecord(dSetp, IdLabel.Text, tankIdLabel.Text, sTransp, sDriver, sCustomer, SourceTankBox.SelectedIndex);
                    GVL.dbrec = true;
                    this.Close();
                }
            }
        }

        private void TranspEnter(object sender, EventArgs e)
        {
            transpField.ForeColor = Color.Black;
            if (transpField.Text == "введіть номер транспорту")
            {
                transpField.Text = "";
            }
        }

        private void TranspLeave(object sender, EventArgs e)
        {
            if (transpField.Text == "")
            {
                transpField.Text = "введіть номер транспорту";
                transpField.ForeColor = Color.Gray;
            }
        }

        private void driverEnter(object sender, EventArgs e)
        {
            driverField.ForeColor = Color.Black;
            if (driverField.Text == "введіть П.І.Б водія")
            {
                driverField.Text = "";
            }
        }

        private void driverLeave(object sender, EventArgs e)
        {
            if (driverField.Text == "")
            {
                driverField.Text = "введіть П.І.Б водія";
                driverField.ForeColor = Color.Gray;
            }
        }

        private void customerEnter(object sender, EventArgs e)
        {
            customerField.ForeColor = Color.Black;
            if (customerField.Text == "введіть назву Замовника")
            {
                customerField.Text = "";
            }
        }

        private void customerLeave(object sender, EventArgs e)
        {
            if (customerField.Text == "")
            {
                customerField.Text = "введіть назву Замовника";
                customerField.ForeColor = Color.Gray;
            }
        }
    }
}
