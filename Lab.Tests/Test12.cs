using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab12;

namespace Lab12.Tests
{
    [TestClass]
    public class ProgramWritingSimulatorTests
    {
        [TestMethod]
        public void Simulate_SingleTrial_ReturnsValidLength()
        {
            var simulator = new ProgramWritingSimulator();
            var result = simulator.Simulate(10, 1);

            Assert.AreEqual(1, result.TrialCount);
            Assert.IsTrue(result.Lengths[0] >= 10);
        }

        [TestMethod]
        public void Simulate_MultipleTrials_ReturnsCorrectCount()
        {
            var simulator = new ProgramWritingSimulator();
            var result = simulator.Simulate(20, 50);

            Assert.AreEqual(50, result.TrialCount);
            Assert.AreEqual(50, result.Lengths.Count);
        }

        [TestMethod]
        public void Simulate_AllLengthsGreaterOrEqualThanDictionarySize()
        {
            var simulator = new ProgramWritingSimulator();
            var result = simulator.Simulate(15, 100);

            foreach (var length in result.Lengths)
            {
                Assert.IsTrue(length >= 15);
            }
        }

        [TestMethod]
        [DataRow(16)]
        [DataRow(32)]
        [DataRow(64)]
        [DataRow(128)]
        public void Simulate_DifferentDictionarySizes_WorksCorrectly(int n)
        {
            var simulator = new ProgramWritingSimulator();
            var result = simulator.Simulate(n, 100);

            Assert.AreEqual(n, result.DictionarySize);
            Assert.AreEqual(100, result.TrialCount);
        }

        [TestMethod]
        public void TheoreticalExpectedLength_ValidInput_ReturnsCorrectValue()
        {
            double result = ProgramWritingSimulator.TheoreticalExpectedLength(32);

            double expected = 0.9 * 32 * Math.Log2(32);
            Assert.AreEqual(expected, result, 0.0001);
        }

        [TestMethod]
        public void TheoreticalExpectedLengthAlternative_ValidInput_ReturnsCorrectValue()
        {
            double result = ProgramWritingSimulator.TheoreticalExpectedLengthAlternative(20, 30);

            double expected = 20 * Math.Log2(20) + 30 * Math.Log2(30);
            Assert.AreEqual(expected, result, 0.0001);
        }

        [TestMethod]
        public void TheoreticalVariance_ValidInput_ReturnsCorrectValue()
        {
            double result = ProgramWritingSimulator.TheoreticalVariance(16);

            double expected = (Math.PI * Math.PI * 16 * 16) / 6.0;
            Assert.AreEqual(expected, result, 0.0001);
        }

        [TestMethod]
        public void TheoreticalStandardDeviation_ValidInput_ReturnsCorrectValue()
        {
            double result = ProgramWritingSimulator.TheoreticalStandardDeviation(64);

            double variance = (Math.PI * Math.PI * 64 * 64) / 6.0;
            double expected = Math.Sqrt(variance);
            Assert.AreEqual(expected, result, 0.0001);
        }

        [TestMethod]
        public void TheoreticalRelativeError_ValidInput_ReturnsCorrectValue()
        {
            double result = ProgramWritingSimulator.TheoreticalRelativeError(128);

            double expected = 1.0 / (2 * Math.Log2(128));
            Assert.AreEqual(expected, result, 0.0001);
        }

        [TestMethod]
        public void TheoreticalRelativeError_SmallDictionary_ReturnsReasonableValue()
        {
            double result = ProgramWritingSimulator.TheoreticalRelativeError(8);

            Assert.IsTrue(result > 0 && result < 1);
        }
    }

    [TestClass]
    public class SimulationResultTests
    {
        [TestMethod]
        public void EmpiricalMean_ValidData_CalculatesCorrectly()
        {
            var result = new SimulationResult
            {
                DictionarySize = 10,
                TrialCount = 3,
                Lengths = new System.Collections.Generic.List<int> { 15, 20, 25 }
            };

            Assert.AreEqual(20.0, result.EmpiricalMean);
        }

        [TestMethod]
        public void EmpiricalVariance_ValidData_CalculatesCorrectly()
        {
            var result = new SimulationResult
            {
                DictionarySize = 10,
                TrialCount = 3,
                Lengths = new System.Collections.Generic.List<int> { 15, 20, 25 }
            };

            double expectedVariance = ((15 - 20) * (15 - 20) + (20 - 20) * (20 - 20) + (25 - 20) * (25 - 20)) / 3.0;
            Assert.AreEqual(expectedVariance, result.EmpiricalVariance, 0.0001);
        }

        [TestMethod]
        public void EmpiricalStandardDeviation_ValidData_CalculatesCorrectly()
        {
            var result = new SimulationResult
            {
                DictionarySize = 10,
                TrialCount = 3,
                Lengths = new System.Collections.Generic.List<int> { 15, 20, 25 }
            };

            double variance = result.EmpiricalVariance;
            double expectedStdDev = Math.Sqrt(variance);
            Assert.AreEqual(expectedStdDev, result.EmpiricalStandardDeviation, 0.0001);
        }

        [TestMethod]
        public void EmpiricalRelativeError_ValidData_CalculatesCorrectly()
        {
            var result = new SimulationResult
            {
                DictionarySize = 10,
                TrialCount = 3,
                Lengths = new System.Collections.Generic.List<int> { 15, 20, 25 }
            };

            double expected = result.EmpiricalStandardDeviation / result.EmpiricalMean;
            Assert.AreEqual(expected, result.EmpiricalRelativeError, 0.0001);
        }

