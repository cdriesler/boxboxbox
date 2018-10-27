using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Rhino.Compute;

namespace Box.Core.Geometry.Modify
{
    public static class Curves
    {
        /// <summary>
        /// Shatter a line at its intersections with a region. Return segments either inside or outside the region.
        /// </summary>
        /// <param name="line">Line to trim.</param>
        /// <param name="region">Region to trim with.</param>
        /// <param name="inside">Return segments inside the region if true, outside if false.</param>
        /// <returns></returns>
        public static List<Curve> TrimLineWithRegion(Curve line, Curve region, bool inside = true)
        {
            var ccx = Rhino.Compute.Intersect.IntersectionCompute.CurveCurve(line, region, 0.1, 0.1);

            var cxPoints = ccx.Where(x => x.IsPoint).Select(x => x.PointA).ToList();

            var segments = new List<Curve>();

            for (int i = 0; i < cxPoints.Count; i++)
            {
                var startPt = i == 0 ? line.PointAtStart : cxPoints[i - 1];
                var endPt = i == cxPoints.Count - 1 ? line.PointAtEnd : cxPoints[i];

                segments.Add(new LineCurve(startPt, endPt));
            }

            return inside ? segments.Where(x => Verify.Curves.CurveInRegion(region, x)).ToList() : segments.Where(x => !Verify.Curves.CurveInRegion(region, x)).ToList();
        }
    }
}
