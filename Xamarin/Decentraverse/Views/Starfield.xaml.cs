using System;
using System.Threading.Tasks;
using MathNet.Numerics;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Decentraverse.Views {
    public partial class Starfield : ContentPage {
        public class Star {
            public double X;
            public double Y;
            public double InitialX;
            public double InitialY;
            public double SizeMultiplier;
            public double Slope;
            public double Intercept;

            public Vec2 Coords => new Vec2(X, Y);
            public double YSloped => (X * Slope) + Intercept;
            public void SetSlopeIntercept(double intercept, double slope) {
                Slope = slope;
                Intercept = intercept;
            }

            public void SetSlopeIntercept(Tuple<double, double> slopeIntercept) {
                SetSlopeIntercept(slopeIntercept.Item1, slopeIntercept.Item2);
            }

            public double CalculatedSize(Vec2 origin) {
                return CurrentSize = Distance.Manhattan(new[] {Coords.X, Coords.Y}, new[] {origin.X, origin.Y}) /
                    (DeviceDisplay.ScreenMetrics.Density * 10) * SizeMultiplier;
            }
            public double CurrentSize { get; private set; }

            public Star() {
                X = InitialX;
                Y = InitialY;
            }
        }

        public static string PATH => "Starfield";
        private Star[] stars = new Star[8];
        private bool pageIsActive;
        private Random random = new Random();
        private Vec2 Origin;
        private double frameTime = 1.0 / 30;

        public Starfield() {
            InitializeComponent();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();
            pageIsActive = true;
            Origin = new Vec2(DeviceDisplay.ScreenMetrics.Width / 2, DeviceDisplay.ScreenMetrics.Height / 2);
            GenerateStarField();
//            new Task(() => {  }).Start();
            await AnimationLoop();
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            pageIsActive = false;
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e) {
            var surface = e.Surface;
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.Black);

            using(var circleFill = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Fill, Color = SKColors.White })
            using(var circleBorder = new SKPaint { IsAntialias = true, Style = SKPaintStyle.Stroke, Color = SKColors.Gray, StrokeWidth = 5 })
            foreach(var star in stars) {    
                if (star == null)
                    continue;
                canvas.DrawRect((float)star.X, (float)star.Y, (float)star.CalculatedSize(Origin), (float)star.CalculatedSize(Origin), circleFill);
                canvas.DrawRect((float)star.X, (float)star.Y, (float)star.CalculatedSize(Origin), (float)star.CalculatedSize(Origin), circleBorder);
            }
        }

        private async Task AnimationLoop() {
            while (pageIsActive) {
                MoveStarField(frameTime);
                canvasView.InvalidateSurface();
                //await Task.Delay(TimeSpan.FromSeconds(frameTime));
            }
        }

        private void GenerateStarField() {
            for(int i = 0; i < stars.Length; i++) {
                stars[i] = new Star();
                ConfigureStar(stars[i]);
            }
        }

        private void ConfigureStar(Star star) {
            var xCoord = random.Next((int)DeviceDisplay.ScreenMetrics.Width);
            var yCoord = random.Next((int)DeviceDisplay.ScreenMetrics.Height);

            star.InitialX = xCoord;
            star.InitialY = yCoord;
            star.SizeMultiplier = random.NextDouble();
            star.X = xCoord;
            star.Y = yCoord;

            var slopeIntercept = Fit.Line(new[] {Origin.X, xCoord}, new[] {Origin.Y, yCoord});
            star.SetSlopeIntercept(slopeIntercept);
        }

        private void MoveStarField(double frameTime) {
//            while(pageIsActive)
                foreach(var star in stars) {
                    if(star == null)
                        continue;
                    MoveStar(star, frameTime);
                }
        }

        private void MoveStar(Star star, double frameTime) {
            if (star.X < (0 - star.CurrentSize) ||
                star.Y < (0 - star.CurrentSize) ||
                star.X > (DeviceDisplay.ScreenMetrics.Width + star.CurrentSize) ||
                star.Y > (DeviceDisplay.ScreenMetrics.Height + star.CurrentSize)) {
                ConfigureStar(star);
            }

            if (star.InitialX <= Origin.X) {
                // Top left
                star.X -= Math.Abs(1 / star.Slope) * star.CurrentSize;// * 10 * frameTime;
                star.Y =  star.YSloped;
            } else if (star.InitialX > Origin.X) {
                // Top right
                star.X += Math.Abs(1 / star.Slope) * star.CurrentSize;// * 10 * frameTime;
                star.Y =  star.YSloped;
            }
        }

        private double interpolate(double p1, double p2, double fraction)
            => p1 + (p2 - p1) * fraction;
    }
}
