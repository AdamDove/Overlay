using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlay.ViewModel
{
    public delegate void ApplicationExitEvent(object sender, EventArgs e);

    public class AppControlViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event ApplicationExitEvent OnApplicationExit;

    }
}
