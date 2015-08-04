using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lte.WinApp.Models
{
    public interface IDataSeries
    {
        LinePattern LinePattern { get; set; }

        double LineThickness { get; set; }

        Brush LineColor { get; set; }
    }

    public static class DataSeriesOperations
    {
        public static void AddLinePattern(this IDataSeries series, Shape shape)
        {
            shape.Stroke = series.LineColor;
            shape.StrokeThickness = series.LineThickness;

            switch (series.LinePattern)
            {
                case LinePattern.Dash:
                    shape.StrokeDashArray = new DoubleCollection(new Double[] {4, 3});
                    break;
                case LinePattern.Dot:
                    shape.StrokeDashArray = new DoubleCollection(new Double[] {1, 2});
                    break;
                case LinePattern.DashDot:
                    shape.StrokeDashArray = new DoubleCollection(new Double[] {4, 2, 1, 2});
                    break;
                case LinePattern.None:
                    shape.Stroke = Brushes.Transparent;
                    break;
            }
        }
    }

    public interface IChartStyle
    {

        double Xmin { get; set; }

        double Xmax { get; set; }

        double Ymin { get; set; }

        double Ymax { get; set; }

        Canvas ChartCanvas { get; set; }
    }

    public static class ChartStyleOperations
    {
        public static Point NormalizePoint(this IChartStyle style, Point point)
        {
            if (style.ChartCanvas.Width.ToString() == "NaN")
                style.ChartCanvas.Width = 270;
            if (style.ChartCanvas.Height.ToString() == "NaN")
                style.ChartCanvas.Height = 250;
            return new Point
            {
                X = (int)((point.X - style.Xmin) * style.ChartCanvas.Width / (style.Xmax - style.Xmin)),
                Y = (int)(style.ChartCanvas.Height - (point.Y - style.Ymin) * style.ChartCanvas.Height / (style.Ymax - style.Ymin))
            };
        }

        public static Line GenerateLine(this Point pt, Brush borderColor, double borderThickness, double halfSize, int[] direction)
        {
            Line line = new Line
            {
                Stroke = borderColor,
                StrokeThickness = borderThickness,
                X1 = pt.X + direction[0]*halfSize,
                Y1 = pt.Y + direction[1]*halfSize,
                X2 = pt.X + direction[2]*halfSize,
                Y2 = pt.Y + direction[3]*halfSize
            };
            Panel.SetZIndex(line, 5);
            return line;
        }
    }

    public static class CanvasOperations
    {
        public static Rectangle GenerateLegendRect(this Canvas canvas, double legendWidth, double legendHeight)
        {
            canvas.Width = legendWidth + 55;
            Rectangle rect = new Rectangle
            {
                Stroke = Brushes.Black,
                Fill = Brushes.White,
                Width = legendWidth,
                Height = legendHeight
            };
            Panel.SetZIndex(canvas, 10);
            canvas.Width = rect.Width;
            canvas.Height = rect.Height;
            return rect;
        }

        public static void GenerateLegendLines(this Canvas canvas, IEnumerable<DataSeries> dataList, double textHeight, double lineLength)
        {
            const double sx = 6;
            const double sy = 0;
            int n = 1;
            double xText = 2 * sx + lineLength;
            foreach (DataSeries ds in dataList)
            {
                double yText = n * sy + (2 * n - 1) * textHeight / 2;
                Line line = new Line
                {
                    X1 = sx,
                    Y1 = yText,
                    X2 = sx + lineLength,
                    Y2 = yText
                };
                ds.AddLinePattern(line);
                canvas.Children.Add(line);
                ds.Symbols.AddSymbol(canvas, new Point(0.5 * (line.X2 - line.X1 + ds.Symbols.SymbolSize) + 1, line.Y1));
                TextBlock tb = new TextBlock { Text = ds.SeriesName };
                canvas.Children.Add(tb);
                Canvas.SetTop(tb, yText - textHeight / 2);
                Canvas.SetLeft(tb, xText);
                n++;
            }
        }
    }

    public enum LinePattern
    {
        Solid,
        Dash,
        Dot,
        DashDot,
        None
    }

    public enum LegendPosition
    {
        East,
        NorthEast,
        North,
        NorthWest,
        West,
        SouthWest,
        South,
        SouthEast
    }
}

