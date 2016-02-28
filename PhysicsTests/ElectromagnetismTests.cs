using Microsoft.VisualStudio.TestTools.UnitTesting;
using Physics.Electromagnetism;
using Symbolic;
using Symbolic.Matrix.Lorentz;
using Symbolic.Operators;
using Symbolic.Vector.Euclidean;
using Symbolic.Vector.Lorentz;
using System;

namespace PhysicsTests
{
    [TestClass]
    public class ElectromagnetismTests
    {
        static EuclideanVector3 v = new EuclideanVector3(new Variable("v"));
        [TestMethod]
        public void PointCharge()
        {
            TestPotential(x => new LorentzVectorU(1 / x.Vector.Length), v);
        }

        [TestMethod]
        public void ChargedWire()
        {
            TestPotential(x => new LorentzVectorU(Functions.Log(Functions.Sqrt(x.Vector[1] * x.Vector[1] + x.Vector[2] * x.Vector[2]))), v);
        }

        [TestMethod]
        public void UnchargedWireWithCurrent()
        {
            TestPotential(x => new LorentzVectorU(Symbol.Zero, Functions.Log(Functions.Sqrt(x.Vector[1] * x.Vector[1] + x.Vector[2] * x.Vector[2]))), v);
        }

        [TestMethod]
        public void PlaneCharge()
        {
            TestPotential(x => new LorentzVectorU(x.Vector[0]), v);
        }

        [TestMethod]
        public void UnchargedPlaneWithCurrent()
        {
            TestPotential(x => new LorentzVectorU(Symbol.Zero, x.Vector[2]), v);
        }

        static void TestPotential(Func<LorentzVectorVariableU, LorentzVectorU> createPotential, EuclideanVector3 v)
        {
            LorentzVectorVariableU coordinates = new LorentzVectorVariableU("t", "x", "y", "z");

            LorentzVectorU potential = createPotential(coordinates);
            LorentzVectorOperatorL del = coordinates.Del;

            ElectromagneticField field0 = new ElectromagneticField(potential, del);
            TestField(field0);

            ElectromagneticField field1 = field0.Transform(v);
            TestField(field1);
            TestFieldTransforms(field0.ElectricField, field0.MagneticField, field1.ElectricField, field1.MagneticField, v);
            CompareInvariantScalars(field0, field1);

            ElectromagneticField field2 = field1.Transform(-v);
            TestField(field2);
            TestFieldTransforms(field1.ElectricField, field1.MagneticField, field2.ElectricField, field2.MagneticField, -v);
            CompareInvariantScalars(field1, field2);

            Assert.AreEqual(field0.Potential, field2.Potential, "A dual transform");
            Assert.AreEqual(field0.Del.ToString(), field2.Del.ToString(), "Del dual transform");
            Assert.AreEqual(field0.ElectricField, field2.ElectricField, "E dual transform");
            Assert.AreEqual(field0.MagneticField, field2.MagneticField, "B dual transform");
            Assert.AreEqual(field0.CurrentDensity, field2.CurrentDensity, "J dual transform");
        }

        static void TestField(ElectromagneticField field)
        {
            TestFieldStrength(field.FieldStrengthTensor, field.ElectricField, field.MagneticField);
            TestDualFieldStrength(field.DualFieldStrengthTensor, field.ElectricField, field.MagneticField);
            TestMaxwellsEquations(field);
            TestBianchiIdentities(field.FieldStrengthTensor, field.Del.Invert());
            TestCurrentDensity(field.CurrentDensity, field.Del);
        }

        static void CompareInvariantScalars(ElectromagneticField field1, ElectromagneticField field2)
        {
            EuclideanVector3 E1 = field1.ElectricField;
            EuclideanVector3 B1 = field1.MagneticField;
            EuclideanVector3 E2 = field2.ElectricField;
            EuclideanVector3 B2 = field2.MagneticField;
            //Symbol comparison failure
            // Expected:<2(y^2+z^2)^-1>. Actual:<2(-v^2+1)^-1*(y^2+z^2)^-2*(z^2+v^2*(-z^2-y^2)+y^2)>.
            //Assert.AreEqual(field1.FieldStrengthTensor.InvariantScalar, field2.FieldStrengthTensor.InvariantScalar);
            Assert.AreEqual(field1.FieldStrengthTensor.Dot(field1.DualFieldStrengthTensor), field2.FieldStrengthTensor.Dot(field2.DualFieldStrengthTensor));
        }

