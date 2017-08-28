using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace GameObjects
{
    /// <summary>
    /// Interaction logic for Boat.xaml
    /// </summary>
    public partial class Boat : UserControl,INotifyPropertyChanged
    {
        
        private double x;
        private double y;
        ScaleTransform sc = new ScaleTransform(-1, 1, 0, 0);
        int w = 180;
        int h = 150;
        public Boat()
        {
            InitializeComponent();
            this.RenderTransform = sc;
        }



        public bool Check(double x,double y)
        {
            if (Direction==1)
            {
                if (x >= X-w && x <= X && y >= Y && y <= Y + h)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (x >= X  && x <= X+w && y >= Y && y <= Y + h)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public double X
        {
            get { return x; }
            set { x= value;
                OnPropertyChanged(nameof(X));
            }
        }
        public double Y
        {
            get { return y; }
            set { y=value;
                OnPropertyChanged(nameof(Y));
            }
        }

        public void Start()
        {
            this.X = 0;
            this.Y = 220;
            Direction = 1;
            sc.ScaleX = -1;
        }

        public void Move()
        {
            this.X+=Direction*Speed;
        }
        private int direction=1;

        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        private double speed=3;

        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }


        public void Rotate()
        {
            sc.ScaleX *= -1;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
