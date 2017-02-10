using Faker;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApplicationGraphique
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point currentPoint = new Point();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                currentPoint = e.GetPosition(this);

            Rectangle line = new Rectangle();

            line.Stroke = SystemColors.WindowFrameBrush;
            /*line.X1 = currentPoint.X;
            line.Y1 = currentPoint.Y;
            line.X2 = e.GetPosition(this).X;
            line.Y2 = e.GetPosition(this).Y;*/
            line.Height = 80;
            line.Width = 80;
            var brush = new SolidColorBrush(Color.FromRgb(
                Byte.Parse(Number.RandomNumber(0, 255).ToString()),
                Byte.Parse(Number.RandomNumber(0, 255).ToString()),
                Byte.Parse(Number.RandomNumber(0, 255).ToString())
                ));
            line.Fill = brush;

            Canvas.SetLeft(line, currentPoint.X);
            Canvas.SetTop(line, currentPoint.Y);

            currentPoint = e.GetPosition(this);

            paintSurface.Children.Add(line);

            Task.Factory.StartNew(() =>
            {
                Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    paintSurface.Children.Remove(line);
                }));
            });
        }

        private void Canvas_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Rectangle line = new Rectangle();

                line.Stroke = SystemColors.WindowFrameBrush;
                /*line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = e.GetPosition(this).X;
                line.Y2 = e.GetPosition(this).Y;*/
                line.Height = 80;
                line.Width = 80;
                var brush = new SolidColorBrush(Color.FromRgb(
                Byte.Parse(Number.RandomNumber(0, 255).ToString()),
                Byte.Parse(Number.RandomNumber(0, 255).ToString()),
                Byte.Parse(Number.RandomNumber(0, 255).ToString())
                ));
                line.Fill = brush;

                Canvas.SetLeft(line, currentPoint.X);
                Canvas.SetTop(line, currentPoint.Y);

                currentPoint = e.GetPosition(this);
                paintSurface.Children.Add(line);

                Task.Factory.StartNew(() =>
                {
                    Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {
                        paintSurface.Children.Remove(line);
                    }));
                });
            }
        }
    }
}
