using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;

namespace FillFlex
{
    public partial class SingleRecord : Form
    {
        public SingleRecord()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void setId(string value) { 
            SelId.Text = value;
        }

        public void setPlcId(string value)
        {
            SelPlcId.Text = value;
        }

        public void setSts(string value)
        {
            SelSts.Text = value;
        }

        public void setDtc(string value)
        {
            SelDtc.Text = value;
        }

        public void setDts(string value)
        {
            SelDts.Text = value;
        }

        public void setDtf(string value)
        {
            SelDtf.Text = value;
        }

        public void setSetp(string value)
        {
            SelSetp.Text = value;
        }

        public void setMass(string value)
        {
            SelMass.Text = value;
        }

        public void setVol(string value)
        {
            SelVol.Text = value;
        }

        public void setCns(string value)
        {
            SelCns.Text = value;
        }

        public void setCnf(string value)
        {
            SelCnf.Text = value;
        }

        public void setTemp(string value)
        {
            SelTemp.Text = value;
        }

        public void setDens(string value)
        {
            SelDens.Text = value;
        }

        public void setTankId(string value)
        {
            SelTankId.Text = value;
        }

        public void setTransp(string value)
        {
            SelTranspt.Text = value;
        }

        public void setDriver(string value)
        {
            SelDrivert.Text = value;
        }

        public void setCustomer(string value)
        {
            SelCustomert.Text = value;
        }

        public void setSourceTank(string value)
        {
            SelSourceTank.Text = value;
        }

