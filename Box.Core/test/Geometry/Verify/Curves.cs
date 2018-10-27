using System;
using Factory = Box.Core.Geometry.Create;
using Box.Core.Geometry.Verify;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Compute;
using Rhino.Geometry;

namespace Box.Core.Test.Geometry.Verify
{
    [TestClass]
    public class CurvesIntersectTests
    {
        [TestMethod]
        public void UnitXUnitY_ReturnsTrue()
        {
            var crvA = Factory.Curves.UnitXCurve(1);
            var crvB = Factory.Curves.UnitYCurve(1);

            var result = Curves.CurvesIntersect(crvA, crvB);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UnitXUnitXNoOverlap_ReturnsFalse()
        {
            var crvA = Factory.Curves.UnitXCurve(1);
            var crvB = Factory.Curves.UnitXCurve(1);

            var result = Curves.CurvesIntersect(crvA, crvB);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UnitXUnitXYesOverlap_ReturnsTrue()
        {
            var crvA = Factory.Curves.UnitXCurve(1);
            var crvB = Factory.Curves.UnitXCurve(1);

            var result = Curves.CurvesIntersect(crvA, crvB, true);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DisjointCurves_ReturnsFalse()
        {
            var crvA = Factory.Curves.LineSDL(new Point2d(5, 5), new Vector2d(0, 1), 5);
            var crvB = Factory.Curves.UnitXCurve(1);

            var result = Curves.CurvesIntersect(crvA, crvB);

            Assert.IsFalse(result);
        }
    }

    [TestClass]
    public class CurveInRegionTests
    {
        [TestMethod]
        public void UnitSquareSmallUnitX_ReturnsTrue()
        {
            var region = Factory.Curves.RectangleCWH(Point3d.Origin, 2, 2);
            var crv = Factory.Curves.UnitXCurve(0.5);

            var result = Curves.CurveInRegion(region, crv);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UnitSquareSmallUnitXPartial_ReturnsTrue()
        {
            var region = Factory.Curves.RectangleCWH(Point3d.Origin, 2, 2);
            var crv = Factory.Curves.UnitXCurve(0.5);

            var result = Curves.CurveInRegion(region, crv, false);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UnitSquareUnitX_ReturnsTrue()
        {
            var region = Factory.Curves.RectangleCWH(new Point3d(0, 1, 0), 2, 2);
            var crv = new LineCurve(region.PointAtNormalizedLength(0.25), region.PointAtNormalizedLength(0.75));

            var result = Curves.CurveInRegion(region, crv);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UnitSquareUnitXPartial_ReturnsTrue()
        {
            var region = Factory.Curves.RectangleCWH(new Point3d(0, 1, 0), 2, 2);
            var crv = Factory.Curves.UnitXCurve(2);

            var result = Curves.CurveInRegion(region, crv, false);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UnitSquareLargeUnitX_ReturnsFalse()
        {
            var region = Factory.Curves.RectangleCWH(Point3d.Origin, 2, 2);
            var crv = Factory.Curves.UnitXCurve(20);

            var result = Curves.CurveInRegion(region, crv);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UnitSquareLargeUnitXPartial_ReturnsTrue()
        {
            var region = Factory.Curves.RectangleCWH(Point3d.Origin, 2, 2);
            var crv = Factory.Curves.UnitXCurve(20);

            var result = Curves.CurveInRegion(region, crv, false);

            Assert.IsTrue(result);
        }
    }
}
