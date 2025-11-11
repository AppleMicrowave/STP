using System;

namespace Lab9
{
    public class TFrac : IEquatable<TFrac>
    {
        public int Numerator { get; }
        public int Denominator { get; }

        public TFrac(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Denominator cannot be zero.", nameof(denominator));

            Numerator = numerator;
            Denominator = denominator;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TFrac);
        }

        public bool Equals(TFrac other)
        {
            if (other is null) return false;
            return Numerator * other.Denominator == Denominator * other.Numerator;
        }

        public override int GetHashCode()
        {
            int gcd = Gcd(Numerator, Denominator);
            int simpleNum = Numerator / gcd;
            int simpleDen = Denominator / gcd;
            return HashCode.Combine(simpleNum, simpleDen);
        }

        private static int Gcd(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return Math.Abs(a);
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
    }
}