using Overlay.Model;
using Overlay.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Overlay.ViewModel
{
    public class ProcessViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public class SingleProcess
        {
            public string Name { get; set; }
            public double Percent { get; set; }
        }

        private List<SingleProcess> m_Processes = new List<SingleProcess>();

        public List<SingleProcess> Processes
        {
            get { return m_Processes; }
            set
            {
                m_Processes = value;
                RaisePropertyChangedEvent(nameof(Processes));
            }
        }

        private Timer m_UpdateTimer;

        public ProcessViewModel()
        {
            m_UpdateTimer = new Timer(new TimerCallback(OnUpdateTimerCallback), null, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
        }

        private void OnUpdateTimerCallback(object state)
        {
            List<Tuple<string, double>> input = PerformanceModel.GetAllProcessCpuUsage();
            if ((input != null) && (input.Count > 0))
            {
                int numberToDraw = (Settings.Default.ProcessNumberOfProcesses > input.Count ? input.Count : Settings.Default.ProcessNumberOfProcesses);
                Processes = input.GetRange(0, numberToDraw).Select(item => new SingleProcess() { Name = item.Item1, Percent = item.Item2 }).ToList();
            }
        }

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
