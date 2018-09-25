using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;

namespace Cicero.Core.Logic.Solve
{
    public static class Check
    {
        public static bool IfCurvesIntersect(Curve curveA, Curve curveB)
        {
            var ccx = Intersection.CurveCurve(curveA, curveB, 0.1, 0.1);

            if (ccx.Count < 2)
            {
                return false;
            }

            var isOverlap = false;

            foreach (var cx in ccx)
            {
                if (cx.IsOverlap)
                {
                    isOverlap = true;
                }
            }

            if (isOverlap)
            {
                return false;
            }

            return true;
        }
    }
}
