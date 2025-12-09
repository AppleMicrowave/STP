using Lab12;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("МЕТРОЛОГИЯ И ТЕСТИРОВАНИЕ ПРОГРАММНОГО ОБЕСПЕЧЕНИЯ");
        Console.WriteLine("Лабораторная работа №1: Вероятностное моделирование метрических характеристик программ");
        Console.WriteLine("=");

        Console.WriteLine("\n1-2. ВЕРОЯТНОСТНОЕ МОДЕЛИРОВАНИЕ ПРОЦЕССА НАПИСАНИЯ ПРОГРАММЫ");

        var simulator = new ProgramWritingSimulator();
        int[] dictionarySizes = { 16, 32, 64, 128 };

        foreach (int n in dictionarySizes)
        {
            var result = simulator.Simulate(n, trials: 10000);
            result.PrintStatistics();
        }

        Console.WriteLine("\n");
        Console.WriteLine("4. АНАЛИЗ МЕТРИК РАЗРАБОТАННОЙ ПРОГРАММЫ");

        var (n1, n2, dictionarySize, actualLength) = ProgramMetricsAnalyzer.AnalyzeThisProgram();
        ProgramMetricsAnalyzer.CalculatePredictedLengths(n1, n2, actualLength);

        Console.WriteLine("\n");
        Console.WriteLine("5. ОПРЕДЕЛЕНИЕ n*2");
        ProgramMetricsAnalyzer.DetermineN2Star(n1, n2, actualLength);
    }
}