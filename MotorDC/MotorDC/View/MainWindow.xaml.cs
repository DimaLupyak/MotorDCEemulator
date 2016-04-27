using System.Windows;
using ModelDCViewModel;

namespace MotorDC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private object seriesMotorViewModel;
        private object shuntMotorViewModel;
        public MainWindow()
        {
            InitializeComponent();
            seriesMotorViewModel = new SeriesMotorViewModel();
            shuntMotorViewModel = new ShuntMotorViewModel();
            this.DataContext = seriesMotorViewModel;
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SerialTabItem.IsSelected)
            {
                this.DataContext = seriesMotorViewModel;
            }
            if (ShuntTabItem.IsSelected)
            {
                this.DataContext = shuntMotorViewModel;
            }
        }
    }
}
