using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Overlay.Model
{
    static public class PerformanceModel //Make singleton
    {
        static private IEnumerable<ManagementObject> m_LastMeasure = null;
        static private ManagementObjectSearcher m_ProcessSearcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_PerfRawData_PerfProc_Process");

        /// <summary>
        ///   Returns an ordered list of CPU Usage per Processor.
        ///   Will return empty list on first call.
        /// </summary>
        /// <remarks>
        ///   Helpful link: https://msdn.microsoft.com/en-us/library/aa394323(v=vs.85).aspx
        /// </remarks>
        /// <returns></returns>
        static public List<Tuple<String, UInt64>> GetAllProcessCpuUsage()
        {

            IEnumerable<ManagementObject> currentPerformance = m_ProcessSearcher.Get().Cast<ManagementObject>();
            List<Tuple<String, UInt64>> results = new List<Tuple<string, ulong>>();

            if (m_LastMeasure != null)
            {
                foreach (ManagementObject process in currentPerformance)
                {
                    ManagementObject previous = m_LastMeasure.FirstOrDefault(mo => (UInt32)mo["IDProcess"] == (UInt32)process["IDProcess"]);
                    if (previous != null) //Only add items that exist in both lists
                    {
                        results.Add(new Tuple<string, ulong>((string)process["Name"],
                                                             (((UInt64)process["PercentProcessorTime"] - (UInt64)previous["PercentProcessorTime"]) * 100)
                                                              / (((UInt64)process["Timestamp_Sys100NS"] - (UInt64)previous["Timestamp_Sys100NS"]) * (UInt64)Environment.ProcessorCount)));
                    }
                }

                //Remove Total and Idle and sort
                results = results.Where(tuple => (String.Compare(tuple.Item1, "_Total") != 0) && (String.Compare(tuple.Item1, "Idle") != 0)).OrderByDescending(tuple => tuple.Item2).ToList();

                if (results != null && (results.Count() > 0))
                {
                    List<string> strings = new List<string>();
                    foreach (Tuple<String, UInt64> tuple in results)
                    {
                        strings.Add(String.Format("Name: {0} - {1}", tuple.Item1, tuple.Item2));
                    }
                    strings.Count();
                }
            }
            m_LastMeasure = currentPerformance;


            return results;
        }
    }
}
