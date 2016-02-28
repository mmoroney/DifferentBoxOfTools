using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbolic;
using Symbolic.Complex;

namespace SymbolicTests
{
    [TestClass]
    public class ComplexSymbolTests
    {
        [TestMethod]
        public void ComplexSymbolOperations()
        {
            ComplexSymbol c = new ComplexSymbol(new Variable("x"), new Variable("y"));
            Assert.AreEqual(c - c, ComplexSymbol.Zero);
            Assert.AreEqual(c + c, 2 * c);
            Assert.AreEqual(c + ComplexSymbol.Zero, c);
            Assert.AreEqual(c * ComplexSymbol.One, c);
            Assert.AreEqual(ComplexSymbol.One * ComplexSymbol.One, ComplexSymbol.One);
            Assert.AreEqual(c * ComplexSymbol.Zero, ComplexSymbol.Zero);
            Assert.AreEqual(ComplexSymbol.One * ComplexSymbol.I, ComplexSymbol.I);
        }
    }
}
