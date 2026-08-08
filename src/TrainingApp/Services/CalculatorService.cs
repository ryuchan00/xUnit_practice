namespace TrainingApp.Services;

public class CalculatorService
{
    public int Add(int a, int b) => a + b;

    public int Subtract(int a, int b) => a - b;

    public int Divide(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("0で割ることはできません。");
        }

        return a / b;
    }
}
