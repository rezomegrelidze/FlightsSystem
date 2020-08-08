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

namespace FlightsSystem.DBGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ZoomInOrOutHandle(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                var newFontSize = FontSize + (e.Delta) * 0.01;
                FontSize = newFontSize > 10 ? newFontSize : FontSize;
            }
        }

        private void AddToDB(object sender, RoutedEventArgs e)
        {
            
        }

        private void ReplaceDB(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
