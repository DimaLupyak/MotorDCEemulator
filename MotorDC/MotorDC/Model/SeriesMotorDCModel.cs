using System;

namespace MotorDCModel
{
    public class SeriesMotor : Motor
    {
        #region Constructor
        protected SeriesMotor() : base() { }
        #endregion
        #region singleton
        protected static Motor instance = null;
        public static Motor Instance
        {
            get
            {
                if (instance == null)
                    instance = new SeriesMotor();
                return instance;

            }
        }
        #endregion

        protected override double CalculateM(double Ia)
        {
            return C * Math.Pow(Ia, 2);
        }

        protected override double CalculateN(double Ia)
        {
            return (U - (Ia * (Rd - Ra + Rz))) / (C * 2 * Ia);
        }
    }
}
