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
    public partial class RefillWind : Form
    {
        public RefillWind()
        {
            InitializeComponent();
            this.TopMost = true;
            this.CenterToScreen();
            GVL.isRefill = true;
            updtimer.Enabled = true;
        }

        private void RefOn_Click(object sender, EventArgs e)
        {
            modbustcp.sendSingleRegister(5797, 1); // set refill mode
        }

        private void RefOff_Click(object sender, EventArgs e)
        {
            modbustcp.sendSingleRegister(5797, 0); // reset refill mode
        }

        private void updtimer_Tick(object sender, EventArgs e)
        {
            if (GVL.registers[5797]!=0)
            {
                refact.Image = Image.FromFile("C:/FillFlex/Images/Ind_green.png");
            }
            else
            {
                refact.Image = Image.FromFile("C:/FillFlex/Images/Ind_grey.png");
            }
        }

        private void RefillFormClosing(object sender, FormClosingEventArgs e)
        {
            GVL.isRefill = false;
            updtimer.Enabled = false;
            this.Dispose();
        }
    }
}
