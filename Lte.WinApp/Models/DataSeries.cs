using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lte.WinApp.Models
{
    public class DataSeries : IDataSeries
    {
        public Brush LineColor { get; set; }

        public Polyline LineSeries { get; set; }

        public double LineThickness { get; set; }

        public LinePattern LinePattern { get; set; }

        public string SeriesName { get; set; }

        public Symbols Symbols { get; set; }

        public DataSeries()
        {
            LineSeries = new Polyline();
            LineThickness = 1;
            SeriesName = "Default Name";
            LineColor = Brushes.Black;
            Symbols = new NoneSymbols();
        }
    }

    public class DataCollection
    {
        public List<DataSeries> DataList { get; set; }

        public DataCollection()
        {
            DataList = new List<DataSeries>();
        }

        public void AddLines(IChartStyle cs)
        {
            int j = 0;
            foreach (DataSeries ds in DataList)
            {
                if (ds.SeriesName == "Default Name")
                    ds.SeriesName = "DataSeries" + j;
                ds.AddLinePattern(ds.LineSeries);
                for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                {
                    ds.LineSeries.Points[i] =
                        cs.NormalizePoint(ds.LineSeries.Points[i]);
                }
                cs.ChartCanvas.Children.Add(ds.LineSeries);
                j++;
            }
        }

        private IEnumerable<string> GetLegendLabels()
        {
            int n = 0;
            string[] legendLabels = new string[DataList.Count];
            foreach (DataSeries ds in DataList)
            {
                legendLabels[n] = ds.SeriesName;
                n++;
            }
            return legendLabels;
        }

        public double CalculateLegendWidth()
        {
            IEnumerable<string> legendLabels = GetLegendLabels();

            double legendWidth = 0;
            foreach (TextBlock tb in legendLabels.Select(label => new TextBlock
            {
                Text = label
            }))
            {
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                if (legendWidth < tb.DesiredSize.Width)
                    legendWidth = tb.DesiredSize.Width;
            }
            return legendWidth;
        }

        public double CalculateLegendHeight(double textHeight)
        {
            return textHeight*DataList.Count;
        }
    }

}