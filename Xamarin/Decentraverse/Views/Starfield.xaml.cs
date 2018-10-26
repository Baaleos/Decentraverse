using System;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Decentraverse.Views
{
    public partial class Starfield : ContentPage
    {
        public struct Star
        {
            public float X;
            public float Y;
            public Vec2 Target;
            public float Size => Math.Abs(XNormalised);

            public float XNormalised => X - (float)(DeviceDisplay.ScreenMetrics.Width / 2);
            public float YNormalised => Y - (float)(DeviceDisplay.ScreenMetrics.Height / 2);
        }

        public enum Direction {
            TOPLEFT,
            TOPRIGHT,
            BOTTOMLEFT,
            BOTTOMRIGHT
        }

        public static string PATH => "Starfield";
        private Star[] stars = new Star[60];
        private bool pageIsActive;
        private Random random = new Random();

        private double CentreX;
        private double CentreY;

        public Starfield()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            pageIsActive = true;
            CentreX = DeviceDisplay.ScreenMetrics.Width / 2;
            CentreY = DeviceDisplay.ScreenMetrics.Height / 2;
            GenerateStarField();
            AnimationLoop();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            pageIsActive = false;
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            using (var circleFill = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Fill, Color = SKColors.BlueViolet })
            using (var circleBorder = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Stroke, Color = SKColors.Gray, StrokeWidth = 5 })
            foreach (var star in stars)
            {
                canvas.Clear(SKColors.Black);
                canvas.DrawCircle(star.X, star.Y, star.Size, circleFill);
                canvas.DrawCircle(star.X, star.Y, star.Size, circleBorder);
            }
        }

        async Task AnimationLoop()
        {
            while (pageIsActive)
            {
                canvasView.InvalidateSurface();

                await Task.Delay(TimeSpan.FromSeconds(1.0 / 60));
            }
        }

        private void GenerateStarField()
        {
            for (int i = 0; i < stars.Length; i++)
            {
                var xCoord = random.NextDouble() % DeviceDisplay.ScreenMetrics.Width;
                var yCoord = random.NextDouble() % DeviceDisplay.ScreenMetrics.Height;

                stars[i] = new Star
                {
                    X = (float)xCoord,
                    Y = (float)yCoord
                };

                Vec2 target = new Vec2();

                if (stars[i].XNormalised > 0)
                {
                    // Right of screen
                    target.X = random.NextDouble() % CentreX;
                }
                else
                {
                    target.X = -random.NextDouble() % CentreX;
                    // Left of screen
                }

                if (stars[i].YNormalised > 0)
                {
                    // Bottom of screen
                    target.Y = random.NextDouble() % CentreY;
                }
                else
                {
                    // Top of screen
                    target.Y = -random.NextDouble() % CentreY;
                }

                stars[i].Target = target;
            }
        }

        private void MoveStarField()
        {
            foreach(var star in stars)
            {

            }
        }
    }
}
