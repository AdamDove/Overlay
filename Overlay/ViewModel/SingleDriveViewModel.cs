using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlay.ViewModel
{
    public class SingleDriveViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DriveInfo m_DriveInfo;
        private string m_DriveLetter;
        private double m_PercentFull;
        private string m_SpaceRemaining;

        public string DriveLetter
        {
            get { return m_DriveLetter; }
            private set
            {
                m_DriveLetter = value;
                RaisePropertyChangedEvent(nameof(DriveLetter));
            }
        }

        public double PercentFull
        {
            get { return m_PercentFull; }
            private set
            {
                m_PercentFull = value;
                RaisePropertyChangedEvent(nameof(PercentFull));
            }
        }

        public string SpaceRemaining
        {
            get { return m_SpaceRemaining; }
            private set
            {
                m_SpaceRemaining = value;
                RaisePropertyChangedEvent(nameof(SpaceRemaining));
            }
        }

        public SingleDriveViewModel(DriveInfo driveInfo)
        {
            m_DriveInfo = driveInfo;
            Update();
        }

        public static bool operator==(SingleDriveViewModel singleDrive, DriveInfo drive)
        {
            if (ReferenceEquals(singleDrive, null) && ReferenceEquals(drive, null))
            {
                return true;
            }
            else if (ReferenceEquals(singleDrive, null) || ReferenceEquals(drive, null))
            {
                return false;
            }
            else if (String.IsNullOrEmpty(drive.Name))
            {
                return String.IsNullOrEmpty(singleDrive.DriveLetter);
            }

            return (String.Compare(drive.Name.Substring(0, 1), singleDrive.DriveLetter) == 0);
        }

        public static bool operator!=(SingleDriveViewModel singleDrive, DriveInfo drive)
        {
            return !(singleDrive == drive);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void Update()
        {
            try
            {
                if (m_DriveInfo != null)
                {
                    if (!String.IsNullOrEmpty(m_DriveInfo.Name))
                    {
                        DriveLetter = m_DriveInfo.Name.Substring(0, 1);
                    }
                    if (m_DriveInfo.TotalSize > 0)
                    {
                        PercentFull = Convert.ToDouble(m_DriveInfo.TotalSize - m_DriveInfo.TotalFreeSpace) / Convert.ToDouble(m_DriveInfo.TotalSize);
                    }
                    SpaceRemaining = BytesToHumanString(m_DriveInfo.TotalFreeSpace);
                }
            }
            catch (Exception generalException)
            {

            }
        }

        private string BytesToHumanString(long bytes)
        {
            if (bytes < 1000)
            {
                return String.Format("{0} B", bytes);
            }
            bytes /= 1000;
            if (bytes < 1000)
            {
                return String.Format("{0} KB", bytes);
            }
            bytes /= 1000;
            if (bytes < 1000)
            {
                return String.Format("{0} MB", bytes);
            }
            bytes /= 1000;
            if (bytes < 1000)
            {
                return String.Format("{0} GB", bytes);
            }
            bytes /= 1000;
            return String.Format("{0} TB", bytes);
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
