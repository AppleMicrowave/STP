using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab12
{
    public class ProgramWritingSimulator
    {
        private readonly Random _random;

        public ProgramWritingSimulator()
        {
            _random = new Random();
        }

        public SimulationResult Simulate(int n, int trials = 10000)
        {
            var lengths = new List<int>();

            for (int trial = 0; trial < trials; trial++)
            {
                lengths.Add(SimulateSingleTrial(n));
            }

            return new SimulationResult
            {
                DictionarySize = n,
                TrialCount = trials,
                Lengths = lengths
            };
        }

        private int SimulateSingleTrial(int n)
        {
            var uniqueItems = new HashSet<int>();
            int totalSelections = 0;

            while (uniqueItems.Count < n)
            {
                int selectedItem = _random.Next(n);
                uniqueItems.Add(selectedItem);
                totalSelections++;
            }

            return totalSelections;
        }

        public static double TheoreticalExpectedLength(int n)
        {
            return 0.9 * n * Math.Log2(n);
        }

        public static double TheoreticalExpectedLengthAlternative(int n1, int n2)
        {
            return n1 * Math.Log2(n1) + n2 * Math.Log2(n2);
        }

        public static double TheoreticalVariance(int n)
        {
            return (Math.PI * Math.PI * n * n) / 6.0;
        }

        public static double TheoreticalStandardDeviation(int n)
        {
            return Math.Sqrt(TheoreticalVariance(n));
        }

        public static double TheoreticalRelativeError(int n)
        {
            return 1.0 / (2 * Math.Log2(n));
        }
    }

    public class SimulationResult
    {
        public int DictionarySize { get; set; }
        public int TrialCount { get; set; }
        public List<int> Lengths { get; set; }

        public double EmpiricalMean => Lengths.Average();
        public double EmpiricalVariance
        {
            get
            {
                double mean = EmpiricalMean;
                return Lengths.Sum(x => Math.Pow(x - mean, 2)) / TrialCount;
            }
        }
        public double EmpiricalStandardDeviation => Math.Sqrt(EmpiricalVariance);
        public double EmpiricalRelativeError => EmpiricalStandardDeviation / EmpiricalMean;

        public void PrintStatistics()
        {
            Console.WriteLine($"\n=== Результаты для η = {DictionarySize} ===");
            Console.WriteLine($"Количество испытаний: {TrialCount}");
            Console.WriteLine($"Эмпирическое мат. ожидание длины: {EmpiricalMean:F4}");
            Console.WriteLine($"Эмпирическая дисперсия: {EmpiricalVariance:F4}");
            Console.WriteLine($"Эмпирическое СКО: {EmpiricalStandardDeviation:F4}");
            Console.WriteLine($"Эмпирическая относительная погрешность: {EmpiricalRelativeError:P2}");

            double theoreticalMean = ProgramWritingSimulator.TheoreticalExpectedLength(DictionarySize);
            double theoreticalVar = ProgramWritingSimulator.TheoreticalVariance(DictionarySize);
            double theoreticalStdDev = ProgramWritingSimulator.TheoreticalStandardDeviation(DictionarySize);
            double theoreticalRelError = ProgramWritingSimulator.TheoreticalRelativeError(DictionarySize);

            Console.WriteLine($"\nТеоретическое мат. ожидание: {theoreticalMean:F4}");
            Console.WriteLine($"Теоретическая дисперсия: {theoreticalVar:F4}");
            Console.WriteLine($"Теоретическое СКО: {theoreticalStdDev:F4}");
            Console.WriteLine($"Теоретическая относительная погрешность: {theoreticalRelError:P2}");

            Console.WriteLine($"\nРазница мат. ожиданий: {Math.Abs(theoreticalMean - EmpiricalMean):F4} " +
                            $"(относительная: {Math.Abs(theoreticalMean - EmpiricalMean) / theoreticalMean:P2})");
            Console.WriteLine($"Разница дисперсий: {Math.Abs(theoreticalVar - EmpiricalVariance):F4} " +
                            $"(относительная: {Math.Abs(theoreticalVar - EmpiricalVariance) / theoreticalVar:P2})");
        }
    }

    public class ProgramMetricsAnalyzer
    {
        public static (int n1, int n2, int dictionarySize, int actualLength) AnalyzeThisProgram()
        {
            int n1 = 45;
            int n2 = 60;
            int dictionarySize = n1 + n2;
            int actualLength = 350;

            return (n1, n2, dictionarySize, actualLength);
        }

        public static void CalculatePredictedLengths(int n1, int n2, int actualLength)
        {
            int n = n1 + n2;

            double predictedLength1 = ProgramWritingSimulator.TheoreticalExpectedLength(n);
            double predictedLength2 = ProgramWritingSimulator.TheoreticalExpectedLengthAlternative(n1, n2);

            Console.WriteLine("\n=== Анализ метрик разработанной программы ===");
            Console.WriteLine($"Количество операторов (n1): {n1}");
            Console.WriteLine($"Количество операндов (n2): {n2}");
            Console.WriteLine($"Размер словаря (η = n1 + n2): {n}");
            Console.WriteLine($"Фактическая длина программы: {actualLength} токенов");
            Console.WriteLine($"\nПрогнозируемая длина по формуле 1 (0.9ηlog₂η): {predictedLength1:F2}");
            Console.WriteLine($"Прогнозируемая длина по формуле 2 (n1log₂n1 + n2log₂n2): {predictedLength2:F2}");

            Console.WriteLine($"\nОтклонение от формулы 1: {Math.Abs(actualLength - predictedLength1):F2} " +
                            $"(относительное: {Math.Abs(actualLength - predictedLength1) / actualLength:P2})");
            Console.WriteLine($"Отклонение от формулы 2: {Math.Abs(actualLength - predictedLength2):F2} " +
                            $"(относительное: {Math.Abs(actualLength - predictedLength2) / actualLength:P2})");
        }

        public static void DetermineN2Star(int n1, int n2, int actualLength)
        {
            Console.WriteLine("\n=== Определение n*2 ===");

            double minDifference = double.MaxValue;
            int bestN2 = 0;
            double bestPrediction = 0;

            for (int testN2 = 1; testN2 <= 2 * n2; testN2++)
            {
                double predicted = ProgramWritingSimulator.TheoreticalExpectedLengthAlternative(n1, testN2);
                double difference = Math.Abs(actualLength - predicted);

                if (difference < minDifference)
                {
                    minDifference = difference;
                    bestN2 = testN2;
                    bestPrediction = predicted;
                }
            }

            Console.WriteLine($"Оптимальное n*2: {bestN2}");
            Console.WriteLine($"Прогнозируемая длина при n*2: {bestPrediction:F2}");
            Console.WriteLine($"Фактическая длина: {actualLength}");
            Console.WriteLine($"Разница: {Math.Abs(actualLength - bestPrediction):F2}");
            Console.WriteLine($"Исходное n2: {n2}, n*2: {bestN2}, разница: {Math.Abs(bestN2 - n2)}");
        }
    }
}
