#pragma once
#include <string>
#include <vector>

class Lab3Lib
{
public:
    static int cyclicShift(int a, int n, bool direction);

    static long long fibonacci(int n);

    static int removeDigits(int a, int p, int n);

    static double sumAboveSecondaryDiagonal(const std::vector<std::vector<double>>& A);
};

