using System;
using System.Collections.Generic;
using System.Diagnostics;
using WielkaSowa.Helpers.Extensions;
using WielkaSowa.Models;
using Interval = WielkaSowa.Helpers.Pair<double, int>;

namespace WielkaSowa.Helpers.Calculators
{
    public static class MarkCalculator
    {
        private const int PointsBelowAverage = 20;
        private const int PointsAboveAverage = 40;

        public static double SchoolAverage { get; private set; } = 0.0;

        private static void UpdateAverage()
        {
            if (Storage.Instance == null || 
                Storage.Instance.Classes.Count == 0) return;
            SchoolAverage = 0.0;
            foreach(Class c in Storage.Instance.Classes)
            {
                SchoolAverage += c.AverageMark.ToDouble();
            }
            SchoolAverage /= Storage.Instance.Classes.Count;
            SchoolAverage = Math.Round(SchoolAverage, 2);
            Debug.WriteLine($"Updated mark average. New average: {SchoolAverage}");
        }

        private static List<Interval> CreateIntervals()
        {
            List<Interval> intervals = new List<Interval>();
            double start = Math.Round(((SchoolAverage * 100) % 10) / 100, 2);
            int currPoints = PointsBelowAverage;
            intervals.Add(new Interval(1, 0));
            for(double currEnd = start == 0 ? 1 + 0.1 : 1 + start; currEnd <= 6.00; currEnd = Math.Round(currEnd + 0.1, 2))
            {
                var interval = new Interval(Math.Min(currEnd, 6.0), currPoints);
                intervals.Add(interval);
                currPoints += currEnd < SchoolAverage ? PointsBelowAverage : PointsAboveAverage;
            }
#if DEBUG
            Debug.WriteLine($"Start: {start}");
            Debug.WriteLine("Intervals\n===");
            foreach(Interval i in intervals)
            {
                Debug.WriteLine(i.ToString());
            }
            Debug.WriteLine("===");
#endif
            return intervals;
        }

        private static int GetPoints(List<Interval> intervals, Class c)
        {
            if (intervals == null) return 0;
            int l = 0, r = intervals.Count - 1;
            double search = c.AverageMark.ToDouble();
            int sr = 0;
            while (l <= r)
            {
                sr = (l + r) / 2;
                if (intervals[sr].First < search) l = sr + 1;
                else if (intervals[sr].First > search) r = sr - 1;
                else return intervals[sr].Second;
            }
            return intervals[sr].Second;
        }

        private static void AssignPoints(List<Interval> intervals)
        {
            foreach(Class c in Storage.Instance!.Classes)
            {
                c.MarkPoints = GetPoints(intervals, c);
            }
        }

        public static void UpdatePoints()
        {
            if (Storage.Instance == null) return;
            UpdateAverage();
            AssignPoints(CreateIntervals());
        }
    }
}
