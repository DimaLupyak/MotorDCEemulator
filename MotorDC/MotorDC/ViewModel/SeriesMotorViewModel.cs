﻿using MotorDCModel;

namespace ModelDCViewModel
{
    public class SeriesMotorViewModel
    {
        public Motor MotorDC { get; set; }
        public SeriesMotorViewModel()
        {
            MotorDC = SeriesMotor.Instance;
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
