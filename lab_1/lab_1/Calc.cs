using System;

namespace lab_1
{
    public class Calc : IDisposable
    {
        public long Add(int a, int b) => a + b;

        public int Decrement(int a, int b) => a - b;

        public int Divide(int a, int b) => a / b;

        public long Multiple(int a, int b) => a * b;

        public double Sqrt(long a)
        {
            if (a <= 0)
            {
                throw new ArgumentException(a.ToString());
            }

            return Math.Sqrt(a);
        }

        public double Square(int a) => Math.Pow(a, 2);

        public void Dispose()
        {
            //dispose
        }
    }
}