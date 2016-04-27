using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MotorDCModel
{
    abstract public class Motor : INotifyPropertyChanged
    {
        protected int p, a, w;
        protected double u, ia, rd, ra, rz, c, m, n, p1, p2, efficiency, f;
        public ObservableCollection<KeyValuePair<double, double>> LineM { get; set; }
        public ObservableCollection<KeyValuePair<double, double>> LineN { get; set; }
        public ObservableCollection<KeyValuePair<double, double>> LineI { get; set; }
        public ObservableCollection<KeyValuePair<double, double>> CurrentPoints { get; set; }

        protected void updateLines()
        {
            LineN.Clear();
            LineM.Clear();
            LineI.Clear();
            for (double i = 0.1; i <= 1.2; i+=0.1)
            {
                LineN.Add(new KeyValuePair<double, double>(CalculateM(i)* CalculateN(i), CalculateN(i)));
                LineM.Add(new KeyValuePair<double, double>(CalculateM(i) * CalculateN(i), CalculateM(i)));
                LineI.Add(new KeyValuePair<double, double>(CalculateM(i) * CalculateN(i), i));
            }
        }
        #region Properties
        /// <summary>
        /// Число пар полюсів двигуна
        /// </summary>
        public int P
        {
            get { return p; }
            set
            {
                p = value;
                C = (P * W) / (2 * Math.PI * A);
                OnPropertyChanged("P");
            }
        }
        /// <summary>
        /// Кількість пар паралельних витків в обмотці якоря
        /// </summary>
        public int A
        {
            get { return a; }
            set
            {
                a = value;
                C = (P * W) / (2 * Math.PI * A);
                OnPropertyChanged("A");
            }            
        }
        /// <summary>
        /// Число активних провідників обмотки
        /// </summary>
        public int W
        {
            get { return w; }
            set
            {
                w = value;
                C = (P * W) / (2 * Math.PI * A);
                OnPropertyChanged("W");
            }            
        }
        /// <summary>
        /// Підведена напруга
        /// </summary>
        public double U
        {
            get { return u; }
            set
            {
                u = value;
                P1 = Ia * U;
                CalculateN();
                updateLines();
                OnPropertyChanged("U");
            }
        }
        /// <summary>
        /// Сила струму якоря
        /// </summary>
        public double Ia
        {
            get { return ia; }
            set
            {
                ia = value;
                CalculateN();
                CalculateM();
                P1 = Ia * U;
                
                OnPropertyChanged("Ia");
            }
        }
        /// <summary>
        /// Магнітний потік двигуна
        /// </summary>
        public double F
        {
            get { return f; }
            set
            {
                f = value;
                CalculateN();
                CalculateM();
                updateLines();
                OnPropertyChanged("F");
            }
        }
        /// <summary>
        /// Додатковий опір
        /// </summary>
        public double Rd
        {
            get { return rd; }
            set
            {
                rd = value;
                CalculateN();
                updateLines();
                OnPropertyChanged("Rd");
            }
        }
        /// <summary>
        /// Опір обмотки якоря
        /// </summary>
        public double Ra
        {
            get { return ra; }
            set
            {
                ra = value;
                CalculateN();
                updateLines();
                OnPropertyChanged("Ra");
            }
        }
        /// <summary>
        /// Опір обмотки збудження
        /// </summary>
        public double Rz
        {
            get { return rz; }
            set
            {
                rz = value;
                CalculateN();
                updateLines();
                OnPropertyChanged("Rz");
            }
        }
        /// <summary>
        /// Конструктивний коефіцієнт
        /// </summary>
        public double C
        {
            get { return c; }
            protected set
            {
                c = value;
                CalculateN();
                CalculateM();
                updateLines();
                OnPropertyChanged("C");
            }
        }
        /// <summary>
        /// Електромагнітний момент
        /// </summary>
        public double M
        {
            get { return m; }
            protected set
            {
                m = value;                
                P2 = N * M;
                OnPropertyChanged("M");
            }
        }
        /// <summary>
        /// Частота обертання
        /// </summary>
        public double N
        {
            get { return n; }
            protected set
            {
                n = value;
                P2 = N * M;                
                OnPropertyChanged("N");
            }
        }
        /// <summary>
        /// Підведена до двигуна потужність
        /// </summary>
        public double P1
        {
            get { return p1; }
            protected set
            {
                p1 = value;
                Efficiency = P2 / P1;
                OnPropertyChanged("P1");
            }
        }
        /// <summary>
        /// Корисна потужність двигуна
        /// </summary>
        public double P2
        {
            get { return p2; }
            protected set
            {
                p2 = value;
                Efficiency = P2 / P1;
                OnPropertyChanged("P2");
            }
        }
        /// <summary>
        /// Коефіцієнт корисної дії
        /// </summary>
        public double Efficiency
        {
            get { return efficiency; }
            protected set
            {
                efficiency = value;
                OnPropertyChanged("Efficiency");
            }
        }
        #endregion
        #region Constructor
        protected Motor()
        {
            LineM = new ObservableCollection<KeyValuePair<double, double>>();
            LineN = new ObservableCollection<KeyValuePair<double, double>>();
            LineI = new ObservableCollection<KeyValuePair<double, double>>();
            CurrentPoints = new ObservableCollection<KeyValuePair<double, double>>();
            updateLines();
        }
        #endregion
        
        #region Implement INotyfyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler RemoveMe;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            try
            {
                CurrentPoints.Clear();
                CurrentPoints.Add(new KeyValuePair<double, double>(N * M, N));
                CurrentPoints.Add(new KeyValuePair<double, double>(N * M, M));
            }
            catch (Exception)
            {
            }
            
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        abstract protected double CalculateN(double Ia);
        abstract protected double CalculateM(double Ia);
        protected void CalculateN()
        {
            N = CalculateN(Ia);
        }
        protected void CalculateM()
        {
            M = CalculateM(Ia);
        }

        #endregion
    }
}
