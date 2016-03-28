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
    public class DriveViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<DriveInfo> m_DriveInformation = new List<DriveInfo>();

        public List<DriveInfo> DriveInformation
        {
            get { return m_DriveInformation; }
            set
            {
                m_DriveInformation = value;
                RaisePropertyChangedEvent(nameof(DriveInformation));
            }
        }

        private Timer m_UpdateTimer;

        public DriveViewModel()
        {
            m_UpdateTimer = new Timer(new TimerCallback(OnUpdateTimerCallback), null, TimeSpan.FromMilliseconds(500),TimeSpan.FromMilliseconds(-1));
        }

        private void OnUpdateTimerCallback(object state)
        {
            List<DriveInfo> itemsToAdd = new List<DriveInfo>();
            foreach (DriveInfo drive in DriveInfo.GetDrives().Where(item => item.DriveType == DriveType.Fixed).ToList())
            {
                if (drive != null && drive.TotalSize != 0)
                {
                    itemsToAdd.Add(drive);
                }
            }
            DriveInformation = itemsToAdd;
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
