using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveCharts.Defaults;
using LiveCharts;
using LiveCharts.Wpf;

namespace FillFlex
{
    public class trendViewModel
    {

        public trendViewModel()
        {
            var now = DateTime.Now;
            Formatter = x => new DateTime((long)x).ToString("HH:mm:ss");
            this.From = DateTime.Now.AddSeconds(0).Ticks;//10
            this.To = DateTime.Now.AddSeconds(60).Ticks;
            Values = this.Values;
            seriesCollection = this.seriesCollection;

        }

        public object Mapper { get; set; }
        public ChartValues<DateTimePoint> Values { get; set; }
        public LineSeries lineSeries { get; set; }
        public SeriesCollection seriesCollection { get; set; }

        public double From { get; set; }
        public double To { get; set; }

        public Func<double, string> Formatter { get; set; }
        /*
        public void ToggleSeries(object sender, System.EventArgs e)
        {
            Values.Visibility = MariaSeries.Visibility == Visibility.Visible
                ? Visibility.Hidden
                : Visibility.Visible;
        }
        */
    }
}
