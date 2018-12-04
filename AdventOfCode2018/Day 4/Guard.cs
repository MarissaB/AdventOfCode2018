using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    public class Guard
    {
        public int ID { get; set; }
        public List<int> AsleepMinutes { get; set; }
        public int TotalSleepingMinutes { get; set; }
        public int CommonMinute { get; set; }
        public int CommonCount { get; set; }

        public Guard() { }

        public Guard(string input)
        {
            string[] splitInput = input.Split(null);
            ID = Convert.ToInt32(splitInput[3].Replace("#", ""));
            AsleepMinutes = new List<int>();
            TotalSleepingMinutes = 0;
        }

        public void AddSleep(int start, int end)
        {
            for (int minute = start; minute < end; minute++)
            {
                AsleepMinutes.Add(minute);
            }

            TotalSleepingMinutes = AsleepMinutes.Count();
            CommonMinute = MostCommonMinute();
            CommonCount = AsleepMinutes.Where(m => m == CommonMinute).Count();
        }

        public int MostCommonMinute()
        {
            int common = AsleepMinutes.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();

            return common;
        }
    }
}
