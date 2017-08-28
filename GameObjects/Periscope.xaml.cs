using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GameObjects
{
    /// <summary>
    /// Interaction logic for Periscope.xaml
    /// </summary>
    public partial class Periscope : UserControl
    {
        bool fGame = false;
        DispatcherTimer dt = new DispatcherTimer();
        Boat b = new Boat();
        Torpedo tp = new Torpedo();
        Explosion expl = new Explosion();
        public event EventHandler<SpeedEventArg> ClickTorpedo;
        public event EventHandler<PositionEventArgs> ClickBoat;
        int movePeriscope = 300;
        public Periscope()
        {
            InitializeComponent();
            dt.Interval = new TimeSpan(TimeSpan.TicksPerMillisecond * 10);
            dt.Tick += Dt_Tick;

            gameCanvas.Children.Add(b);
            gameCanvas.Children.Add(tp);
            gameCanvas.Children.Add(expl);
            b.Start();
            TorpedoReady();
            ExplosionReady();
            this.DataContext = b;
            b.SetBinding(Canvas.LeftProperty, new Binding(nameof(b.X)));
            b.SetBinding(Canvas.TopProperty, new Binding(nameof(b.Y)));
            b.MouseDown += Boat_MouseDown;
            tp.MouseDown += Torpedo_MouseDown;

        }

        public void PeriscopeMove(KeyEventArgs e)
        {

            if (fGame)
            {
                if (e.Key == Key.Left)
                {
                    foreach (var item in gameCanvas.Children)
                    {
                        if (item != null && (!fTorpedo && (item is Torpedo))||((item is Explosion)&& expl.IsActive))
                        {
                            Canvas.SetLeft((UIElement)item, Canvas.GetLeft((UIElement)item) + movePeriscope);
                        }
                        else if (item != null && item is Boat)
                        {
                            ((Boat)item).X += movePeriscope;
                        }
                    }
                }
                else if (e.Key == Key.Right)
                {
                    foreach (var item in gameCanvas.Children)
                    {
                        if (item != null && ((!fTorpedo && (item is Torpedo)) || ((item is Explosion) && expl.IsActive)))
                        {
                            Canvas.SetLeft((UIElement)item, Canvas.GetLeft((UIElement)item) - movePeriscope);

                        }
                        else if(item !=null&& item is Boat )
                        {
                            ((Boat)item).X -= movePeriscope;
                        }
                    }
                } 
            }
        }




        private void Torpedo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.OnClickTorpedo(tp.Speed);
        }

        private void Boat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double x = e.GetPosition(this).X;
            double y = e.GetPosition(this).Y;
            this.OnClickBoat(x, y);
        }


        protected virtual void OnClickBoat(double x, double y)
        {
            if (ClickBoat != null)
            {
                ClickBoat.Invoke(this, new PositionEventArgs(x, y));
            }
        }

        protected virtual void OnClickTorpedo(double speed)
        {
            if (ClickBoat != null)
            {
                ClickTorpedo.Invoke(this, new SpeedEventArg(speed));
            }
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            b.Move();
            if (b.X > gameCanvas.Width + 250 || b.X < -250)
            {
                b.Rotate();
                b.Direction *= -1;
            }
            if (!fTorpedo)
            {
                tp.Move();
                MoveTorpedo();
                if (tp != null && tp.IsNeedToDelete)
                {
                    TorpedoReady();
                    fTorpedo = true;
                }
            }
            if (b.Check(tp.X,tp.Y))
            {
                Canvas.SetLeft(expl, tp.X-65);
                Canvas.SetTop(expl, tp.Y-100);
                b.Start();
                TorpedoReady();
                fTorpedo = true;
                expl.IsActive = true;
            }
            if (expl.IsActive)
            {
                expl.LifeTime--;
            }
            if (expl.IsNeedToDelete)
            {
                ExplosionReady();
            }
        }

        public void TorpedoReady()
        {
            tp.Start();
            Canvas.SetTop(tp, 670);
            Canvas.SetLeft(tp, 500);
            tp.X = 0;
            tp.Y = 0;
            
        }

        public void ExplosionReady()
        {
            expl.Start();
            Canvas.SetTop(expl, -150);
            Canvas.SetLeft(expl, -150);
            expl.IsActive = false;
        }

        private void MoveTorpedo()
        {
            Canvas.SetTop(tp, Canvas.GetTop(tp) - tp.Speed);
            tp.X = Canvas.GetLeft(tp)+tp.TorpedoWidth/2;
            tp.Y= Canvas.GetTop(tp);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (fGame)
            {
                dt.Stop();
                b.Start();
                TorpedoReady();
                ExplosionReady();
                fTorpedo = true;
                fGame = false;
                btnStart.Content = "Start!";
            }
            else
            {
                dt.Start();
                fGame = true;
                btnStart.Content = "Stop!";
            }
        }

        private void btnTorpedo_Click(object sender, RoutedEventArgs e)
        {
            if (fGame)
            {
                TorpedoReady();
                fTorpedo = false;
            }
        }



        public bool fTorpedo
        {
            get { return (bool)GetValue(fTorpedoProperty); }
            set { SetValue(fTorpedoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for fTorpedo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty fTorpedoProperty =
            DependencyProperty.Register("fTorpedo", typeof(bool), typeof(Periscope), new PropertyMetadata(true));


    }
}
