using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace GameObjects
{
    public class SpeedEventArg:EventArgs
    {
        public SpeedEventArg(double speed)
        {
            this.Speed = speed;
        }
        public double Speed { get; set; }
    }
}
