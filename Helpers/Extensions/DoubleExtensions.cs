namespace WielkaSowa.Helpers.Extensions
{
    public static class DoubleExtensions
    {
        public static bool IsInRange(this double x, double l, double r)
        {
            return x <= r && x >= l;
        }
    }
}