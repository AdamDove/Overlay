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
    /// Interaction logic for ClockViewWindow.xaml
    /// </summary>
    public partial class ClockWindow : Window
    {
        public ClockWindow()
        {
            InitializeComponent();
        }

        private void KeyBinding_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void KeyBinding_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Settings.Default.ClockWindowLeft = this.Left;
            Settings.Default.ClockWindowTop = this.Top;
            Settings.Default.Save();
        }
    }
}
