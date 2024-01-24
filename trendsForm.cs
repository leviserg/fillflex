using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Forms;
using System.Globalization;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using LiveCharts.Events;
using Binding = System.Windows.Data.Binding;
using System.Reflection;
using System.Drawing;
using System.IO;
using System.Drawing.Printing;

namespace FillFlex
{
    public partial class trendsForm : Form
    {
        private List<DateTime> xData = new List<DateTime>();
        private List<List<double>> yData = new List<List<double>>();
        private List<List<string>> data = new List<List<string>>();
        private trendViewModel _viewModel = new trendViewModel();
        private ChartValues<DateTimePoint> l = new ChartValues<DateTimePoint>();
        private String sNullTitle = "Series";
        private bool[] datavis = new bool[8];
        private String sTitle;

        public LineSeries Line0Series { get; set; }
        public LineSeries Line1Series { get; set; }
        public LineSeries Line2Series { get; set; }
        public LineSeries Line3Series { get; set; }
        public LineSeries Line4Series { get; set; }
        public LineSeries Line5Series { get; set; }
        public LineSeries Line6Series { get; set; }
        public LineSeries Line7Series { get; set; }

        public trendsForm(List<List<string>> data, String sTitle)
        {
            InitializeComponent();
            this.data = data;
            this.sTitle = sTitle;
            this.Text = sTitle;
            foreach (List<string> val in data) {
                DateTime dt = DateTime.ParseExact(val[1], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                this.xData.Add(dt);
                List<double> dataRow = new List<double>();
                for (int i = 2; i < val.Count; i++) { // 0 - id; 1 - datetime; 2... values
                    dataRow.Add(double.Parse(val[i]));
                }
                this.yData.Add(dataRow);
            }

            SeriesCollection seriesCollection = new SeriesCollection();

            LineSeries[] Lines = InitializeArray<LineSeries>(8);// up to 8 lines

            for (int i = 0; i < this.yData[0].Count; i++)
            {
                ChartValues<DateTimePoint> record = new ChartValues<DateTimePoint>();
                for (int j = 0; j < this.xData.Count; j++)
                {
                    record.Add(new DateTimePoint(this.xData[j], this.yData[j][i]));
                }
                /*
                    string ClassName = "Line" + i.ToString() + "Series";
                    var objectDictionary = new Dictionary<string, object>();
                    objectDictionary.Add(ClassName, new LineSeries());
                    var objInstance = objectDictionary[ClassName] as LineSeries;
                    objInstance.Title = "Param " + (i + 1).ToString();
                    objInstance.Values = record;
                    objInstance.StrokeThickness = 2;
                    objInstance.AreaLimit = 0;
                    objInstance.Fill = System.Windows.Media.Brushes.Transparent;
                    seriesCollection.Add(objInstance);
                */
                Lines[i].Title = "Parameter " + (i + 1).ToString();
                Lines[i].Values = record;
                Lines[i].StrokeThickness = 2;
                Lines[i].AreaLimit = 0;
                Lines[i].Fill = System.Windows.Media.Brushes.Transparent;
                //seriesCollection.Add(Lines[i]);
            }
            
            Line0Series = Lines[0];
            Line1Series = Lines[1];
            Line2Series = Lines[2];
            Line3Series = Lines[3];
            Line4Series = Lines[4];
            Line5Series = Lines[5];
            Line6Series = Lines[6];
            Line7Series = Lines[7];

            if (!Line0Series.Title.Equals(sNullTitle))
            {
                seriesCollection.Add(Line0Series);
                chckParam0.Text = Line0Series.Title;
                chckParam0.Enabled = true;
                this.datavis[0] = true;
            }
            else {
                chckParam0.Enabled = false;
                this.datavis[0] = false;
            }
            if (!Line1Series.Title.Equals(sNullTitle))
            {
                seriesCollection.Add(Line1Series);
                chckParam1.Text = Line1Series.Title;
                chckParam1.Enabled = true;
                this.datavis[1] = true;
            }
            else
            {
                chckParam1.Enabled = false;
                this.datavis[1] = false;
            }
            if (!Line2Series.Title.Equals(sNullTitle))
            {
                seriesCollection.Add(Line2Series);
                chckParam2.Text = Line2Series.Title;
                chckParam2.Enabled = true;
                this.datavis[2] = true;
            }
            else
            {
                chckParam2.Enabled = false;
                this.datavis[2] = false;
            }
            if (!Line3Series.Title.Equals(sNullTitle))
            {
                seriesCollection.Add(Line3Series);
                chckParam3.Text = Line3Series.Title;
                chckParam3.Enabled = true;
                this.datavis[3] = true;
            }
            else
            {
                chckParam3.Enabled = false;
                this.datavis[3] = false;
            }
            if (!Line4Series.Title.Equals(sNullTitle))
            {
                seriesCollection.Add(Line4Series);
                chckParam4.Text = Line4Series.Title;
                chckParam4.Enabled = true;
                this.datavis[4] = true;
            }
            else
            {
                chckParam4.Enabled = false;
                this.datavis[4] = false;
            }
            if (!Line5Series.Title.Equals(sNullTitle))
            {
                seriesCollection.Add(Line5Series);
                chckParam5.Text = Line5Series.Title;
                chckParam5.Enabled = true;
                this.datavis[5] = true;
            }
            else
            {
                chckParam5.Enabled = false;
                this.datavis[5] = false;
            }
            if (!Line6Series.Title.Equals(sNullTitle))
            {
                seriesCollection.Add(Line6Series);
                chckParam6.Text = Line6Series.Title;
                chckParam6.Enabled = true;
                this.datavis[6] = true;
            }
            else
            {
                chckParam6.Enabled = false;
                this.datavis[6] = false;
            }
            if (!Line7Series.Title.Equals(sNullTitle))
            {
                seriesCollection.Add(Line7Series);
                chckParam7.Text = Line7Series.Title;
                chckParam7.Enabled = true;
                this.datavis[7] = true;
            }
            else
            {
                chckParam7.Enabled = false;
                this.datavis[7] = false;
            }

            cartChart.Series = seriesCollection;
     
            var ax = new Axis
            {
                LabelFormatter = _viewModel.Formatter,
                Title = "",
                Separator = new Separator { IsEnabled = true }
            };

            ax.RangeChanged += Axis_OnRangeChanged;
            cartChart.AxisX.Add(ax);
            cartChart.Zoom = ZoomingOptions.X;
            cartChart.LegendLocation = LegendLocation.Right;

            cartChart.AxisX[0].MinValue = this.xData[this.xData.Count*3/4].Ticks;
            cartChart.AxisX[0].MaxValue = this.xData[this.xData.Count - 1].Ticks;

            // ************** Scroller Chart **************

            System.Windows.Media.Color BrushColor = new System.Windows.Media.Color();
            BrushColor = System.Windows.Media.Color.FromArgb(37,48,48,48);
            SolidColorBrush myScrollBrush = new SolidColorBrush(BrushColor);
            scrollChart.DisableAnimations = false;
            scrollChart.ScrollMode = ScrollMode.X;
            scrollChart.ScrollBarFill = myScrollBrush;
            scrollChart.DataTooltip = null;
            scrollChart.Hoverable = false;
            scrollChart.DataTooltip = null;

            var auxX = new Axis
            {
                LabelFormatter = x => new DateTime((long)x).ToString("dd.MM.yyyy"),
                Separator = new Separator { IsEnabled = false },
                IsMerged = true,
                Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(152, 0, 0, 0)),
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                MinValue = this.xData[0].Ticks,
                MaxValue = this.xData[this.xData.Count - 1].Ticks
            };

            scrollChart.AxisX.Add(auxX);
            scrollChart.AxisY.Add(new Axis { Separator = new Separator { IsEnabled = true }, ShowLabels = false });

            scrollChart.Series.Add(
                new LineSeries {
                    Values = cartChart.Series[0].Values,
                    Fill = System.Windows.Media.Brushes.Silver,
                    StrokeThickness = 0,
                    PointGeometry = null,
                    AreaLimit = 0
                }
            );
                        
            var assistant = new BindingAssistant
            {
                From = cartChart.AxisX[0].MinValue,
                To = cartChart.AxisX[0].MaxValue
            };
           
            cartChart.AxisX[0].SetBinding(Axis.MinValueProperty,
                new Binding { Path = new PropertyPath("From"), Source = assistant, Mode = BindingMode.TwoWay });
            cartChart.AxisX[0].SetBinding(Axis.MaxValueProperty,
                new Binding { Path = new PropertyPath("To"), Source = assistant, Mode = BindingMode.TwoWay });
            
            scrollChart.Base.SetBinding(CartesianChart.ScrollHorizontalFromProperty,
                new Binding { Path = new PropertyPath("From"), Source = assistant, Mode = BindingMode.TwoWay });
            scrollChart.Base.SetBinding(CartesianChart.ScrollHorizontalToProperty,
                new Binding { Path = new PropertyPath("To"), Source = assistant, Mode = BindingMode.TwoWay });

            this.CenterToScreen();
            this.TopMost = true;
        }


