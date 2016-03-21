using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlay.Model
{
    public class DriveInformation
    {
        public List<DriveInfo> AllDrives;

        public DriveInformation()
        {
            AllDrives = DriveInfo.GetDrives().ToList();
        }
    }
}
