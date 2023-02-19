using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexSpeedApp.MVVM.ViewModel
{
    internal class ThrottleGaugeViewModel
    {
        public IEnumerable<ISeries> Series { get; set; }
            = new GaugeBuilder()
    .WithLabelsSize(0)
    .WithInnerRadius(20)
    .WithOffsetRadius(8)
    .WithBackgroundInnerRadius(20)
    .WithBackground(new SolidColorPaint(new SKColor(100, 181, 246, 90)))
    .AddValue(0.2, "Brake", SKColors.Red)
    .AddValue(1, "Throttle" , SKColors.YellowGreen)
    .BuildSeries();
    }
}
