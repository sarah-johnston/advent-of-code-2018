using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class Day4
    {
        public static void Day4Solution()
        {
            var input = Common.ParseFile("Input4.txt");

            // first split into date + string pairs
            List<KeyValuePair<DateTime, string>> sorted = SortRecords(input);
            Dictionary<DateTime, int> guardShifts = GetGuardShifts(sorted); //find which guards worked on which days
            var distinctDates = sorted.Select(x => x.Key.Date).Distinct().ToList();
            Dictionary<DateTime, List<KeyValuePair<int, string>>> datedRecords = new Dictionary<DateTime, List<KeyValuePair<int, string>>>();

            foreach (var date in distinctDates)
            {
                List<KeyValuePair<int, string>> records = new List<KeyValuePair<int, string>>();
                foreach (var item in sorted)
                {
                    if (item.Key.Date.Equals(date.Date))
                    {
                        records.Add(new KeyValuePair<int, string>(item.Key.Minute, item.Value));
                    }
                }
                datedRecords.Add(date.Date, records);
            }

            List<Shift> shifts = new List<Shift>();

            foreach (var guardShift in guardShifts)
            {
                Shift shift = new Shift();
                shift.Date = guardShift.Key;
                shift.GuardId = guardShift.Value;
                int currentMinute = 0;
                bool currentlyAwake = true;
                List<KeyValuePair<int, string>> records = new List<KeyValuePair<int, string>>();

                //need to do some sort of check here to make sure not null.
                var datedRecord = datedRecords.FirstOrDefault(x => x.Key.Equals(shift.Date));
                if (datedRecord.Value != null)
                {
                    records = datedRecord.Value;
                }
                while (currentMinute < 60)
                {
                    bool recordFound = false;
                    foreach (var item in records)
                    {
                        if (item.Key == currentMinute)
                        {
                            recordFound = true;
                            if (item.Value.Contains("wakes up"))
                            {
                                shift.Minutes[currentMinute] = 0;
                                currentlyAwake = true;
                            }
                            else if (item.Value.Contains("falls asleep"))
                            {
                                shift.Minutes[currentMinute] = 1;
                                currentlyAwake = false;
                            }
                        }
                    }
                    if (!recordFound)
                    {
                        if (currentlyAwake)
                            shift.Minutes[currentMinute] = 0;
                        else
                            shift.Minutes[currentMinute] = 1;
                    }
                    currentMinute++;
                }
                shifts.Add(shift);
            }

            Dictionary<int, int> guardSleepMinutes = new Dictionary<int, int>();
            var guardsList = GuardsList(input);
            foreach (var guard in guardsList)
            {
                int minutes = 0;
                foreach (var shift in shifts)
                {
                    if (shift.GuardId == guard)
                    {
                        minutes += shift.Minutes.Count(x => x == 1);
                    }
                }
                guardSleepMinutes.Add(guard, minutes);
            }

            var sleepiestGuard = guardSleepMinutes.OrderByDescending(x => x.Value).First();

            Dictionary<int, int[]> sleepiestMinutesByGuard = new Dictionary<int, int[]>(); //id, minute/amount

            foreach (var guard in guardsList)
            {
                int[] sleepingMinutes = new int[60];
                Array.Clear(sleepingMinutes, 0, 60);

                //find sleepiest minute
                foreach (var shift in shifts)
                {
                    if (shift.GuardId == guard)
                    {
                        for (int i = 0; i < 60; i++)
                        {
                            if (shift.Minutes[i] == 1)
                            {
                                sleepingMinutes[i]++;
                            }
                        }
                    }
                }
                int sleepiestMinute = sleepingMinutes.Max();
                int sleepiestMinuteIndex = sleepingMinutes.ToList().IndexOf(sleepiestMinute);
                sleepiestMinutesByGuard.Add(guard, new[] { sleepiestMinuteIndex, sleepiestMinute });
            }

            var sleepyhead = sleepiestMinutesByGuard.OrderByDescending(x => x.Value[1]).First();


            //var test = shifts.Where(x => x.Date == new DateTime(1518, 8, 17));

            Console.WriteLine("");
            //Console.ReadLine();
        }

        static List<int> GuardsList(List<string> input)
        {
            List<int> list = new List<int>();
            foreach (var entry in input)
            {
                if (entry.Contains('#'))
                {
                    list.Add(Int32.Parse(entry.Split('#').Last().Split(' ').First()));
                }
            }
            return list.Distinct().ToList();
        }

        static List<KeyValuePair<DateTime, string>> SortRecords(List<string> inputs)
        {
            List<KeyValuePair<DateTime, string>> records = new List<KeyValuePair<DateTime, string>>();
            foreach (var input in inputs)
            {
                var splitInput = input.Split(']');
                DateTime date = DateTime.Parse(splitInput[0].Trim('['));
                string record = splitInput[1].TrimStart(' ');
                records.Add(new KeyValuePair<DateTime, string>(date, record));
            }

            var sorted = records.OrderBy(x => x.Key.TimeOfDay)
                .ThenBy(x => x.Key.Date)
                .ToList();

            return sorted;
        }

        static Dictionary<DateTime, int> GetGuardShifts(List<KeyValuePair<DateTime, string>> sorted)
        {
            Dictionary<DateTime, int> guardShifts = new Dictionary<DateTime, int>();

            for (int i = 0; i < sorted.Count; i++)
            {
                string record = sorted[i].Value;
                DateTime dateTime = sorted[i].Key;
                if (record.Contains('#'))
                {
                    DateTime date = dateTime.Date;
                    var id = Int32.Parse(record.Split('#').Last().Split(' ').First());
                    //extract guard ID and store (clear time). If time is after 1am, the guard started his shift early so +1 to date.
                    if (dateTime.TimeOfDay >= Convert.ToDateTime("1:00:00 AM").TimeOfDay)
                    {
                        date = dateTime.AddDays(1).Date;
                    }
                    guardShifts.Add(date, id);
                }
            }
            return guardShifts;
        }
    }

    public class Shift
    {
        public int GuardId;
        public DateTime Date;
        public int[] Minutes; // store 0 as awake and 1 as asleep

        public Shift()
        {
            Minutes = new int[60];
            Array.Clear(Minutes, 0, 60);
        }
    }
}
