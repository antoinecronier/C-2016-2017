using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Media.Animation;
using System.IO;
using System.Windows.Navigation;
using wpfzoo.commandworker;

namespace wpfzoo.logger
{
    public class Logger
    {
        private const string INIT = "Initialization";
        private const string ALERT = "ALERT : ";
        private const string LOG_SAVING_QUESTION = "\nDo you want to report it?";
        private const string OVERLAY = "overlay_";
        private const string APPLICATION = "Application";

        private LogMode logMode;
        private string path;
        private string tag;
        private Boolean haveToBeSend;
        private AlertMode alertMode;
        private object data;
        private CancellationTokenSource cancellationTokenSource;
        private Point baseNotificationLocation;
        private CommandWorker commandWorker;
        private ManualResetEvent manualResetEvent;
        private CancellationTokenSource cancellationTS;
        private bool haveToRun;
        private object locker;
        private Boolean lifeCycle;

        public Boolean LifeCycle
        { get
            {
                return this.lifeCycle;
            }
            set
            {
                this.lifeCycle = value;
                if (this.lifeCycle)
                {
                    BindLifeCycle();
                }
                else
                {
                    UnBindLifeCycle();
                }
            }
        }

        #region LifeCycle

        private void UnBindLifeCycle()
        {
            #region Base Life Cycle
            System.Windows.Application.Current.Startup -= App_Startup;
            System.Windows.Application.Current.LoadCompleted -= App_LoadCompleted;
            System.Windows.Application.Current.Activated -= App_Activated;
            System.Windows.Application.Current.Deactivated -= App_Deactivated;
            System.Windows.Application.Current.SessionEnding -= App_SessionEnding;
            System.Windows.Application.Current.Exit -= App_Exit;
            #endregion

            #region Errors
            System.Windows.Application.Current.DispatcherUnhandledException -= App_DispatcherUnhandledException;
            #endregion

            #region Navigation
            System.Windows.Application.Current.FragmentNavigation -= App_FragmentNavigation;
            System.Windows.Application.Current.Navigated -= App_Navigated;
            System.Windows.Application.Current.Navigating -= App_Navigating;
            System.Windows.Application.Current.NavigationFailed -= App_NavigationFailed;
            System.Windows.Application.Current.NavigationProgress -= App_NavigationProgress;
            System.Windows.Application.Current.NavigationStopped -= App_NavigationStopped;
            #endregion
        }

        private void BindLifeCycle()
        {
            #region Base Life Cycle
            System.Windows.Application.Current.Startup += App_Startup;
            System.Windows.Application.Current.LoadCompleted += App_LoadCompleted;
            System.Windows.Application.Current.Activated += App_Activated;
            System.Windows.Application.Current.Deactivated += App_Deactivated;
            System.Windows.Application.Current.SessionEnding += App_SessionEnding;
            System.Windows.Application.Current.Exit += App_Exit;
            #endregion

            #region Errors
            System.Windows.Application.Current.DispatcherUnhandledException += App_DispatcherUnhandledException;
            #endregion

            #region Navigation
            System.Windows.Application.Current.FragmentNavigation += App_FragmentNavigation;
            System.Windows.Application.Current.Navigated += App_Navigated;
            System.Windows.Application.Current.Navigating += App_Navigating;
            System.Windows.Application.Current.NavigationFailed += App_NavigationFailed;
            System.Windows.Application.Current.NavigationProgress += App_NavigationProgress;
            System.Windows.Application.Current.NavigationStopped += App_NavigationStopped;
            #endregion
        }

        private void App_NavigationStopped(object sender, NavigationEventArgs e)
        {
            String temp = this.tag;
            this.tag = APPLICATION;
            this.CurrentFolder("App_NavigationStopped", "lifecyclelogs");
            this.Console("App_NavigationStopped");
            this.tag = temp;
        }

        private void App_NavigationProgress(object sender, NavigationProgressEventArgs e)
        {
            String temp = this.tag;
            this.tag = APPLICATION;
            this.CurrentFolder("App_NavigationProgress", "lifecyclelogs");
            this.Console("App_NavigationProgress");
            this.tag = temp;
        }

        private void App_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            String temp = this.tag;
            this.tag = APPLICATION;
            this.CurrentFolder("App_Navigating", "lifecyclelogs");
            this.Console("App_Navigating");
            this.tag = temp;
        }

