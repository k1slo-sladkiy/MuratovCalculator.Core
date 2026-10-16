using Microsoft.VisualStudio.TestTools.UnitTesting;
using MuratovCalculator.Core;

namespace MuratovCalculator.Tests;

[TestClass]
public class CalculatorTests
{
    private readonly Calculator _calc = new();

    [TestMethod]
    public void Add_ReturnsSum()
        => Assert.AreEqual(5.0, _calc.Add(2, 3), 1e-9);

    [TestMethod]
    public void Subtract_ReturnsDifference()
        => Assert.AreEqual(-1.0, _calc.Subtract(2, 3), 1e-9);

    [TestMethod]
    public void Multiply_ReturnsProduct()
        => Assert.AreEqual(6.0, _calc.Multiply(2, 3), 1e-9);

    [TestMethod]
    public void Divide_ReturnsQuotient()
        => Assert.AreEqual(2.5, _calc.Divide(5, 2), 1e-9);

    [TestMethod]
    [ExpectedException(typeof(DivideByZeroException))]
    public void Divide_ByZero_Throws()
        => _calc.Divide(5, 0);

    [TestMethod]
    public void Power_ReturnsPower()
        => Assert.AreEqual(8.0, _calc.Power(2, 3), 1e-9);

    [TestMethod]
    public void Power_FractionalExponent_ReturnsRoot()
        => Assert.AreEqual(3.0, _calc.Power(9, 0.5), 1e-9);

    [TestMethod]
    public void Calculate_AllOperators_Work()
    {
        Assert.AreEqual(5.0, _calc.Calculate(2, 3, "+"), 1e-9);
        Assert.AreEqual(-1.0, _calc.Calculate(2, 3, "-"), 1e-9);
        Assert.AreEqual(6.0, _calc.Calculate(2, 3, "*"), 1e-9);
        Assert.AreEqual(0.5, _calc.Calculate(1, 2, "/"), 1e-9);
        Assert.AreEqual(8.0, _calc.Calculate(2, 3, "^"), 1e-9);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Calculate_UnknownOperator_Throws()
        => _calc.Calculate(2, 3, "%");

    [TestMethod]
    [ExpectedException(typeof(DivideByZeroException))]
    public void Calculate_DivideByZero_Throws()
        => _calc.Calculate(1, 0, "/");
}