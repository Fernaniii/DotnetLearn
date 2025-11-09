namespace Generic_Demo
{
    public class EvaluateImportance : IImportance<int>, IImportance<string>
    {
        public int MostImportant(int a, int b)
        {
            if (a > b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }

        public string MostImportant(string a, string b)
        {
            if (a.Length > b.Length)
            {
                return a;
            }
            else
            {
                return b;
            }
        }
    }
}
