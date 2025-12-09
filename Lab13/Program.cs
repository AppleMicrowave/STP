namespace Lab13
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Метрики Холстеда");
            Console.WriteLine("==========================================\n");

            var metrics = new List<AlgorithmMetrics>
            {
                new AlgorithmMetrics
                {
                    Name = "Поиск минимума в массиве",
                    η2_star = 2,
                    η1 = 12,
                    η2 = 8,
                    N1 = 28,
                    N2 = 20
                },
                new AlgorithmMetrics
                {
                    Name = "Сортировка пузырьком",
                    η2_star = 1,
                    η1 = 15,
                    η2 = 10,
                    N1 = 45,
                    N2 = 30
                },
                new AlgorithmMetrics
                {
                    Name = "Бинарный поиск",
                    η2_star = 2,
                    η1 = 10,
                    η2 = 7,
                    N1 = 25,
                    N2 = 18
                },
                new AlgorithmMetrics
                {
                    Name = "Поиск минимума в матрице",
                    η2_star = 2,
                    η1 = 14,
                    η2 = 9,
                    N1 = 35,
                    N2 = 25
                },
                new AlgorithmMetrics
                {
                    Name = "Реверс массива",
                    η2_star = 1,
                    η1 = 8,
                    η2 = 6,
                    N1 = 20,
                    N2 = 15
                },
                new AlgorithmMetrics
                {
                    Name = "Циклический сдвиг",
                    η2_star = 2,
                    η1 = 16,
                    η2 = 11,
                    N1 = 40,
                    N2 = 28
                },
                new AlgorithmMetrics
                {
                    Name = "Замена элементов",
                    η2_star = 3,
                    η1 = 9,
                    η2 = 7,
                    N1 = 22,
                    N2 = 16
                }
            };

            PrintHeader();
            foreach (var m in metrics)
            {
                m.CalculateMetrics();
                PrintRow(m);
            }

            CalculateAverages(metrics);

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void PrintHeader()
        {
            Console.WriteLine("Таблица метрических характеристик:");
            Console.WriteLine("==========================================================================================================================");
            Console.WriteLine("| Алгоритм                | η2* | η1 | η2 | η  | N1 | N2 | N  | N^  | V    | L    | L^   | I    | T1^  | T2^  | T3^  |");
            Console.WriteLine("==========================================================================================================================");
        }

        static void PrintRow(AlgorithmMetrics m)
        {
            Console.WriteLine($"| {m.Name,-23} | {m.η2_star,3} | {m.η1,3} | {m.η2,3} | {m.η,3} | {m.N1,3} | {m.N2,3} | {m.N,3} | {m.N_hat,4} | {m.V,5:F1} | {m.L,5:F3} | {m.L_hat,5:F3} | {m.I,5:F1} | {m.T1_hat,5:F1} | {m.T2_hat,5:F1} | {m.T3_hat,5:F1} |");
        }

        static void CalculateAverages(List<AlgorithmMetrics> metrics)
        {
            Console.WriteLine("==========================================================================================================================");

            double avgL = 0, avgV = 0, avgλ1 = 0, avgλ2 = 0;

            foreach (var m in metrics)
            {
                avgL += m.L;
                avgV += m.V;
                avgλ1 += m.λ1;
                avgλ2 += m.λ2;
            }

            int count = metrics.Count;
            Console.WriteLine($"\nСредние значения:");
            Console.WriteLine($"Средний уровень программы (L): {avgL / count:F3}");
            Console.WriteLine($"Средний объем (V): {avgV / count:F1}");
            Console.WriteLine($"Средний уровень языка λ1: {avgλ1 / count:F1}");
            Console.WriteLine($"Средний уровень языка λ2: {avgλ2 / count:F1}");
        }
    }

    public class AlgorithmMetrics
    {
        public string Name { get; set; }
        public int η2_star { get; set; }
        public int η1 { get; set; }
        public int η2 { get; set; }
        public int N1 { get; set; }
        public int N2 { get; set; }

        public int η => η1 + η2;
        public int N => N1 + N2;
        public int N_hat { get; private set; }
        public double V_star { get; private set; }
        public double V { get; private set; }
        public double L { get; private set; }
        public double L_hat { get; private set; }
        public double I { get; private set; }
        public double T1_hat { get; private set; }
        public double T2_hat { get; private set; }
        public double T3_hat { get; private set; }
        public double λ1 { get; private set; }
        public double λ2 { get; private set; }

        private const double S = 18;

        public void CalculateMetrics()
        {
            N_hat = (int)(η1 * Math.Log2(η1) + η2 * Math.Log2(η2));

            V_star = (2 + η2_star) * Math.Log2(2 + η2_star);

            V = N * Math.Log2(η);

            L = V_star / V;

            L_hat = (2.0 / η1) * (η2 / (double)N2);

            I = L_hat * N * Math.Log2(η);

            T1_hat = (V * V) / (S * V_star);

            T2_hat = (η1 * N2 * (η1 * Math.Log2(η1) + η2 * Math.Log2(η2)) * Math.Log2(η)) / (2 * S * η2);

            T3_hat = (η1 * N2 * N * Math.Log2(η)) / (2 * S * η2);

            λ1 = L * L * V;

            λ2 = (V * V) / V_star;
        }
    }
}