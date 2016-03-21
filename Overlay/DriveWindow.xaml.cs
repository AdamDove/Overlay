using Overlay.Properties;
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
using System.Windows.Shapes;

namespace Overlay
{
    /// <summary>
    /// Interaction logic for DriveWindow.xaml
    /// </summary>
    public partial class DriveWindow : Window
    {
        public DriveWindow()
        {
            InitializeComponent();
        }

        private void KeyBinding_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void KeyBinding_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Settings.Default.DriveWindowLeft = this.Left;
            Settings.Default.DriveWindowTop = this.Top;
            Settings.Default.Save();
        }
    }
}
