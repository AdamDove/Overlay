using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Overlay.ViewModel
{
    public class ClockViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Timer m_ClockUpdateTimer;
        private DateTime m_CurrentTime = DateTime.Now;

        public DateTime CurrentTime
        {
            get { return m_CurrentTime; }
            set
            {
                m_CurrentTime = value;
                RaisePropertyChangedEvent(nameof(CurrentTime));
            }
        }

        public ClockViewModel()
        {
            DateTime startTime = DateTime.Now;
            while (startTime.Second == DateTime.Now.Second) ;

            m_ClockUpdateTimer = new Timer(new TimerCallback(OnClockUpdateTimerCallback), null, TimeSpan.FromMilliseconds(250), TimeSpan.FromMilliseconds(250));
        }
            
        private void OnClockUpdateTimerCallback(object state)
        {
            CurrentTime = DateTime.Now;
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
