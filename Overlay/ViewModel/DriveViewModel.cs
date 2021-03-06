﻿using Overlay.Model;
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

        private List<SingleDriveViewModel> m_AllDrives = new List<SingleDriveViewModel>();

        public List<SingleDriveViewModel> AllDrives
        {
            get { return m_AllDrives; }
            set
            {
                m_AllDrives = value;
                RaisePropertyChangedEvent(nameof(AllDrives));
            }
        }

        private Timer m_UpdateTimer;

        public DriveViewModel()
        {
            m_UpdateTimer = new Timer(new TimerCallback(OnUpdateTimerCallback), null, TimeSpan.FromMilliseconds(500),TimeSpan.FromSeconds(5));
        }

        private void OnUpdateTimerCallback(object state)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives().Where(item => item.DriveType == DriveType.Fixed))
            {
                if (drive != null && !String.IsNullOrEmpty(drive.Name) && drive.TotalSize != 0)
                {
                    SingleDriveViewModel foundDrive = AllDrives.Find(d => d == drive);
                    if (foundDrive == null)
                    {
                        AllDrives.Add(new SingleDriveViewModel(drive));
                        RaisePropertyChangedEvent(nameof(AllDrives));
                    }
                    else
                    {
                        foundDrive.Update();
                    }
                }
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
