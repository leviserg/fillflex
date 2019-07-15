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
    public partial class mehForm : Form
    {
        private string sName;
        private int index;
        private int sts;

        public mehForm(string sName, int index)
        {
            InitializeComponent();
            this.sName = sName;
            this.index = index;
            this.Text = sName;
            this.TopMost = true;
        }

        private void mehForm_Load(object sender, EventArgs e)
        {
            UpdTimer.Enabled = true;
            this.winlocind.Text = index.ToString();
        }

        private void mehForm_Close(object sender, FormClosingEventArgs e)
        {
            UpdTimer.Enabled = false;
            modbustcp.sendLocCmd(30, 0, 0);
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            modbustcp.sendLocCmd(30, index, 52);
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            modbustcp.sendLocCmd(30, index, 25);
        }

        private void UpdTimer_Tick(object sender, EventArgs e)
        {
            sts = GVL.registers[index];
            winsts.Text = sts.ToString();
            indb0.Image = setGreenInd(index, 0);
            indb1.Image = setGreenInd(index, 1);
            indb2.Image = setGreenInd(index, 2);
            indb3.Image = setGreenInd(index, 3);
            indb4.Image = setRedInd(index, 4);
            indb5.Image = setRedInd(index, 5);
            indb6.Image = setYellowInd(index, 6);
            indb7.Image = setYellowInd(index, 7);
            winind.Text = GVL.registers[31].ToString();
            wincmd.Text = GVL.registers[30].ToString();
        }

        // functions

        private Image setGreenInd(int register, int bit)
        {
            if (modbustcp.getBit(register, bit))
            {
                return Image.FromFile("C:/FillFlex/Images/smInd_green.png");
            }
            else
            {
                return Image.FromFile("C:/FillFlex/Images/smInd_grey.png");
            }
        }

        private Image setRedInd(int register, int bit)
        {
            if (modbustcp.getBit(register, bit))
            {
                return Image.FromFile("C:/FillFlex/Images/smInd_red.png");
            }
            else
            {
                return Image.FromFile("C:/FillFlex/Images/smInd_grey.png");
            }
        }

        private Image setYellowInd(int register, int bit)
        {
            if (modbustcp.getBit(register, bit))
            {
                return Image.FromFile("C:/FillFlex/Images/smInd_yellow.png");
            }
            else
            {
                return Image.FromFile("C:/FillFlex/Images/smInd_grey.png");
            }
        }
    }
}
