using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
///using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameObjects
{
    /// <summary>
    /// Interaction logic for Torpedo.xaml
    /// </summary>
    public partial class Torpedo : UserControl
    {
        ScaleTransform sc = new ScaleTransform(1, 1, 0.5, 0.5);
        int lifeTime = 100;
        int widt = 33;
        public Torpedo()
        {
            InitializeComponent();
            this.RenderTransform = sc;

        }
        public double TorpedoWidth
        { get { return widt * sc.ScaleX; } }

        public int LifeTime
        {
            get
            {
                return lifeTime;
            }
            set
            {
                if (value > 20)
                {
                    lifeTime = value;
                }
            }
        }
        public double Speed
        {
            get
            { return 4 * Math.Log(LifeTime / 20.0); }
        }
        public bool IsNeedToDelete
        {
            get
            {
                if (LifeTime == 21)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void Start()
        {
            sc.ScaleX = 1;
            sc.ScaleY = 1;
            LifeTime = 100;
        }
        public void Move()
        {
            //Canvas.SetBottom(this, Canvas.GetBottom(this) + this.Speed);
            LifeTime--;
            sc.ScaleX *= 0.98;
            sc.ScaleY *= 0.98;
        }

        //private double x;

        //public double X
        //{
        //    get { return x; }
        //    set { x = value; }
        //}

        //private double y;

        //public double Y
        //{
        //    get { return y; }
        //    set { y = value; }
        //}


        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        // Using a DependencyProperty as the backing store for X.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(Torpedo), new PropertyMetadata(0.0));



        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Y.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(Torpedo), new PropertyMetadata(0.0));


    }
}
