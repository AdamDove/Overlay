using Overlay.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Overlay.ViewModel
{
    public class AppControlViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand OnExitClick
        {
            get { return new CommandDelegate(ExitApplication, true); }
        }

        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
