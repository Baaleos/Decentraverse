using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Decentraverse.Views
{
    public partial class StartPage : ContentPage
    {
        public static string PATH => "Start";

        public StartPage()
        {
            //InitializeComponent();
            //var assembly = typeof(CardCarouselContainer).GetTypeInfo().Assembly;
            //Stream stream = assembly.GetManifestResourceStream("Decentraverse.Effects.starfield.html");

            //using (var reader = new StreamReader(stream))
                //Starfield.Source = new HtmlWebViewSource
                //{
                //    Html = reader.ReadToEnd(),
                //};
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear(SKColors.Black);

            var circleFill = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColors.BlueViolet
            };

            canvas.DrawCircle(100, 100, 40, circleFill);

            var circleBorder = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Gray,
                StrokeWidth = 5
            };

            canvas.DrawCircle(100, 100, 40, circleBorder);
        }
    }
}