        private void App_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            String temp = this.tag;
            this.tag = APPLICATION;
            this.CurrentFolder("App_NavigationFailed", "lifecyclelogs");
            this.Console("App_NavigationFailed");
            this.tag = temp;
        }

        private void App_Navigated(object sender, NavigationEventArgs e)
        {
            String temp = this.tag;
            this.tag = APPLICATION;
            this.CurrentFolder("App_Navigated", "lifecyclelogs");
            this.Console("App_Navigated");
            this.tag = temp;
        }

        private void App_FragmentNavigation(object sender, FragmentNavigationEventArgs e)
        {
            String temp = this.tag;
            this.tag = APPLICATION;
            this.CurrentFolder("App_FragmentNavigation", "lifecyclelogs");
            this.Console("App_FragmentNavigation");
            this.tag = temp;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            String temp = this.tag;
            this.tag = APPLICATION;
            this.CurrentFolder("App_DispatcherUnhandledException", "lifecyclelogs");
            this.Console("App_DispatcherUnhandledException");
            this.tag = temp;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            String temp = this.tag;
            this.tag = APPLICATION;
            this.CurrentFolder("App_Exit", "lifecyclelogs");
            this.Console("App_Exit");
            this.tag = temp;
        }

        private void App_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        {
            String temp = this.tag;
            this.tag = APPLICATION;
            this.CurrentFolder("App_SessionEnding", "lifecyclelogs");
            this.Console("App_SessionEnding");
            this.tag = temp;
        }

        private void App_Deactivated(object sender, EventArgs e)
        {
            String temp = this.tag;
            this.tag = APPLICATION;
            this.CurrentFolder("App_Deactivated", "lifecyclelogs");
            this.Console("App_Deactivated");
            this.tag = temp;
        }

        private void App_Activated(object sender, EventArgs e)
        {
            String temp = this.tag;
            this.tag = APPLICATION;
            this.CurrentFolder("App_Activated", "lifecyclelogs");
            this.Console("App_Activated");
            this.tag = temp;
        }

        private void App_LoadCompleted(object sender, NavigationEventArgs e)
        {
            String temp = this.tag;
            this.tag = APPLICATION;
            this.CurrentFolder("App_LoadCompleted", "lifecyclelogs");
            this.Console("App_LoadCompleted");
            this.tag = temp;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            String temp = this.tag;
            this.tag = APPLICATION;
            this.CurrentFolder("App_Startup", "lifecyclelogs");
            this.Console("App_Startup");
            this.tag = temp;
        }

        #endregion

        public Logger(string tag = "Logger", LogMode logMode = LogMode.NONE, AlertMode alertMode = AlertMode.NONE, object data = null, Boolean haveToBeSend = false, String path = "")
        {
            this.tag = tag;
            this.logMode = logMode;
            this.haveToBeSend = haveToBeSend;
            this.path = path;
            this.alertMode = alertMode;
            this.data = data;
            commandWorker = new CommandWorker("Logger");
            commandWorker.Start();
            manualResetEvent = new ManualResetEvent(false);
            cancellationTS = new CancellationTokenSource();
            locker = new object();
            Console(INIT);
        }

        public void Log(object msg, LogMode logMode = LogMode.NONE, AlertMode alertMode = AlertMode.NONE, object data = null, Boolean haveToBeSend = false, String path = "")
        {
            LogMode logModeBkp = this.logMode;
            Boolean haveToBeSendBkp = this.haveToBeSend;
            String pathBkp = this.path;
            AlertMode alertModeBkp = this.alertMode;
            object dataBkp = this.data;

            UpdateDatas(logMode, alertMode, data, haveToBeSend, path);
            Log(msg);
            UpdateDatas(logModeBkp, alertModeBkp, dataBkp, haveToBeSendBkp, pathBkp);
        }

        private void UpdateDatas(LogMode logMode, AlertMode alertMode, object data, bool haveToBeSend, string path)
        {
            this.logMode = logMode;
            this.haveToBeSend = haveToBeSend;
            this.path = path;
            this.alertMode = alertMode;
            this.data = data;
        }

        public void Log(object msg)
        {
            if (haveToBeSend)
            {
                //Store to send and delete
            }

            switch (logMode)
            {
                case LogMode.NONE:
                    break;
                case LogMode.CONSOLE:
                    Console(msg.ToString());
                    break;
                case LogMode.EXTERNAL:
                    break;
                case LogMode.CURRENT_FOLDER:
                    CurrentFolder(msg.ToString());
                    break;
                case LogMode.TEMP_FOLDER:
                    break;
                default:
                    break;
            }

#if DEBUG
            switch (alertMode)
            {
                case AlertMode.NONE:
                    break;
                case AlertMode.CONSOLE:
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    Console(ALERT + msg.ToString());
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case AlertMode.TOAST:
                    break;
                case AlertMode.MESSAGE_BOX:
                    MessageBox(ALERT + msg.ToString());
                    break;
                case AlertMode.OVERLAY:
                    ManualResetEvent reset = new ManualResetEvent(false);
                    commandWorker.Enqueue(() =>
                    {
                        this.manualResetEvent = reset;
                        this.haveToRun = true;
                        this.cancellationTS = new CancellationTokenSource();
                        Overlay(msg.ToString(), reset);
                    }, reset);
                    
                    break;
                default:
                    break;
            }
#else
            switch (alertMode)
            {
                default:
                    break;
            }
#endif
        }

        private void CurrentFolder(string msg, String filename = "\\current_logs", int number = 0)
        {
            TextWriter file = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + filename + number, true, UTF8Encoding.UTF8);
            FileInfo f = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + filename + number);
            String toSave = System.Windows.Application.Current + " => " + DateTime.Now + " : " + msg;
            int stringValue = UTF8Encoding.UTF8.GetByteCount(toSave);
            if (f.Length + stringValue <= 20000000)
            {
                file.WriteLine(toSave);
                file.Close();
            }
            else
            {
                file.Close();
                number += 1;
                CurrentFolder(msg, filename, number);
            }
        }

        private void Overlay(string msg, ManualResetEvent autoResetEvent)
        {
            //StackPanel stackpanel = CodeBehindNotification(msg);

            Task.Factory.StartNew(() =>
            {
                //Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    StackPanel stackPanelItem = FindChildControl<StackPanel>(System.Windows.Application.Current.MainWindow, "notification") as StackPanel;
                    //baseNotificationLocation = stackPanelItem.TransformToAncestor(System.Windows.Application.Current.MainWindow)
                    //      .Transform(new Point(0, 0));
                    if (stackPanelItem != null)
                    {
                        foreach (FrameworkElement item in stackPanelItem.Children)
                        {
                            switch (item.Name)
                            {
                                case "title":
                                    (item as TextBlock).Text = ALERT;
                                    break;
                                case "message":
                                    (item as TextBlock).Text = msg + LOG_SAVING_QUESTION;
                                    break;
                                case "buttons":
                                    foreach (FrameworkElement button in (item as StackPanel).Children)
                                    {
                                        switch (button.Name)
                                        {
                                            case "yes":
                                                (button as Button).Content = "oui";
                                                (button as Button).Click += Yes_Click;
                                                break;
                                            case "no":
                                                (button as Button).Content = "non";
                                                (button as Button).Click += No_Click;
                                                break;
                                            default:
                                                break;
                                        }

                                    }
                                    break;
                                default:
                                    break;
                            }
                        }

                        DockPanel dockPanelItem = FindChildControl<DockPanel>(System.Windows.Application.Current.MainWindow, "notificationDock") as DockPanel;

                        Task.Factory.StartNew(() =>
                        {
                            FrameworkElementMoving(dockPanelItem, 0, 0);
                        });

                        cancellationTokenSource = new CancellationTokenSource();
                        Task.Factory.StartNew(() =>
                        {
                            Task.Delay(TimeSpan.FromSeconds(8)).Wait(this.cancellationTokenSource.Token);
                            FindAndRemove(8, autoResetEvent);
                        }, this.cancellationTS.Token);
                    }
                }));
            });
        }

        private void FrameworkElementMoving(FrameworkElement element, double newX, double newY)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                if (element.Visibility != Visibility.Visible)
                {
                    element.Visibility = Visibility.Visible;
                }
                Point relativePoint = element.TransformToAncestor(System.Windows.Application.Current.MainWindow).Transform(
                    new Point(0, -30));
                //TODO find top right corner
                //new Point(-System.Windows.Application.Current.MainWindow.ActualHeight - element.ActualHeight * 2.1, element.ActualWidth));
                var top = relativePoint.Y;
                var left = relativePoint.X;

                TranslateTransform trans = new TranslateTransform();
                element.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(top, newY, TimeSpan.FromSeconds(3));
                DoubleAnimation anim2 = new DoubleAnimation(left, newX, TimeSpan.FromSeconds(3));
                trans.BeginAnimation(TranslateTransform.XProperty, anim1);
                trans.BeginAnimation(TranslateTransform.YProperty, anim2);
            }));
        }

        private Storyboard FrameworkElementOpacityRemoveSetup()
        {
            Storyboard storyboard = new Storyboard();;
            var closingAnimation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(3)))
            {
                BeginTime = TimeSpan.FromSeconds(0)
            };
            var resetAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0)))
            {
                BeginTime = TimeSpan.FromSeconds(4)
            };

            storyboard.Children.Add(closingAnimation);
            storyboard.Children.Add(resetAnimation);

            Storyboard.SetTargetProperty(closingAnimation, new PropertyPath(UIElement.OpacityProperty));
            Storyboard.SetTargetProperty(resetAnimation, new PropertyPath(UIElement.OpacityProperty));
            return storyboard;
        }
        
        private void No_Click(object sender, RoutedEventArgs e)
        {
            this.cancellationTS.Cancel();
            FindAndRemove(0, manualResetEvent);
        }

        private void FindAndRemove(Int32 delay, ManualResetEvent autoResetEvent)
        {
            Task.Factory.StartNew(() =>
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                lock (this)
                {
                    DockPanel dockPanelItem = FindChildControl<DockPanel>(System.Windows.Application.Current.MainWindow, "notificationDock") as DockPanel;
                    if (dockPanelItem != null)
                    {
                        if (this.haveToRun)
                        {
                            dockPanelItem.BeginStoryboard(FrameworkElementOpacityRemoveSetup());
                        }

                        Task.Factory.StartNew(() =>
                        {
                            Task.Delay(TimeSpan.FromSeconds(delay)).Wait();
                            System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
                            {
                                this.haveToRun = false;
                                dockPanelItem.Visibility = Visibility.Hidden;
                                autoResetEvent.Set();
                            }));
                        }, this.cancellationTokenSource.Token);
                    }
                }
            }));
            }, this.cancellationTokenSource.Token);
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            this.cancellationTS.Cancel();
            FindAndRemove(0, manualResetEvent);
            Log(this.data, LogMode.EXTERNAL);
        }

        private void MessageBox(string msg)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show(msg + LOG_SAVING_QUESTION, ALERT, MessageBoxButton.YesNo, MessageBoxImage.Error);
            if (result == MessageBoxResult.Yes)
            {
                Log(msg, LogMode.EXTERNAL);
            }
        }

        private void Console(string msg)
        {
            System.Console.WriteLine(tag + " : " + msg);
        }

        private DependencyObject FindChildControl<T>(DependencyObject control, string ctrlName)
        {
            int childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (int i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                FrameworkElement fe = child as FrameworkElement;

                /* Not a framework element or is null */
                if (fe == null) return null;

                if (child is T && fe.Name == ctrlName)
                {
                    /* Found the control so return */
                    return child;
                }
                else
                {
                    /* Not found it - search children */
                    DependencyObject nextLevel = FindChildControl<T>(child, ctrlName);
                    if (nextLevel != null)
                        return nextLevel;
                }
            }
            return null;
        }

        private void RemoveFromUI(FrameworkElement item)
        {
            Task.Factory.StartNew(() =>
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
                {
                    FrameworkElementMoving(item, this.baseNotificationLocation.X - 100, this.baseNotificationLocation.Y - 100);
                }));
            });
        }

        private StackPanel CodeBehindNotification(string msg)
        {
            StackPanel stackpanel = new StackPanel();
            stackpanel.Name = OVERLAY + tag;
            stackpanel.HorizontalAlignment = HorizontalAlignment.Right;
            stackpanel.VerticalAlignment = VerticalAlignment.Top;
            stackpanel.Width = 280;
            stackpanel.Height = 180;
            stackpanel.Background = new SolidColorBrush(Color.FromRgb(100, 100, 0));

            TextBlock overlayName = new TextBlock();
            overlayName.Text = ALERT;
            overlayName.HorizontalAlignment = HorizontalAlignment.Stretch;
            overlayName.VerticalAlignment = VerticalAlignment.Stretch;
            overlayName.Width = 60;
            overlayName.Height = 40;
            stackpanel.Children.Add(overlayName);

            TextBlock info = new TextBlock();
            info.Text = msg + LOG_SAVING_QUESTION;
            info.HorizontalAlignment = HorizontalAlignment.Stretch;
            info.VerticalAlignment = VerticalAlignment.Stretch;
            info.Width = 60;
            info.Height = 40;
            stackpanel.Children.Add(info);

            StackPanel buttons = new StackPanel();
            buttons.Orientation = Orientation.Horizontal;

            Button yes = new Button();
            yes.Content = "yes";
            yes.HorizontalAlignment = HorizontalAlignment.Stretch;
            yes.VerticalAlignment = VerticalAlignment.Stretch;
            yes.Width = 60;
            yes.Height = 40;
            yes.Click += Yes_Click;

            Button no = new Button();
            no.Content = "no";
            no.HorizontalAlignment = HorizontalAlignment.Stretch;
            no.VerticalAlignment = VerticalAlignment.Stretch;
            no.Width = 60;
            no.Height = 40;
            no.Click += No_Click;

            buttons.Children.Add(yes);
            buttons.Children.Add(no);

            buttons.HorizontalAlignment = HorizontalAlignment.Stretch;
            buttons.VerticalAlignment = VerticalAlignment.Stretch;
            buttons.Width = 160;
            buttons.Height = 140;
            stackpanel.Children.Add(buttons);
            return stackpanel;
        }

        private void RemoveFromUI(Grid item)
        {
            Task.Factory.StartNew(() =>
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
                {
                    StackPanel itemToRemove = FindChildControl<StackPanel>(System.Windows.Application.Current.MainWindow, OVERLAY + tag) as StackPanel;
                    if (item.Children.Contains(itemToRemove))
                    {
                        (itemToRemove.Parent as Grid).Children.Remove(itemToRemove);
                    }
                }));
            });
        }

        private void HandleOnCompleted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StoryboardCreation(FrameworkElement AssociatedObject)
        {
            var loadingAnimation = new DoubleAnimation(0.01, 1, new Duration(TimeSpan.FromSeconds(0.5)));
            var closingAnimation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(3)))
            {
                BeginTime = TimeSpan.FromSeconds(5)
            };

            Storyboard.SetTarget(loadingAnimation, AssociatedObject);
            Storyboard.SetTarget(closingAnimation, AssociatedObject);

            Storyboard.SetTargetProperty(loadingAnimation, new PropertyPath(UIElement.OpacityProperty));
            Storyboard.SetTargetProperty(closingAnimation, new PropertyPath(UIElement.OpacityProperty));

            Storyboard.SetTarget(loadingAnimation, AssociatedObject);
            Storyboard.SetTarget(closingAnimation, AssociatedObject);

            var storyboard = new Storyboard();
            storyboard.Children.Add(loadingAnimation);
            storyboard.Children.Add(closingAnimation);
            // Subscription to events must be done at this point, because the Storyboard object becomes frozen later on
            storyboard.Completed += HandleOnCompleted;

            string storyBoardName = "BeginNotificationStoryboard";

            // We define the BeginStoryBoard action for the EventTrigger
            var beginStoryboard = new BeginStoryboard();
            beginStoryboard.Name = storyBoardName;
            beginStoryboard.Storyboard = storyboard;

            // We create the EventTrigger
            var eventTrigger = new EventTrigger(Control.LoadedEvent);
            eventTrigger.Actions.Add(beginStoryboard);

            // Actions for the entering animation
            var enterSeekStoryboard = new SeekStoryboard
            {
                Offset = TimeSpan.FromSeconds(5),
                BeginStoryboardName = storyBoardName
            };
            var enterPauseStoryboard = new PauseStoryboard
            {
                BeginStoryboardName = storyBoardName
            };

            // Actions for the exiting animation
            var exitSeekStoryboard = new SeekStoryboard
            {
                Offset = TimeSpan.FromSeconds(5),
                BeginStoryboardName = storyBoardName
            };
            var exitResumeStoryboard = new ResumeStoryboard
            {
                BeginStoryboardName = storyBoardName
            };

            var trigger = new Trigger
            {
                Property = UIElement.IsMouseOverProperty,
                Value = true
            };

            trigger.EnterActions.Add(enterSeekStoryboard);
            trigger.EnterActions.Add(enterPauseStoryboard);
            trigger.ExitActions.Add(exitSeekStoryboard);
            trigger.ExitActions.Add(exitResumeStoryboard);

            var style = new Style();
            // The name of the Storyboard must be registered so the actions can find it
            style.RegisterName(storyBoardName, beginStoryboard);
            // Add both the EventTrigger and the regular Trigger
            style.Triggers.Add(eventTrigger);
            style.Triggers.Add(trigger);

            AssociatedObject.Style = style;
        }
    }
    //public static class UIRefresh
    //{
    //    private static Action EmptyDelegate = delegate () { };

    //    public static void Refresh(this UIElement uiElement)
    //    {
    //        uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
    //    }
    //}
}
