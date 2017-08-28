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

namespace GameObjects
{
    /// <summary>
    /// Interaction logic for Explosion.xaml
    /// </summary>
    public partial class Explosion : UserControl
    {
        public Explosion()
        {
            InitializeComponent();
        }
        public int LifeTime { get; set; } = 20;
        public bool IsActive { get; set; } = false;
        public bool IsNeedToDelete
        {
            get
            {
                if (LifeTime == 0)
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
            LifeTime = 20;
        }
    }
}
