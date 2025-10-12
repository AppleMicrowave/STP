namespace Lab4_Matrix
{
    public class MyException : Exception
    {
        public MyException(string s) : base(s)
        { }
    }
    public class Matrix
    {
        int[,] m;

        //Свойство для работы с числом строк.
        public int I { get; }
        //Свойство для работы с числом столбцов.
        public int J { get; }

        public Matrix(int i, int j)
        {
            if (i <= 0)
                throw new MyException($"недопустимое значение строки ={i}");
            if (j <= 0)
                throw new MyException($"недопустимое значение столбца = {j}");

            I = i;
            J = j;
            m = new int[i, j];
        }
        public int this[int i, int j]
        {
            get
            {
                if (i < 0 | i > I - 1)
                    throw new MyException($"неверное значение i = {i}");
                if (j < 0 | j > J - 1)
                    throw new MyException($"неверное значение j = {j}");
                return m[i, j];
            }
            set
            {
                if (i < 0 | i > I - 1)
                    throw new MyException($"неверное значение i = {i}");
                if (j < 0 | j > J - 1)
                    throw new MyException($"неверное значение j = {j}");
                m[i, j] = value;
            }
        }
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.I != b.I || a.J != b.J)
                throw new MyException("Размеры матриц не совпадают для сложения");

            Matrix c = new Matrix(a.I, a.J);
            for (int i = 0; i < a.I; i++)
                for (int j = 0; j < a.J; j++)
                {
                    c[i, j] = a.m[i, j] + b.m[i, j];
                }
            return c;
        }
        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.I != b.I || a.J != b.J)
                throw new MyException("Размеры матриц не совпадают для вычитания");

            Matrix c = new Matrix(a.I, a.J);
            for (int i = 0; i < a.I; i++)
                for (int j = 0; j < a.J; j++)
                {
                    c[i, j] = a.m[i, j] - b.m[i, j];
                }
            return c;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.J != b.I)
                throw new MyException("Матрицы не согласованы для умножения");

            Matrix c = new Matrix(a.I, b.J);
            for (int i = 0; i < a.I; i++)
                for (int j = 0; j < b.J; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < a.J; k++)
                    {
                        sum += a[i, k] * b[k, j];
                    }
                    c[i, j] = sum;
                }
            return c;
        }

        public static bool operator ==(Matrix a, Matrix b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            if (a.I != b.I || a.J != b.J) return false;

            bool q = true;
            for (int i = 0; i < a.I; i++)
                for (int j = 0; j < a.J; j++)
                {
                    if (a[i, j] != b[i, j])
                    {
                        q = false; break;
                    }
                }
            return q;
        }

        public static bool operator !=(Matrix a, Matrix b)
        {
            return !(a == b);
        }
        public Matrix Transp()
        {
            if (I != J)
                throw new MyException("Матрица должна быть квадратной для транспонирования");

            Matrix result = new Matrix(I, J);
            for (int i = 0; i < I; i++)
                for (int j = 0; j < J; j++)
                {
                    result[j, i] = this[i, j];
                }
            return result;
        }
        public int Min()
        {
            if (I == 0 || J == 0)
                throw new MyException("Матрица пуста");

            int min = this[0, 0];
            for (int i = 0; i < I; i++)
                for (int j = 0; j < J; j++)
                {
                    if (this[i, j] < min)
                        min = this[i, j];
                }
            return min;
        }
        public override string ToString()
        {
            string result = "{";
            for (int i = 0; i < I; i++)
            {
                result += "{";
                for (int j = 0; j < J; j++)
                {
                    result += this[i, j];
                    if (j < J - 1)
                        result += ",";
                }
                result += "}";
                if (i < I - 1)
                    result += ",";
            }
            result += "}";
            return result;
        }

        public void Show()
        {
            for (int i = 0; i < I; i++)
            {
                for (int j = 0; j < J; j++)
                {
                    Console.Write("\t" + this[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public override bool Equals(object obj)
        {
            if (obj is Matrix other)
                return this == other;
            return false;
        }

        public override int GetHashCode()
        {
            return m?.GetHashCode() ?? 0;
        }
    }
}
