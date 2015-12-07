using System.Windows;
using ModelDCViewModel;

namespace MotorDC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MotorViewModel();
        }
    }
}
