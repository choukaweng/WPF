using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        //private List<ArrangeObject> controlGridList;
        //private double lastKnownYPosition = 0;


        public MainWindow()
        {
            InitializeComponent();
            InitializePoints();
        }

       
        private void InitializePoints()
        {
            Random rand = new Random();
            for(int i = 0; i < 100; i++)
            {
                MainViewModel.AddPoint(new DataPoint(rand.Next(1, 10), rand.Next(1, 10)));
            }
           
        }
    }


    public class MainViewModel
    {

        public string Title { get; private set; }

        public static IList<DataPoint> Points { get; private set; }
        public MainViewModel()
        {
            this.Title = "Example 2";
            MainViewModel.Points = new List<DataPoint>();
        }

        public static void AddPoint(DataPoint point)
        {
            Points.Add(point);
        }
    }
}
