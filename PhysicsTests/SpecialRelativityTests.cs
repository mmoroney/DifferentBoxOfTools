using Microsoft.VisualStudio.TestTools.UnitTesting;
using Physics.SpecialRelativity;
using Symbolic;
using Symbolic.Vector.Euclidean;
using System;

namespace PhysicsTests
{
    [TestClass]
    public class SpecialRelativityTests
    {
        static EuclideanVector3 v = new EuclideanVector3(new Variable("v"));
        [TestMethod]
        public void StationaryParticle()
        {
            TestRelativisticParticle(t =>
            {
                return EuclideanVector3.Zero;
            });
        }

        [TestMethod]
        public void ParticleAtConstantSpeed()
        {
            TestRelativisticParticle(t => {
                return v * t;
            });
        }

        public void TestRelativisticParticle(Func<Symbol, EuclideanVector3> createPosition)
        {
            Variable m = new Variable("m");
            Variable t = new Variable("t");
            RelativisticParticle particle0 = new RelativisticParticle(m, createPosition(t), t);

            TestVelocity(particle0);
            TestMomentum(particle0);

            RelativisticParticle particle1 = particle0.Transform(v);

            TestVelocity(particle1);
            TestMomentum(particle1);
        }

        static void TestVelocity(RelativisticParticle particle)
        {
            Assert.AreEqual(particle.Velocity.InvariantScalar, -Symbol.One, "Velocity scalar product");
        }

        static void TestMomentum(RelativisticParticle particle)
        {
            Assert.AreEqual(particle.Momentum.InvariantScalar, -Functions.Pow(particle.Mass, 2), "Momentum scalar product");
        }
    }
}