        public void setSelDiff(string value)
        {
            SelDiff.Text = value;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font headerFont = new Font("Times", 14, FontStyle.Bold);
            Font textFont = new Font("Courier", 10, FontStyle.Regular);
            Font basicFont = new Font("Arial", 9, FontStyle.Regular);
            Font labelFont = new Font("Arial", 10, FontStyle.Underline);
            Font smallFont = new Font("Arial", 8, FontStyle.Regular);
            Font basicBoldFont = new Font("Arial", 9, FontStyle.Bold);
            Single yStart, xLeft = 200.0F, yStep = 16.0F;
            int templateRows;

            e.Graphics.DrawString("Звіт по наливу №" + this.SelId.Text + " : " + this.SelPlcId.Text, headerFont, Brushes.Black, 330.0F, 40.0F);
            e.Graphics.DrawLine(Pens.Black, 100.0F, 80.0F, 780.0F, 80.0F);
            string sPath = "C:/FillFlex/ReportTemplate.xml";
            System.IO.FileInfo TemplateFile = new System.IO.FileInfo(sPath);

            if(TemplateFile.Exists){
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
                FileStream fs = new FileStream(sPath, FileMode.Open, FileAccess.Read);
                xmldoc.Load(fs);
                XmlNodeList xmlnode = xmldoc.GetElementsByTagName("header");
                templateRows = xmlnode.Item(0).ChildNodes.Count;
                for(int i = 0; i < templateRows; i++){
                    e.Graphics.DrawString(xmlnode.Item(0).ChildNodes.Item(i).InnerText, basicFont, Brushes.Black, 100.0F, 100.0F + i * (basicFont.Height + 1));
                }
                yStart = templateRows * basicFont.Height + 100.0F;

                xmlnode = xmldoc.GetElementsByTagName("footer");
                templateRows = xmlnode.Item(0).ChildNodes.Count;
                for(int i = templateRows - 1; i > -1; i--){
                    e.Graphics.DrawString(xmlnode.Item(0).ChildNodes.Item(templateRows - 1 - i).InnerText, basicFont, Brushes.Black, 100.0F, 1000.0F - i * (labelFont.Height + 0.4F));
                }
                fs.Close();
                fs.Dispose();

                e.Graphics.DrawString("Ідентифікаційний номер наливу........ " + this.SelPlcId.Text, textFont, Brushes.Black, xLeft, yStart + 3 * yStep);
                e.Graphics.DrawString("Ідентифікаційний номер флексітанку... " + this.SelTankId.Text, textFont, Brushes.Black, xLeft, yStart + 4 * yStep);
                e.Graphics.DrawString("Бак.................................. " + this.SelSourceTank.Text, textFont, Brushes.Black, xLeft, yStart + 5 * yStep);
                e.Graphics.DrawString("Статус............................... " + this.SelSts.Text, textFont, Brushes.Black, xLeft, yStart + 6 * yStep);
                e.Graphics.DrawString("Дата/час створення................... " + this.SelDtc.Text, textFont, Brushes.Black, xLeft, yStart + 7 * yStep);
                e.Graphics.DrawString("Дата/час запуску..................... " + this.SelDts.Text, textFont, Brushes.Black, xLeft, yStart + 8 * yStep);
                e.Graphics.DrawString("Дата/час завершення.................. " + this.SelDtf.Text, textFont, Brushes.Black, xLeft, yStart + 9 * yStep);
                e.Graphics.DrawString("Задана маса наливу, кг............... " + this.SelSetp.Text, textFont, Brushes.Black, xLeft, yStart + 10 * yStep);
                e.Graphics.DrawString("Факт. маса наливу, кг................ " + this.SelMass.Text, textFont, Brushes.Black, xLeft, yStart + 11 * yStep);
                e.Graphics.DrawString("Факт. об'єм наливу, л................ " + this.SelVol.Text, textFont, Brushes.Black, xLeft, yStart + 12 * yStep);
                e.Graphics.DrawString("Температура наливу, °С............... " + this.SelTemp.Text, textFont, Brushes.Black, xLeft, yStart + 13 * yStep);
                e.Graphics.DrawString("Густина наливу, кг/л................. " + this.SelDens.Text, textFont, Brushes.Black, xLeft, yStart + 14 * yStep);
                e.Graphics.DrawString("Поч. показн. лічильника, кг.......... " + this.SelCns.Text, textFont, Brushes.Black, xLeft, yStart + 15 * yStep);
                e.Graphics.DrawString("Кін. показн. лічильника, кг.......... " + this.SelCnf.Text, textFont, Brushes.Black, xLeft, yStart + 16 * yStep);
                e.Graphics.DrawString("", textFont, Brushes.Black, xLeft, yStart + 17 * yStep);
                e.Graphics.DrawString("Дані перевезення:", basicBoldFont, Brushes.Black, 330.0F, yStart + 18 * yStep);
                e.Graphics.DrawString("", textFont, Brushes.Black, xLeft, yStart + 19 * yStep);
                if (this.SelCustomert.Text != "") {
                    e.Graphics.DrawString("Замовник.................. " + this.SelCustomert.Text, textFont, Brushes.Black, xLeft, yStart + 20 * yStep);
                }
                if (this.SelTranspt.Text != "")
                {
                    e.Graphics.DrawString("Транспорт................. " + this.SelTranspt.Text, textFont, Brushes.Black, xLeft, yStart + 21 * yStep);
                }
                if (this.SelDrivert.Text != "")
                {
                    e.Graphics.DrawString("Водій..................... " + this.SelDrivert.Text, textFont, Brushes.Black, xLeft, yStart + 22 * yStep);
                }
                e.Graphics.DrawLine(Pens.DarkGray, 100.0F, 1110.0F, 780.0F, 1110.0F);
                e.Graphics.DrawString("Надруковано " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), smallFont, Brushes.DarkGray, 600.0F, 1125.0F);
            }
            else{
                MessageBox.Show("Файла шаблону звіту не існує", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void prntBtn_Click(object sender, EventArgs e)
        {
            this.printSingleRep.DefaultPageSettings.Margins = new Margins(20, 20, 20, 30);
            this.printSingleRep.DocumentName = "SingleReport";
            this.printDialog1.Document = this.printSingleRep;
            if(printDialog1.ShowDialog() == DialogResult.OK){
                this.printSingleRep.Print();
                this.Close();
            }
        }

        private void LoadWindow(object sender, EventArgs e)
        {
            prntBtn.Focus();
        }


    }
}
