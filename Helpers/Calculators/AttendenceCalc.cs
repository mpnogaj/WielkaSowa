using System;
using System.Collections.Generic;
using System.Linq;
using WielkaSowa.Models;
using WielkaSowa.Helpers.Extensions;
using AttPair = WielkaSowa.Helpers.Pair<double, int> ;

namespace WielkaSowa.Helpers.Calculators
{
    public static class AttendenceCalc
    {
        private static readonly int[] AttPoints = { 50, 40, 30, 0 };

        public static void AssingPoints(List<AttPair> list)
        {
            int currPlace = -1;
            double prev = -1.0;
            foreach(var c in list)
            {
                // Check if ex aequo
                if (prev != c.First) currPlace++;
                Storage.Instance!.Classes[c.Second].AttPoints = AttPoints[Math.Min(currPlace, 3)];
                prev = c.First;
            }
        }

        private static List<AttPair> CreateWoringList()
        {
            List<AttPair> output = new();
            for(int i = 0; i < Storage.Instance!.Classes.Count; i++)
            {
                Class c = Storage.Instance!.Classes[i];
                output.Add(new AttPair(c.AverageAtt.ToDouble(), i));
            }
            return output.OrderByDescending(x => x.First).ToList();
        }

        public static void UpdatePoints() => AssingPoints(CreateWoringList());
    }
}
