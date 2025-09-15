using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Functions
    {
        public static double MultiplyEvenIndexedElements(double[] array)
        {
            if (array == null || array.Length == 0)
                return 0;

            double product = 1;
            for (int i = 0; i < array.Length; i += 2)
            {
                product *= array[i];
            }
            return product;
        }

        public static void CyclicShift(double[] array, int shift, string direction)
        {
            if (array == null || array.Length <= 1)
                return;

            int n = array.Length;
            shift = shift % n;
            if (shift < 0) shift += n;

            if (direction.ToLower() == "left")
            {
                ShiftLeft(array, shift);
            }
            else if (direction.ToLower() == "right")
            {
                ShiftRight(array, shift);
            }
        }

        private static void ShiftLeft(double[] array, int shift)
        {
            int n = array.Length;
            double[] temp = new double[shift];
            Array.Copy(array, temp, shift);
            Array.Copy(array, shift, array, 0, n - shift);
            Array.Copy(temp, 0, array, n - shift, shift);
        }

        private static void ShiftRight(double[] array, int shift)
        {
            int n = array.Length;
            double[] temp = new double[shift];
            Array.Copy(array, n - shift, temp, 0, shift);
            Array.Copy(array, 0, array, shift, n - shift);
            Array.Copy(temp, 0, array, 0, shift);
        }

        public static int FindMaxEvenElementWithEvenIndex(int[] array)
        {
            if (array == null || array.Length == 0)
                return int.MinValue;

            int max = int.MinValue;
            bool found = false;

            for (int i = 0; i < array.Length; i += 2)
            {
                if (array[i] % 2 == 0)
                {
                    if (!found || array[i] > max)
                    {
                        max = array[i];
                        found = true;
                    }
                }
            }

            return found ? max : int.MinValue;
        }
    }
}
