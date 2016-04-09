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
    /// Interaction logic for ProcessWindow.xaml
    /// </summary>
    public partial class ProcessWindow : Window
    {
        public ProcessWindow()
        {
            InitializeComponent();
        }

        private void KeyBinding_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void KeyBinding_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Settings.Default.ProcessWindowLeft = this.Left;
            Settings.Default.ProcessWindowTop = this.Top;
            Settings.Default.Save();
        }
    }
}
