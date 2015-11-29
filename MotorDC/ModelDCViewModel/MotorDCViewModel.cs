using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorDCModel;
using System.Threading.Tasks;

namespace ModelDCViewModel
{
    public class MotorViewModel
    {
        public Motor MotorDC { get; set; }
        public MotorViewModel()
        {
            MotorDC = Motor.Instance;
            MotorDC.P = 2;
            MotorDC.A = 2;
            MotorDC.W = 126;
            MotorDC.U = 220;
            MotorDC.Rd = 10;
            MotorDC.Ra = 10;
            MotorDC.Rz = 10;
            MotorDC.Ia = 0.5;
        }

    }
}
