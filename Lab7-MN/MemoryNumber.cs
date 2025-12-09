using System;

namespace Lab7_MN
{
    public enum MemoryState
    {
        On,
        Off
    }

    public class TMemory<T> where T : new()
    {
        private T FNumber;
        private MemoryState FState;

        public TMemory()
        {
            FNumber = new T();
            FState = MemoryState.Off;
        }

        public TMemory(T initialNumber)
        {
            FNumber = initialNumber;
            FState = MemoryState.On;
        }

        public void Store(T number)
        {
            FNumber = number;
            FState = MemoryState.On;
        }

        public T Get()
        {
            FState = MemoryState.On;
            return FNumber;
        }

        public void Add(T number)
        {
            dynamic current = FNumber;
            dynamic newValue = number;
            FNumber = current + newValue;
            FState = MemoryState.On;
        }

        public void Clear()
        {
            FNumber = new T();
            FState = MemoryState.Off;
        }

        public string GetState()
        {
            return FState.ToString();
        }

        public T ReadNumber()
        {
            return FNumber;
        }

        public MemoryState State
        {
            get { return FState; }
        }

        public T Number
        {
            get { return FNumber; }
        }

        public override string ToString()
        {
            return $"Memory: {FNumber}, State: {FState}";
        }
    }
    public class TFrac
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public TFrac()
        {
            Numerator = 0;
            Denominator = 1;
        }

        public TFrac(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Denominator cannot be zero");

            Numerator = numerator;
            Denominator = denominator;
            Simplify();
        }

        public TFrac(string fraction)
        {
            string[] parts = fraction.Split('/');
            if (parts.Length != 2)
                throw new ArgumentException("Invalid fraction format");

            Numerator = int.Parse(parts[0]);
            Denominator = int.Parse(parts[1]);

            if (Denominator == 0)
                throw new ArgumentException("Denominator cannot be zero");

            Simplify();
        }

        public static TFrac operator +(TFrac a, TFrac b)
        {
            int newNumerator = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
            int newDenominator = a.Denominator * b.Denominator;
            return new TFrac(newNumerator, newDenominator);
        }

        private void Simplify()
        {
            int gcd = GCD(Math.Abs(Numerator), Math.Abs(Denominator));
            Numerator /= gcd;
            Denominator /= gcd;

            if (Denominator < 0)
            {
                Numerator = -Numerator;
                Denominator = -Denominator;
            }
        }

        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
    }
}
