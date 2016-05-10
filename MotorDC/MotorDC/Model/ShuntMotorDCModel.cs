namespace MotorDCModel
{
    public class ShuntMotor : Motor
    {
        #region Constructor
        protected ShuntMotor() : base() { }
        #endregion
        #region singleton
        protected static Motor instance = null;
        public static Motor Instance
        {
            get
            {
                if (instance == null)
                    instance = new ShuntMotor();
                return instance;

            }
        }
        #endregion

        protected override double CalculateM(double Ia)
        {
            return C * Ia * F;
        }

        protected override double CalculateN(double Ia)
        {
            return (U - (Ia * (Rd + Ra))) / (C * F);
        }
    }
}