        static void TestFieldStrength(LorentzMatrixUU F, EuclideanVector3 E, EuclideanVector3 B)
        {
            Assert.AreEqual(F[0, 0], Symbol.Zero, "F[0, 0]");
            Assert.AreEqual(F[0, 1], E[0], "F[0, 1]");
            Assert.AreEqual(F[0, 2], E[1], "F[0, 2]");
            Assert.AreEqual(F[0, 3], E[2], "F[0, 3]");
            Assert.AreEqual(F[1, 0], -E[0], "F[1, 0]");
            Assert.AreEqual(F[1, 1], Symbol.Zero, "F[1, 1]");
            Assert.AreEqual(F[1, 2], B[2], "F[1, 2]");
            Assert.AreEqual(F[1, 3], -B[1], "F[1, 3]");
            Assert.AreEqual(F[2, 0], -E[1], "F[2, 0]");
            Assert.AreEqual(F[2, 1], -B[2], "F[2, 1]");
            Assert.AreEqual(F[2, 2], Symbol.Zero, "F[2, 2]");
            Assert.AreEqual(F[2, 3], B[0], "F[2, 3]");
            Assert.AreEqual(F[3, 0], -E[2], "F[3, 0]");
            Assert.AreEqual(F[3, 1], B[1], "F[3, 1]");
            Assert.AreEqual(F[3, 2], -B[0], "F[3, 2]");
            Assert.AreEqual(F[3, 3], Symbol.Zero, "F[3, 3]");
        }

        static void TestDualFieldStrength(LorentzMatrixLL F, EuclideanVector3 E, EuclideanVector3 B)
        {
            Assert.AreEqual(F[0, 0], Symbol.Zero, "~F[0, 0]");
            Assert.AreEqual(F[0, 1], -B[0], "~F[0, 1]");
            Assert.AreEqual(F[0, 2], -B[1], "~F[0, 2]");
            Assert.AreEqual(F[0, 3], -B[2], "~F[0, 3]");
            Assert.AreEqual(F[1, 0], B[0], "~F[1, 0]");
            Assert.AreEqual(F[1, 1], Symbol.Zero, "~F[1, 1]");
            Assert.AreEqual(F[1, 2], -E[2], "~F[1, 2]");
            Assert.AreEqual(F[1, 3], E[1], "~F[1, 3]");
            Assert.AreEqual(F[2, 0], B[1], "~F[2, 0]");
            Assert.AreEqual(F[2, 1], E[2], "~F[2, 1]");
            Assert.AreEqual(F[2, 2], Symbol.Zero, "F~[2, 2]");
            Assert.AreEqual(F[2, 3], -E[0], "~F[2, 3]");
            Assert.AreEqual(F[3, 0], B[2], "~F[3, 0]");
            Assert.AreEqual(F[3, 1], -E[1], "~F[3, 1]");
            Assert.AreEqual(F[3, 2], E[0], "~F[3, 2]");
            Assert.AreEqual(F[3, 3], Symbol.Zero, "~F[3, 3]");
        }

        static void TestMaxwellsEquations(ElectromagneticField F)
        {
            Operator dt = F.Del.Scalar;
            EuclideanVector3Operator del = F.Del.Vector;
            Symbol chargeDensity = F.CurrentDensity.Scalar;
            EuclideanVector3 currentDensity = F.CurrentDensity.Vector;

            Assert.AreEqual(del.Dot(F.ElectricField), chargeDensity, "Gauss' Law:");
            Assert.AreEqual(del.Dot(F.MagneticField), Symbol.Zero, "Gauss' Law for Magnetism:");
            Assert.AreEqual(del.Cross(F.ElectricField) + dt * F.MagneticField, EuclideanVector3.Zero, "Faraday's Law of Induction");
            Assert.AreEqual(del.Cross(F.MagneticField) - dt * F.ElectricField, currentDensity, "Ampere's Law");
        }

        static void TestBianchiIdentities(LorentzMatrixUU F, LorentzVectorOperatorU del)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        Assert.AreEqual(del[i] * F[j, k] + del[j] * F[k, i] + del[k] * F[i, j], Symbol.Zero, string.Format("Bianchi {0} {1} {2}", i, j, k));
                    }
                }
            }
        }

        static void TestCurrentDensity(LorentzVectorU J, LorentzVectorOperatorL del)
        {
            Assert.AreEqual(del.Dot(J), Symbol.Zero, "Divergence of current density");
        }

        static void TestFieldTransforms(EuclideanVector3 E, EuclideanVector3 B, EuclideanVector3 Ep, EuclideanVector3 Bp, EuclideanVector3 v)
        {
            Symbol gamma = LorentzTransform.Gamma(v);
            Assert.AreEqual(Ep.ParallelComponent(v), E.ParallelComponent(v), "E Parallel component:");
            Assert.AreEqual(Ep.PerpendicularComponent(v), gamma * (E + v.Cross(B)).PerpendicularComponent(v), "E Perpendicular component:");
            Assert.AreEqual(Bp.ParallelComponent(v), B.ParallelComponent(v), "B Parallel component:");
            Assert.AreEqual(Bp.PerpendicularComponent(v), gamma * (B - v.Cross(E)).PerpendicularComponent(v), "B Perpendicular component:");
        }
    }
}
