using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            periscopeGame.ClickBoat += PeriscopeGame_ClickBoat;
            periscopeGame.ClickTorpedo += PeriscopeGame_ClickTorpedo;
            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            periscopeGame.PeriscopeMove(e);
        }

        private void PeriscopeGame_ClickTorpedo(object sender, GameObjects.SpeedEventArg e)
        {
            tbSpeed.Text = $"Torpedo speed: x= {e.Speed:#.##}";
        }

        private void PeriscopeGame_ClickBoat(object sender, GameObjects.PositionEventArgs e)
        {
            tbPosttion.Text = $"Boat Position: x= {e.X}, y={e.Y}";
        }
        
    }
}
