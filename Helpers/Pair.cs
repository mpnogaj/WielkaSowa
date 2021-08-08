namespace WielkaSowa.Helpers
{
    public class Pair<T, TY>
    {
        public readonly T First;
        public readonly TY Second;

        public Pair(T first, TY second)
        {
            First = first;
            Second = second;
        }

        public override string ToString()
        {
            return $"{First} : {Second}";
        }
    }
}