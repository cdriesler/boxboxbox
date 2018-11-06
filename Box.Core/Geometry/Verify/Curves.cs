using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;

namespace Box.Core.Geometry.Verify
{
    public static class Curves
    {
        public static bool CurvesIntersect(Curve crvA, Curve crvB, bool includeOverlap = false)
        {
            var ccx = Intersection.CurveCurve(crvA, crvB, 0.1, 0.1);

            var res = includeOverlap ? ccx.Count : ccx.Count(x => x.IsPoint);

            return res > 0;
        }

        /// <summary>
        /// Test whether curve in contained within a region.
        /// </summary>
        /// <param name="region">Closed curve to check for containment with.</param>
        /// <param name="crv">Curve to test on.</param>
        /// <param name="allInside">If true, only return true if curve is completely within region.</param>
        /// <returns></returns>
        public static bool CurveInRegion(Curve region, Curve crv, bool allInside = true)
        {
            var startPt = crv.PointAtStart;
            var midPt = crv.PointAtNormalizedLength(0.5);
            var endPt = crv.PointAtStart;

            var startPtTest = region.Contains(startPt, Plane.WorldXY);
            var midPtTest = region.Contains(midPt, Plane.WorldXY);
            var endPtTest = region.Contains(endPt, Plane.WorldXY);

            var results = new List<bool>()
            {
                startPtTest == PointContainment.Coincident || startPtTest == PointContainment.Inside,
                midPtTest == PointContainment.Inside,
                endPtTest == PointContainment.Coincident || endPtTest == PointContainment.Inside
            };

            if (!allInside)
            {
                return results.Contains(true);
            }

            var ccx = Intersection.CurveCurve(region, crv, 0.1, 0.1).Where(x => x.IsPoint).ToList();
            Console.WriteLine(ccx.Count.ToString());

            if (ccx.Count > 2)
            {
                return false;
            }

            if (ccx.Count > 0)
            {
                foreach (IntersectionEvent pt in ccx)
                {
                    var test = pt.PointA.DistanceTo(crv.PointAtStart) < 0.1 ||
                               pt.PointA.DistanceTo(crv.PointAtEnd) < 0.1;

                    if (!test)
                    {
                        return false;
                    }
                }

                return !results.Contains(false);
            }

            return !results.Contains(false);
        }
    }
}