        // ================ functions ================
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        // --------- zoom & scroll main chart ---------

        private void Axis_OnRangeChanged(RangeChangedEventArgs eventargs)
        {
            var currentRange = eventargs.Range;

            if (currentRange < TimeSpan.TicksPerDay * 2) // < 2 days; TimeSpan.TicksPerDay = 864000000000
            {
                _viewModel.Formatter = x => new DateTime((long)x).ToString("dd.MM.yyyy" + Environment.NewLine + "HH:mm:ss");
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 60) // < 2 months
            {
                _viewModel.Formatter = x => new DateTime((long)x).ToString("dd.MM.yyyy");
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 540) // < 1.5 year
            {
                _viewModel.Formatter = x => new DateTime((long)x).ToString("MM.yyyy");
                return;
            }

            _viewModel.Formatter = x => new DateTime((long)x).ToString("yyyy");
        }

        public T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }
            return array;
        }

        private void ToggleLine0Series(object sender, System.EventArgs e)
        {
            if (!Line0Series.Title.Equals(this.sNullTitle))
            {

                Line0Series.Visibility = this.Line0Series.Visibility == Visibility.Visible
                    ? Visibility.Hidden
                    : Visibility.Visible;

                if (this.Line0Series.Visibility == Visibility.Hidden)
                {
                    chckParam0.Font = new Font(chckParam0.Font, System.Drawing.FontStyle.Strikeout);
                    this.datavis[0] = false;
                }
                else {
                    chckParam0.Font = new Font(chckParam0.Font, System.Drawing.FontStyle.Regular);
                    this.datavis[0] = true;
                }
            }
        }

        private void ToggleLine1Series(object sender, System.EventArgs e)
        {
            if (!Line1Series.Title.Equals(this.sNullTitle))
            {
                Line1Series.Visibility = this.Line1Series.Visibility == Visibility.Visible
                    ? Visibility.Hidden
                    : Visibility.Visible;

                if (this.Line1Series.Visibility == Visibility.Hidden)
                {
                    chckParam1.Font = new Font(chckParam1.Font, System.Drawing.FontStyle.Strikeout);
                    this.datavis[1] = false;
                }
                else
                {
                    chckParam1.Font = new Font(chckParam1.Font, System.Drawing.FontStyle.Regular);
                    this.datavis[1] = true;
                }
            }
        }

        private void ToggleLine2Series(object sender, System.EventArgs e)
        {
            if (!Line2Series.Title.Equals(this.sNullTitle))
            {
                Line2Series.Visibility = this.Line2Series.Visibility == Visibility.Visible
                    ? Visibility.Hidden
                    : Visibility.Visible;

                if (this.Line2Series.Visibility == Visibility.Hidden)
                {
                    chckParam2.Font = new Font(chckParam2.Font, System.Drawing.FontStyle.Strikeout);
                    this.datavis[2] = false;
                }
                else
                {
                    chckParam2.Font = new Font(chckParam2.Font, System.Drawing.FontStyle.Regular);
                    this.datavis[2] = true;
                }
            }
        }
        private void ToggleLine3Series(object sender, System.EventArgs e)
        {
            if (!Line3Series.Title.Equals(this.sNullTitle))
            {
                Line3Series.Visibility = Line3Series.Visibility == Visibility.Visible
                    ? Visibility.Hidden
                    : Visibility.Visible;

                if (this.Line3Series.Visibility == Visibility.Hidden)
                {
                    chckParam3.Font = new Font(chckParam3.Font, System.Drawing.FontStyle.Strikeout);
                    this.datavis[3] = false;
                }
                else
                {
                    chckParam3.Font = new Font(chckParam3.Font, System.Drawing.FontStyle.Regular);
                    this.datavis[3] = true;
                }
            }
        }

        private void ToggleLine4Series(object sender, System.EventArgs e)
        {
            if (!Line4Series.Title.Equals(this.sNullTitle))
            {
                Line4Series.Visibility = Line4Series.Visibility == Visibility.Visible
                    ? Visibility.Hidden
                    : Visibility.Visible;

                if (this.Line4Series.Visibility == Visibility.Hidden)
                {
                    chckParam4.Font = new Font(chckParam4.Font, System.Drawing.FontStyle.Strikeout);
                    this.datavis[4] = false;
                }
                else
                {
                    chckParam4.Font = new Font(chckParam4.Font, System.Drawing.FontStyle.Regular);
                    this.datavis[4] = true;
                }
            }
        }

        private void ToggleLine5Series(object sender, System.EventArgs e)
        {
            if (!Line5Series.Title.Equals(this.sNullTitle))
            {
                Line5Series.Visibility = Line5Series.Visibility == Visibility.Visible
                    ? Visibility.Hidden
                    : Visibility.Visible;

                if (this.Line5Series.Visibility == Visibility.Hidden)
                {
                    chckParam5.Font = new Font(chckParam5.Font, System.Drawing.FontStyle.Strikeout);
                    this.datavis[5] = false;
                }
                else
                {
                    chckParam5.Font = new Font(chckParam5.Font, System.Drawing.FontStyle.Regular);
                    this.datavis[5] = true;
                }
            }
        }

        private void ToggleLine6Series(object sender, System.EventArgs e)
        {
            if (!Line6Series.Title.Equals(this.sNullTitle))
            {
                Line6Series.Visibility = Line6Series.Visibility == Visibility.Visible
                    ? Visibility.Hidden
                    : Visibility.Visible;

                if (this.Line6Series.Visibility == Visibility.Hidden)
                {
                    chckParam6.Font = new Font(chckParam6.Font, System.Drawing.FontStyle.Strikeout);
                    this.datavis[6] = false;
                }
                else
                {
                    chckParam6.Font = new Font(chckParam6.Font, System.Drawing.FontStyle.Regular);
                    this.datavis[6] = true;
                }
            }
        }

        private void ToggleLine7Series(object sender, System.EventArgs e)
        {
            if (!Line7Series.Title.Equals(this.sNullTitle))
            {
                Line7Series.Visibility = Line7Series.Visibility == Visibility.Visible
                    ? Visibility.Hidden
                    : Visibility.Visible;

                if (this.Line7Series.Visibility == Visibility.Hidden)
                {
                    chckParam7.Font = new Font(chckParam7.Font, System.Drawing.FontStyle.Strikeout);
                    this.datavis[7] = false;
                }
                else
                {
                    chckParam7.Font = new Font(chckParam7.Font, System.Drawing.FontStyle.Regular);
                    this.datavis[7] = true;
                }
            }
        }

        private void CopyBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.Clear();
            StringBuilder clipData = new StringBuilder();
            double minTicks = this.scrollChart.AxisX[0].MinValue;
            double maxTicks = this.scrollChart.AxisX[0].MaxValue;
            double minInd = 0.0;
            double maxInd = this.xData.Count;

            int from = this.GetDataIndex(this.cartChart.AxisX[0].MinValue,maxInd,minInd,maxTicks,minTicks)-1;
            int to = this.GetDataIndex(this.cartChart.AxisX[0].MaxValue, maxInd, minInd, maxTicks, minTicks);

            for (int i = from; i < to; i++)
            {
                List<string> val = data[i];
                DateTime dt = DateTime.ParseExact(val[1], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                String dataRow = dt.ToString() + "\t";
                for (int j = 2; j < val.Count; j++)
                {
                    if (this.datavis[j - 2]) {
                        dataRow += val[j].ToString().Replace(",", ".") + "\t";
                    }
                }
                clipData.AppendLine(dataRow.ToString());
            }
            System.Windows.Forms.Clipboard.SetText(clipData.ToString());
            System.Windows.Forms.MessageBox.Show("Data has been copied to Clipboard", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public int GetDataIndex(double x, double y1, double y0, double x1, double x0) {
            double y = 0.0;
            y = y0 + (x - x0) * (y1 - y0) / (x1 - x0);
            if (y < y0){
                y = y0 + 1;
            }
            else if (y > y1) {
                y = y1;
            }
            return Convert.ToInt32(Math.Round(y,0));
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            sf.FilterIndex = 2;
            sf.RestoreDirectory = true;

            if (sf.ShowDialog() == DialogResult.OK)
            {

                StringBuilder clipData = new StringBuilder();
                System.IO.StreamWriter file = new System.IO.StreamWriter(sf.FileName);
                double minTicks = this.scrollChart.AxisX[0].MinValue;
                double maxTicks = this.scrollChart.AxisX[0].MaxValue;
                double minInd = 0.0;
                double maxInd = this.xData.Count;

                int from = this.GetDataIndex(this.cartChart.AxisX[0].MinValue, maxInd, minInd, maxTicks, minTicks) - 1;
                int to = this.GetDataIndex(this.cartChart.AxisX[0].MaxValue, maxInd, minInd, maxTicks, minTicks);

                for (int i = from; i < to; i++)
                {
                    List<string> val = data[i];
                    DateTime dt = DateTime.ParseExact(val[1], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    String dataRow = dt.ToString() + "\t";
                    for (int j = 2; j < val.Count; j++)
                    {
                        if (this.datavis[j - 2])
                        {
                            dataRow += val[j].ToString().Replace(",", ".") + "\t";
                        }
                    }
                    clipData.AppendLine(dataRow.ToString());
                    file.WriteLine(dataRow.ToString());
                }
                file.Close();
                file.Dispose();
                System.Windows.Forms.MessageBox.Show("Data has been saved to File", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            this.printPage.DefaultPageSettings.Margins = new Margins(20, 20, 20, 30);
            this.printPage.DocumentName = "Trends";
            this.printDialog1.Document = this.printPage;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.printPage.Print();
                this.Close();
            }
        }

        private void printPage_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            Font headerFont = new Font("Times", 14, System.Drawing.FontStyle.Bold);
            Font smallFont = new Font("Arial", 8, System.Drawing.FontStyle.Regular);

            e.Graphics.DrawString(this.sTitle, headerFont, System.Drawing.Brushes.Black, 330.0F, 40.0F);
            e.Graphics.DrawLine(Pens.Black, 100.0F, 80.0F, 780.0F, 80.0F);

            Rectangle bounds = this.Bounds;
            Bitmap bitmapWind = new Bitmap(bounds.Width-30,bounds.Height-25);
            bitmapWind.SetResolution(125.0F, 125.0F);

            Graphics g = Graphics.FromImage(bitmapWind);
           // Graphics g = Graphics.FromHwndInternal(this.Handle);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CopyFromScreen(new System.Drawing.Point(bounds.Left+12, bounds.Top), System.Drawing.Point.Empty, bounds.Size, CopyPixelOperation.SourceCopy);

            e.Graphics.DrawImage(bitmapWind, 80.0F, 200.0F);

            e.Graphics.DrawLine(Pens.DarkGray, 100.0F, 1110.0F, 780.0F, 1110.0F);
            e.Graphics.DrawString("Надруковано " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), smallFont, System.Drawing.Brushes.DarkGray, 600.0F, 1125.0F);
        }

    }
}
