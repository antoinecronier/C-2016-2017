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
        Boolean haveToPrint = true;
        CancellationTokenSource cancelTokenS;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void squaring(int delay, CancellationToken token)
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Task.Delay(TimeSpan.FromMilliseconds(delay)).Wait();
                    printSquare(0, 0, Number.RandomNumber(0, 255).ToString(), Number.RandomNumber(0, 255).ToString(), Number.RandomNumber(0, 255).ToString());
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }, cancelTokenS.Token);
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Task.Delay(TimeSpan.FromMilliseconds(delay)).Wait();
                    printSquare(600 - 95, 0, Number.RandomNumber(0, 255).ToString(), Number.RandomNumber(0, 255).ToString(), Number.RandomNumber(0, 255).ToString());
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }, cancelTokenS.Token);
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Task.Delay(TimeSpan.FromMilliseconds(delay)).Wait();
                    printSquare(0, 500 - 115, Number.RandomNumber(0, 255).ToString(), Number.RandomNumber(0, 255).ToString(), Number.RandomNumber(0, 255).ToString());
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }, cancelTokenS.Token);
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Task.Delay(TimeSpan.FromMilliseconds(delay)).Wait();
                    printSquare(600 - 95, 500 - 115, Number.RandomNumber(0, 255).ToString(), Number.RandomNumber(0, 255).ToString(), Number.RandomNumber(0, 255).ToString());
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }, cancelTokenS.Token);
        }

        public void printSquare(int x, int y, String r, String g, String b)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    Rectangle line = new Rectangle();

                    line.Stroke = SystemColors.WindowFrameBrush;

                    line.Height = 80;
                    line.Width = 80;
                    var brush = new SolidColorBrush(Color.FromRgb(
                        Byte.Parse(r),
                        Byte.Parse(g),
                        Byte.Parse(b)
                        ));
                    line.Fill = brush;

                    Canvas.SetLeft(line, x);
                    Canvas.SetTop(line, y);

                    paintSurface.Children.Add(line);
                }));

        }


        private void Canvas_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (haveToPrint)
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
                
                haveToPrint = false;
            }
        }

        private void Canvas_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed
                && currentPoint != e.GetPosition(this))
            {
                Rectangle line = new Rectangle();

                line.Stroke = SystemColors.WindowFrameBrush;
                /*line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = e.GetPosition(this).X;
                line.Y2 = e.GetPosition(this).Y;*/
                line.Height = Number.RandomNumber(0, 80);
                line.Width = Number.RandomNumber(0, 80);
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
            }
        }

        private void paintSurface_MouseUp(object sender, MouseButtonEventArgs e)
        {
            haveToPrint = true;
        }

        private void paintSurface_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                cancelTokenS = new CancellationTokenSource();
                squaring(150,cancelTokenS.Token);
            }
            else if (e.Key == Key.Z)
            {
                cancelTokenS.Cancel();
                cancelTokenS.Dispose();
            }
        }
    }
}
