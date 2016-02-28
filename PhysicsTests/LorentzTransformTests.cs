using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbolic;
using Symbolic.Matrix.Lorentz;
using Symbolic.Vector.Euclidean;
using Symbolic.Vector.Lorentz;

namespace PhysicsTests
{
    [TestClass]
    public class LorentzTransformTests
    {
        static EuclideanVector3 v = new EuclideanVector3(new Variable("v"));
        [TestMethod]
        public void LorentzBoost()
        {
            TestLorentzTransform(v);
        }

        [TestMethod]
        public void IdentityLorentzTransform()
        {
            TestLorentzTransform(EuclideanVector3.Zero);
        }

        static void TestLorentzTransform(EuclideanVector3 v)
        {
            LorentzVectorVariableU x0 = new LorentzVectorVariableU("t", "x", "y", "z");
            
            LorentzVectorU x1 = x0.Transform(v);
            LorentzVectorU x2 = x1.Transform(-v);

            Assert.AreEqual(x0, x2);
            Assert.AreEqual(LorentzMatrixUL.One, LorentzTransform.Matrix(v) * LorentzTransform.Matrix(-v));
        }
    }
}
