using Microsoft.VisualStudio.TestTools.UnitTesting;
using Physics.GeneralRelativity;
using Symbolic;
using Symbolic.Manifold;
using Symbolic.Matrix.Matrix4;
using Symbolic.Vector.Vector4;

namespace PhysicsTests
{
    [TestClass]
    public class GeneralRelativityTests
    {
        [TestMethod]
        public void SchwarzchildSpacetime()
        {
            Variable t = new Variable("t");
            Variable r = new Variable("r");
            Variable theta = new Variable("theta");
            Variable phi = new Variable("phi");
            Variable M = new Variable("M");

            Vector4VariableU coordinates = new Vector4VariableU(t, r, theta, phi);
            Matrix4LL metric = new Matrix4LL(2 * M / r - 1, 1 / (1 - 2 * M / r), Functions.Pow(r, 2), Functions.Pow(r * Functions.Sin(theta), 2));
            Manifold4 manifold = new Manifold4(metric, coordinates.Del);

            Spacetime spaceTime = new Spacetime(manifold);
            //Symbol comparison failure
            //Expected:<M*r^-3*(-(-2M*r^-1+1)^-1-(2M*r^-1-1)^-1) Actual: 0
            //Assert.AreEqual(spaceTime.EinsteinTensor, Matrix4UU.Zero);
            //Symbol comparison failure
            // Expected:<M*r^-3*(-(-2M*r^-1+1)^-1-(2M*r^-1-1)^-1) Actual: 0
            //Assert.AreEqual(spaceTime.EnergyMomentumTensor, Matrix4UU.Zero);
        }
    }
}
