namespace Lab2
{
    public class StaticClass
    {
        public static void SortDescending(int[] numbers)
        {
            if (numbers == null || numbers.Length != 2)
            {
                throw new ArgumentException("Массив должен содержать ровно 2 числа");
            }

            if (numbers[0] < numbers[1])
            {
                int temp = numbers[0];
                numbers[0] = numbers[1];
                numbers[1] = temp;
            }
        }

        public static long ProductOfEvenValues(int[,] A)
        {
            long product = 1;
            bool hasEven = false;
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (A[i, j] % 2 == 0)
                    {
                        product *= A[i, j];
                        hasEven = true;
                    }
                }
            }

            return hasEven ? product : 0;
        }

        public static double SumEvenAboveSecondaryDiagonal(double[,] A)
        {
            double sum = 0.0;
            int n = A.GetLength(0);

            if (A.GetLength(0) != A.GetLength(1))
            {
                throw new ArgumentException("Массив должен быть квадратной матрицей");
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - i; j++)
                {
                    if (A[i, j] % 2 == 0)
                    {
                        sum += A[i, j];
                    }
                }
            }

            return sum;
        }

    }
}
