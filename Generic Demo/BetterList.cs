
namespace Generic_Demo
{
    public class BetterList<T>
    {
        private List<T> data = new();

        public void AddToList(T value)
        {
            data.Add(value);
            Console.WriteLine($"{value} has been added on the List");
        }
        
    }
}
