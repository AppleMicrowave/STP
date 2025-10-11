#include "Lab3Lib.h"

int Lab3Lib::cyclicShift(int a, int n, bool direction = false) {
    std::string numStr = std::to_string(a);
    int len = numStr.length();

    if (len == 0) return 0;

    n = n % len; // Нормализуем сдвиг

    if (!direction) {
        return std::stoi(numStr.substr(n) + numStr.substr(0, n));
    }
    else {
        return std::stoi(numStr.substr(len - n) + numStr.substr(0, len - n));
    }
}

long long Lab3Lib::fibonacci(int n) {
    if (n <= 0) return 0;
    if (n == 1) return 1;

    long long a = 0, b = 1;
    for (int i = 2; i <= n; i++) {
        long long temp = a + b;
        a = b;
        b = temp;
    }
    return b;
}

int Lab3Lib::removeDigits(int a, int p, int n) {
    std::string numStr = std::to_string(a);
    int len = numStr.length();

    if (p < 1 || p > len || n < 0) return a;
    if (p + n - 1 > len) n = len - p + 1; // проверка выхода за границу

    // удаляем n цифр начиная с позиции p
    std::string result = numStr.substr(0, p - 1) + numStr.substr(p - 1 + n);

    return result.empty() ? 0 : std::stoi(result);
}

double Lab3Lib::sumAboveSecondaryDiagonal(const std::vector<std::vector<double>>& A) {
    int rows = A.size();
    if (rows == 0) return 0.0;

    int cols = A[0].size();
    double sum = 0.0;

    for (int i = 0; i < rows; i++) {
        for (int j = 0; j < cols; j++) {
            if (i + j < rows - 1 && (i + j) % 2 == 0) {
                sum += A[i][j];
            }
        }
    }

    return sum;
}