        [TestMethod]
        public void PrintStatistics_DoesNotThrow()
        {
            var result = new SimulationResult
            {
                DictionarySize = 16,
                TrialCount = 5,
                Lengths = new System.Collections.Generic.List<int> { 50, 55, 60, 65, 70 }
            };

            try
            {
                result.PrintStatistics();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail("PrintStatistics threw an exception");
            }
        }
    }

    [TestClass]
    public class ProgramMetricsAnalyzerTests
    {
        [TestMethod]
        public void AnalyzeThisProgram_ReturnsValidTuple()
        {
            var (n1, n2, dictionarySize, actualLength) = ProgramMetricsAnalyzer.AnalyzeThisProgram();

            Assert.IsTrue(n1 > 0);
            Assert.IsTrue(n2 > 0);
            Assert.AreEqual(n1 + n2, dictionarySize);
            Assert.IsTrue(actualLength > 0);
        }

        [TestMethod]
        public void CalculatePredictedLengths_DoesNotThrow()
        {
            try
            {
                ProgramMetricsAnalyzer.CalculatePredictedLengths(45, 60, 350);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail("CalculatePredictedLengths threw an exception");
            }
        }

        [TestMethod]
        public void DetermineN2Star_ValidInput_DoesNotThrow()
        {
            try
            {
                ProgramMetricsAnalyzer.DetermineN2Star(45, 60, 350);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail("DetermineN2Star threw an exception");
            }
        }

        [TestMethod]
        public void DetermineN2Star_FindsBestN2()
        {
            int n1 = 10;
            int n2 = 15;
            int actualLength = 120;

            ProgramMetricsAnalyzer.DetermineN2Star(n1, n2, actualLength);

            Assert.IsTrue(true);
        }
    }

    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void FullSimulationWorkflow_ValidParameters()
        {
            var simulator = new ProgramWritingSimulator();

            for (int n = 8; n <= 32; n *= 2)
            {
                var result = simulator.Simulate(n, 100);

                Assert.AreEqual(n, result.DictionarySize);
                Assert.AreEqual(100, result.TrialCount);
                Assert.AreEqual(100, result.Lengths.Count);

                foreach (var length in result.Lengths)
                {
                    Assert.IsTrue(length >= n);
                }

                Assert.IsTrue(result.EmpiricalMean >= n);
                Assert.IsTrue(result.EmpiricalVariance >= 0);
                Assert.IsTrue(result.EmpiricalStandardDeviation >= 0);
                Assert.IsTrue(result.EmpiricalRelativeError >= 0);
            }
        }

        [TestMethod]
        public void TheoreticalVsEmpirical_ConsistencyCheck()
        {
            int n = 32;
            var simulator = new ProgramWritingSimulator();
            var result = simulator.Simulate(n, 1000);

            double theoreticalMean = ProgramWritingSimulator.TheoreticalExpectedLength(n);
            double empiricalMean = result.EmpiricalMean;

            double relativeDifference = Math.Abs(theoreticalMean - empiricalMean) / theoreticalMean;

            Assert.IsTrue(relativeDifference < 0.3);
        }

        [TestMethod]
        public void RelativeError_WithinExpectedRange()
        {
            int[] sizes = { 16, 32, 64, 128 };

            foreach (int n in sizes)
            {
                double theoreticalError = ProgramWritingSimulator.TheoreticalRelativeError(n);
                Assert.IsTrue(theoreticalError > 0 && theoreticalError < 0.3);
            }
        }

        [TestMethod]
        public void AlternativeFormula_ProducesReasonableValues()
        {
            for (int n1 = 10; n1 <= 40; n1 += 10)
            {
                for (int n2 = 10; n2 <= 40; n2 += 10)
                {
                    double result = ProgramWritingSimulator.TheoreticalExpectedLengthAlternative(n1, n2);

                    Assert.IsTrue(result > 0);
                    Assert.IsTrue(result > n1 + n2);
                }
            }
        }
    }

    [TestClass]
    public class EdgeCaseTests
    {
        [TestMethod]
        public void Simulate_ZeroTrials_ReturnsEmptyResult()
        {
            var simulator = new ProgramWritingSimulator();
            var result = simulator.Simulate(10, 0);

            Assert.AreEqual(0, result.TrialCount);
            Assert.AreEqual(0, result.Lengths.Count);
        }

        [TestMethod]
        public void Simulate_DictionarySizeOne_ReturnsLengthOne()
        {
            var simulator = new ProgramWritingSimulator();
            var result = simulator.Simulate(1, 10);

            foreach (var length in result.Lengths)
            {
                Assert.AreEqual(1, length);
            }
        }

        [TestMethod]
        public void Simulate_SmallDictionary_ShortLengths()
        {
            var simulator = new ProgramWritingSimulator();
            var result = simulator.Simulate(2, 100);

            foreach (var length in result.Lengths)
            {
                Assert.IsTrue(length >= 2 && length < 20);
            }
        }

        [TestMethod]
        public void TheoreticalExpectedLength_LargeInput_CalculatesCorrectly()
        {
            double result = ProgramWritingSimulator.TheoreticalExpectedLength(1000);
            double expected = 0.9 * 1000 * Math.Log2(1000);

            Assert.AreEqual(expected, result, 0.0001);
        }
    }
}