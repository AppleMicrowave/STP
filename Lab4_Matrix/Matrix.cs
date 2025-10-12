using System;
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

        //Конструктор.
        public Matrix(int i, int j)
        {
            if (i <= 0)
                throw new MyException($"недопустимое значение строки ={ i }");
            if (j <= 0)
                throw new MyException($"недопустимое значение столбца = { j }");

            I = i;
            J = j;
            m = new int[i, j];
        }
        //Индексатор для доступа к значениям компонентов матрицы.
        public int this[int i, int j]
        {
            get
            {
                if (i < 0 | i > I - 1)
                    throw new MyException($"неверное значение i = { i }");
                if (j < 0 | j > J - 1)
                throw new MyException($"неверное значение j = { j }");
                return m[i, j];
            }
            set
            {
                if (i < 0 | i > I - 1)
                    throw new MyException($"неверное значение i = { i }");
                if (j < 0 | j > J - 1)
                    throw new MyException($"неверное значение j = { 0 }");
                m[i, j] = value;
            }
        }
        //Сложение матриц.
        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix c = new Matrix(a.I, a.J);
            for (int i = 0; i < a.I; i++)
                for (int j = 0; j < a.J; j++)
                {
                    c[i, j] = a.m[i, j] + b.m[i, j];
                }
            return c;
        }
        public static bool operator ==(Matrix a, Matrix b)
        {
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
        //Вывод значений компонентов на консоль.
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
            return this as Matrix == obj as Matrix;
        }
    }
}
