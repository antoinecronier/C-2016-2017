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
        int waiting = 500;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (haveToPrint)
            {
                haveToPrint = false;
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
            }
        }

        private void Canvas_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed
                && currentPoint != e.GetPosition(this)
                )
            {
                Rectangle line = new Rectangle();

                line.Stroke = SystemColors.WindowFrameBrush;
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

        public void aff4Square(int delay)
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (cancelTokenS.IsCancellationRequested == true)
                    {
                        break;
                    }
                    Task.Delay(TimeSpan.FromMilliseconds(delay)).Wait();
                    affSquare(Number.RandomNumber(0, 1024), Number.RandomNumber(0, 500));
                }
            }, cancelTokenS.Token);
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (cancelTokenS.IsCancellationRequested == true)
                    {
                        break;
                    }
                    Task.Delay(TimeSpan.FromMilliseconds(delay)).Wait();
                    affSquare(Number.RandomNumber(0, 1024), Number.RandomNumber(0, 500));
                }
            }, cancelTokenS.Token);
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (cancelTokenS.IsCancellationRequested == true)
                    {
                        break;
                    }
                    Task.Delay(TimeSpan.FromMilliseconds(delay)).Wait();
                    affSquare(Number.RandomNumber(0, 1024), Number.RandomNumber(0, 500));
                }
            }, cancelTokenS.Token);
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (cancelTokenS.IsCancellationRequested == true)
                    {
                        break;
                    }
                    Task.Delay(TimeSpan.FromMilliseconds(delay)).Wait();
                    affSquare(Number.RandomNumber(0, 1024), Number.RandomNumber(0, 500));
                }
            }, cancelTokenS.Token);
        }

        public void affSquare(int posX, int posY)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                Rectangle rect = new Rectangle();
                var brush = new SolidColorBrush(Color.FromRgb(
    Byte.Parse(Number.RandomNumber(0, 255).ToString()),
    Byte.Parse(Number.RandomNumber(0, 255).ToString()),
    Byte.Parse(Number.RandomNumber(0, 255).ToString())
    ));

                rect.Stroke = SystemColors.WindowFrameBrush;
                rect.Height = Number.RandomNumber(10, 250);
                rect.Width = Number.RandomNumber(10, 250);

                rect.Fill = brush;
                Canvas.SetLeft(rect, posX);
                Canvas.SetTop(rect, posY);
                paintSurface.Children.Add(rect);
            }));
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
                aff4Square(waiting);
            }
            else if (e.Key == Key.Z)
            {
                cancelTokenS.Cancel();
            }
            else if (e.Key == Key.P)
            {
                waiting = waiting + 50;
            }
            else if (e.Key == Key.M)
            {
                if (waiting >= 100)
                {
                    waiting = waiting - 50;
                }
            }
            else if (e.Key == Key.C)
            {
                paintSurface.Children.Clear();
            }

        }
    }
}
