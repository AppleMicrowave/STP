
#include "pch.h"
#include "../../Lab3/Lab3Lib.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace UnitTest3
{
    TEST_CLASS(UnitTest3)
    {
    public:

        TEST_METHOD(CyclicShift_LeftShift_ValidInput)
        {
            int result = Lab3Lib::cyclicShift(12345, 2, false);
            Assert::AreEqual(34512, result);
        }

        TEST_METHOD(CyclicShift_RightShift_ValidInput)
        {
            int result = Lab3Lib::cyclicShift(12345, 2, true);
            Assert::AreEqual(45123, result);
        }

        TEST_METHOD(CyclicShift_ShiftMoreThanLength_Left)
        {
            int result = Lab3Lib::cyclicShift(123, 5, false);
            Assert::AreEqual(231, result); // 5 % 3 = 2
        }

        TEST_METHOD(CyclicShift_ShiftMoreThanLength_Right)
        {
            int result = Lab3Lib::cyclicShift(123, 5, true);
            Assert::AreEqual(312, result); // 5 % 3 = 2
        }

        TEST_METHOD(CyclicShift_ZeroShift_NoChange)
        {
            int result = Lab3Lib::cyclicShift(12345, 0, false);
            Assert::AreEqual(12345, result);
        }

        TEST_METHOD(CyclicShift_SingleDigit_NoChange)
        {
            int result = Lab3Lib::cyclicShift(5, 3, false);
            Assert::AreEqual(5, result);
        }

        TEST_METHOD(Fibonacci_PositiveNumber_ValidResult)
        {
            long long result = Lab3Lib::fibonacci(10);
            Assert::AreEqual(55LL, result);
        }

        TEST_METHOD(Fibonacci_FirstElement_Returns1)
        {
            long long result = Lab3Lib::fibonacci(1);
            Assert::AreEqual(1LL, result);
        }

        TEST_METHOD(Fibonacci_SecondElement_Returns1)
        {
            long long result = Lab3Lib::fibonacci(2);
            Assert::AreEqual(1LL, result);
        }

        TEST_METHOD(Fibonacci_Zero_Returns0)
        {
            long long result = Lab3Lib::fibonacci(0);
            Assert::AreEqual(0LL, result);
        }

        TEST_METHOD(Fibonacci_Negative_Returns0)
        {
            long long result = Lab3Lib::fibonacci(-5);
            Assert::AreEqual(0LL, result);
        }

        TEST_METHOD(RemoveDigits_ValidInput_RemovesCorrectDigits)
        {
            int result = Lab3Lib::removeDigits(123456, 2, 3);
            Assert::AreEqual(156, result); 
        }

        TEST_METHOD(RemoveDigits_RemoveFromStart)
        {
            int result = Lab3Lib::removeDigits(123456, 1, 2);
            Assert::AreEqual(3456, result);
        }

        TEST_METHOD(RemoveDigits_RemoveFromEnd)
        {
            int result = Lab3Lib::removeDigits(123456, 5, 2);
            Assert::AreEqual(1234, result);
        }

        TEST_METHOD(RemoveDigits_RemoveAllDigits_Returns0)
        {
            int result = Lab3Lib::removeDigits(123, 1, 3);
            Assert::AreEqual(0, result);
        }

        TEST_METHOD(RemoveDigits_InvalidPosition_ReturnsOriginal)
        {
            int result = Lab3Lib::removeDigits(123, 5, 2);
            Assert::AreEqual(123, result);
        }

        TEST_METHOD(RemoveDigits_NegativeCount_ReturnsOriginal)
        {
            int result = Lab3Lib::removeDigits(123, 1, -1);
            Assert::AreEqual(123, result);
        }

        TEST_METHOD(RemoveDigits_CountExceedsLength_RemovesToEnd)
        {
            int result = Lab3Lib::removeDigits(12345, 3, 10);
            Assert::AreEqual(12, result);
        }

        TEST_METHOD(SumAboveSecondaryDiagonal_ValidMatrix_CalculatesCorrectSum)
        {
            std::vector<std::vector<double>> matrix = {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
                {7.0, 8.0, 9.0}
            };
            double result = Lab3Lib::sumAboveSecondaryDiagonal(matrix);
            Assert::AreEqual(1.0, result);
        }

        TEST_METHOD(SumAboveSecondaryDiagonal_EmptyMatrix_Returns0)
        {
            std::vector<std::vector<double>> matrix;
            double result = Lab3Lib::sumAboveSecondaryDiagonal(matrix);
            Assert::AreEqual(0.0, result);
        }

        TEST_METHOD(SumAboveSecondaryDiagonal_SingleElement_Returns0)
        {
            std::vector<std::vector<double>> matrix = { {5.0} };
            double result = Lab3Lib::sumAboveSecondaryDiagonal(matrix);
            Assert::AreEqual(0.0, result);
        }

        TEST_METHOD(SumAboveSecondaryDiagonal_2x2Matrix_CalculatesCorrectSum)
        {
            std::vector<std::vector<double>> matrix = {
                {1.0, 2.0},
                {3.0, 4.0}
            };
            double result = Lab3Lib::sumAboveSecondaryDiagonal(matrix);
            Assert::AreEqual(1.0, result);
        }

        TEST_METHOD(SumAboveSecondaryDiagonal_4x4Matrix_CalculatesCorrectSum)
        {
            std::vector<std::vector<double>> matrix = {
                {1.0, 2.0, 3.0, 4.0},
                {5.0, 6.0, 7.0, 8.0},
                {9.0, 10.0, 11.0, 12.0},
                {13.0, 14.0, 15.0, 16.0}
            };
            double result = Lab3Lib::sumAboveSecondaryDiagonal(matrix);
            Assert::AreEqual(19.0, result);
        }

        TEST_METHOD(SumAboveSecondaryDiagonal_RectangularMatrix_CalculatesCorrectSum)
        {
            std::vector<std::vector<double>> matrix = {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0}
            };
            double result = Lab3Lib::sumAboveSecondaryDiagonal(matrix);
            Assert::AreEqual(1.0, result);
        }
    };
}