namespace WielkaSowa.Helpers.Extensions
{
    public static class IntExtensions
    {
        public static bool IsInRange(this int x, double l, double r)
        {
            return x <= r && x >= l;
        }
    }
}
