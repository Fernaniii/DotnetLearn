
using System.Numerics;

namespace Generic_Demo;
    public class MathOperations<T> where T : INumber<T>
    {

        public T Add(T x, T y)
        {
            return x + y;
        }

        public T Subtract(T x, T y) { return x - y; }
        public T Multiply(T x, T y) { return x * y; }

        public T Divide(T x, T y) {return x / y; }

    }

