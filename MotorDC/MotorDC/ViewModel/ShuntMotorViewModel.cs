using MotorDCModel;

namespace ModelDCViewModel
{
    public class ShuntMotorViewModel
    {
        public Motor MotorDC { get; set; }
        public ShuntMotorViewModel()
        {
            MotorDC = ShuntMotor.Instance;
            MotorDC.P = 2;
            MotorDC.A = 2;
            MotorDC.W = 126;
            MotorDC.U = 220;
            MotorDC.Rd = 10;
            MotorDC.Ra = 20;
            MotorDC.Ia = 0.5;
            MotorDC.F = 0.4;
        }

    }
}