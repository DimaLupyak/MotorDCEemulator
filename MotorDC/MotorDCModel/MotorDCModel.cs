using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MotorDCModel
{
    public class Motor : INotifyPropertyChanged
    {
        private int p, a, w;
        double u, ia, rd, ra, rz, c, m, n, p1, p2, efficiency;
        public ObservableCollection<KeyValuePair<double, double>> LineM { get; set; }
        public ObservableCollection<KeyValuePair<double, double>> LineN { get; set; }
        public ObservableCollection<KeyValuePair<double, double>> CurrentPoints { get; set; }
        private void updateLineN()
        {
            LineN.Clear();
            for (double i = 0.1; i <= 3.2; i+=0.1)
            {
                LineN.Add(new KeyValuePair<double, double>(i, (u - (i * (rd + ra + rz))) / (c * 2 * i)));
            }
        }
        private void updateLineM()
        {
            LineM.Clear();
            for (double i = 0; i <= 3.2; i += 0.1)
            {
                LineM.Add(new KeyValuePair<double, double>(i, c * Math.Pow(i, 2)));
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
                N = (U - (Ia * (Rd + Ra + Rz))) / (C * 2 * Ia);
                P1 = Ia * U;
                updateLineN();
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
                N = (U - (Ia * (Rd + Ra + Rz))) / (C * 2 * Ia);
                M = C * Math.Pow(Ia, 2);                
                P1 = Ia * U;
                CurrentPoints.Clear();
                CurrentPoints.Add(new KeyValuePair<double, double>(Ia, (u - (Ia * (rd + ra + rz))) / (c * 2 * Ia)));
                CurrentPoints.Add(new KeyValuePair<double, double>(Ia, c * Math.Pow(Ia, 2)));
                OnPropertyChanged("Ia");
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
                N = (U - (Ia * (Rd + Ra + Rz))) / (C * 2 * Ia);
                updateLineN();
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
                N = (U - (Ia * (Rd + Ra + Rz))) / (C * 2 * Ia);
                updateLineN();
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
                N = (U - (Ia * (Rd + Ra + Rz))) / (C * 2 * Ia);
                updateLineN();
                OnPropertyChanged("Rz");
            }
        }
        /// <summary>
        /// Конструктивний коефіцієнт
        /// </summary>
        public double C
        {
            get { return c; }
            private set
            {
                c = value;
                N = (U - (Ia * (Rd + Ra + Rz))) / (C * 2 * Ia);
                M = C * Math.Pow(Ia,2);
                updateLineN();
                updateLineM();
                OnPropertyChanged("C");
            }
        }
        /// <summary>
        /// Електромагнітний момент
        /// </summary>
        public double M
        {
            get { return m; }
            private set
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
            private set
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
            private set
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
            private set
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
            private set
            {
                efficiency = value;
                OnPropertyChanged("Efficiency");
            }
        }
        #endregion
        #region Constructor
        private Motor()
        {
            LineM = new ObservableCollection<KeyValuePair<double, double>>();
            LineN = new ObservableCollection<KeyValuePair<double, double>>();
            CurrentPoints = new ObservableCollection<KeyValuePair<double, double>>();
        }
        #endregion
        #region singleton
        private static Motor instance = null;
        public static Motor Instance
        {
            get
            {
                if (instance == null)
                    instance = new Motor();
                return instance;

            }
        }
        #endregion
        #region Implement INotyfyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler RemoveMe;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
