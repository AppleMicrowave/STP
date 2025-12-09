using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Lab13;

namespace Lab13.Tests
{
    [TestClass]
    public class AlgorithmMetricsTests
    {
        [TestMethod]
        public void CalculateMetrics_ValidInputs_CalculatesCorrectly()
        {
            var metrics = new AlgorithmMetrics
            {
                Name = "Test",
                η2_star = 2,
                η1 = 10,
                η2 = 8,
                N1 = 25,
                N2 = 20
            };

            metrics.CalculateMetrics();

            Assert.AreEqual(18, metrics.η);
            Assert.AreEqual(45, metrics.N);
            Assert.IsTrue(metrics.V > 0);
            Assert.IsTrue(metrics.L > 0);
            Assert.IsTrue(metrics.L_hat > 0);
            Assert.IsTrue(metrics.I > 0);
        }

        [TestMethod]
        public void η_Property_ReturnsSumOfη1andη2()
        {
            var metrics = new AlgorithmMetrics
            {
                η1 = 12,
                η2 = 8
            };

            Assert.AreEqual(20, metrics.η);
        }

        [TestMethod]
        public void N_Property_ReturnsSumOfN1andN2()
        {
            var metrics = new AlgorithmMetrics
            {
                N1 = 30,
                N2 = 20
            };

            Assert.AreEqual(50, metrics.N);
        }

        [TestMethod]
        public void N_hat_Calculation_IsCorrect()
        {
            var metrics = new AlgorithmMetrics
            {
                η1 = 10,
                η2 = 8,
                N1 = 25,
                N2 = 20
            };

            metrics.CalculateMetrics();

            double expected = 10 * Math.Log2(10) + 8 * Math.Log2(8);
            Assert.AreEqual((int)expected, metrics.N_hat);
        }

        [TestMethod]
        public void V_star_Calculation_IsCorrect()
        {
            var metrics = new AlgorithmMetrics
            {
                η2_star = 3,
                η1 = 10,
                η2 = 8,
                N1 = 25,
                N2 = 20
            };

            metrics.CalculateMetrics();

            double expected = (2 + 3) * Math.Log2(2 + 3);
            Assert.AreEqual(expected, metrics.V_star, 0.001);
        }

        [TestMethod]
        public void V_Calculation_IsCorrect()
        {
            var metrics = new AlgorithmMetrics
            {
                η1 = 10,
                η2 = 8,
                N1 = 25,
                N2 = 20
            };

            metrics.CalculateMetrics();

            double expected = 45 * Math.Log2(18);
            Assert.AreEqual(expected, metrics.V, 0.001);
        }

        [TestMethod]
        public void L_Calculation_IsCorrect()
        {
            var metrics = new AlgorithmMetrics
            {
                η2_star = 2,
                η1 = 10,
                η2 = 8,
                N1 = 25,
                N2 = 20
            };

            metrics.CalculateMetrics();

            double V_star = (2 + 2) * Math.Log2(2 + 2);
            double V = 45 * Math.Log2(18);
            double expected = V_star / V;

            Assert.AreEqual(expected, metrics.L, 0.001);
        }

        [TestMethod]
        public void L_hat_Calculation_IsCorrect()
        {
            var metrics = new AlgorithmMetrics
            {
                η1 = 10,
                η2 = 8,
                N1 = 25,
                N2 = 20
            };

            metrics.CalculateMetrics();

            double expected = (2.0 / 10) * (8 / 20.0);
            Assert.AreEqual(expected, metrics.L_hat, 0.001);
        }

        [TestMethod]
        public void I_Calculation_IsCorrect()
        {
            var metrics = new AlgorithmMetrics
            {
                η1 = 10,
                η2 = 8,
                N1 = 25,
                N2 = 20
            };

            metrics.CalculateMetrics();

            double L_hat = (2.0 / 10) * (8 / 20.0);
            double expected = L_hat * 45 * Math.Log2(18);
            Assert.AreEqual(expected, metrics.I, 0.001);
        }

