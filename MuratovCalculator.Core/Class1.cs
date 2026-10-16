namespace MuratovCalculator.Core;

public class Calculator
{
    public double Add(double a, double b) => a + b;

    public double Subtract(double a, double b) => a - b;

    public double Multiply(double a, double b) => a * b;

    public double Divide(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException("Деление на ноль невозможно.");
        return a / b;
    }

    public double Power(double a, double b) => Math.Pow(a, b);

    public double Calculate(double a, double b, string op) => op switch
    {
        "+" => Add(a, b),
        "-" => Subtract(a, b),
        "*" => Multiply(a, b),
        "/" => Divide(a, b),
        "^" => Power(a, b),
        _ => throw new ArgumentException($"Неизвестный оператор: {op}", nameof(op))
    };
}