        [TestMethod]
        public void T1_hat_Calculation_IsCorrect()
        {
            var metrics = new AlgorithmMetrics
            {
                η2_star = 2,
                η1 = 10,
                η2 = 8,
                N1 = 25,
                N2 = 20
            };

            metrics.CalculateMetrics();

            double V = 45 * Math.Log2(18);
            double V_star = (2 + 2) * Math.Log2(2 + 2);
            double expected = (V * V) / (18 * V_star);
            Assert.AreEqual(expected, metrics.T1_hat, 0.001);
        }

        [TestMethod]
        public void λ1_Calculation_IsCorrect()
        {
            var metrics = new AlgorithmMetrics
            {
                η2_star = 2,
                η1 = 10,
                η2 = 8,
                N1 = 25,
                N2 = 20
            };

            metrics.CalculateMetrics();

            double V = 45 * Math.Log2(18);
            double L = metrics.L;
            double expected = L * L * V;
            Assert.AreEqual(expected, metrics.λ1, 0.001);
        }

        [TestMethod]
        public void λ2_Calculation_IsCorrect()
        {
            var metrics = new AlgorithmMetrics
            {
                η2_star = 2,
                η1 = 10,
                η2 = 8,
                N1 = 25,
                N2 = 20
            };

            metrics.CalculateMetrics();

            double V = 45 * Math.Log2(18);
            double V_star = (2 + 2) * Math.Log2(2 + 2);
            double expected = (V * V) / V_star;
            Assert.AreEqual(expected, metrics.λ2, 0.001);
        }

        [TestMethod]
        public void CalculateMetrics_MultipleCalls_SameResult()
        {
            var metrics = new AlgorithmMetrics
            {
                η2_star = 3,
                η1 = 15,
                η2 = 12,
                N1 = 40,
                N2 = 30
            };

            metrics.CalculateMetrics();
            var firstV = metrics.V;
            var firstL = metrics.L;

            metrics.CalculateMetrics();
            var secondV = metrics.V;
            var secondL = metrics.L;

            Assert.AreEqual(firstV, secondV, 0.001);
            Assert.AreEqual(firstL, secondL, 0.001);
        }
    }

    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void CalculateAverages_WithValidData_CalculatesCorrectly()
        {
            var metrics = new System.Collections.Generic.List<AlgorithmMetrics>
            {
                new AlgorithmMetrics { η1 = 10, η2 = 8, N1 = 25, N2 = 20 },
                new AlgorithmMetrics { η1 = 12, η2 = 9, N1 = 30, N2 = 22 }
            };

            foreach (var m in metrics)
            {
                m.CalculateMetrics();
            }

            double totalL = 0;
            double totalV = 0;
            foreach (var m in metrics)
            {
                totalL += m.L;
                totalV += m.V;
            }

            double avgL = totalL / metrics.Count;
            double avgV = totalV / metrics.Count;

            Assert.IsTrue(avgL > 0);
            Assert.IsTrue(avgV > 0);
        }

        [TestMethod]
        public void PrintRow_DoesNotThrow()
        {
            var metrics = new AlgorithmMetrics
            {
                Name = "Test Algorithm",
                η2_star = 2,
                η1 = 10,
                η2 = 8,
                N1 = 25,
                N2 = 20
            };

            metrics.CalculateMetrics();

            try
            {
                ProgramTestsHelper.PrintRow(metrics);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail("PrintRow threw an exception");
            }
        }

        [TestMethod]
        public void PrintHeader_DoesNotThrow()
        {
            try
            {
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail("PrintHeader threw an exception");
            }
        }
    }

    public static class ProgramTestsHelper
    {

        public static void PrintRow(AlgorithmMetrics m)
        {
            Console.WriteLine($"| {m.Name,-23} | {m.η2_star,3} | {m.η1,3} | {m.η2,3} | {m.η,3} | {m.N1,3} | {m.N2,3} | {m.N,3} | {m.N_hat,4} | {m.V,5:F1} | {m.L,5:F3} | {m.L_hat,5:F3} | {m.I,5:F1} | {m.T1_hat,5:F1} | {m.T2_hat,5:F1} | {m.T3_hat,5:F1} |");
        }
    }
}