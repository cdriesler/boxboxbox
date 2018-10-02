using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;

namespace Cicero.Core.Logic.Solve
{
    public static class Utils
    {
        public static List<Curve> GetPieces(Curve input, Curve box)
        {
            var ccx = Intersection.CurveCurve(input, box, 0.1, 0.1);

            /*
            var boxBrep = Brep.CreatePlanarBreps(box)[0];

            var pieces = input.Split(boxBrep, 0.1);

            var insidePieces = new List<Curve>();
            var outsidePieces = new List<Curve>();

            foreach (Curve curve in pieces)
            {
                if (box.Contains(curve.PointAtNormalizedLength(0.5)).Equals(PointContainment.Inside))
                {
                    insidePieces.Add(curve);
                }
                else
                {
                    outsidePieces.Add(curve);
                }
            }
            */

            return new List<Curve>{new LineCurve(input.PointAtStart, ccx[0].PointA), new LineCurve(ccx[0].PointA, ccx[1].PointA), new LineCurve(ccx[1].PointA, input.PointAtEnd)};
        }

        public static List<Point3d> GetAllCurveIntersections(Curve referenceCurve, List<Curve> otherCurves, bool unique = true)
        {
            //Console.WriteLine("Function called!");

            List<Point3d> allIntersectionPoints = new List<Point3d>();

            foreach (Curve curve in otherCurves)
            {
               CurveIntersections ccx = Rhino.Geometry.Intersect.Intersection.CurveCurve(referenceCurve, curve, 0.1, 0.1);

                //Console.WriteLine("CCX Run!");

                for (int i = 0; i < ccx.Count; i++)
                {
                    if (ccx[i].IsPoint)
                    {
                        allIntersectionPoints.Add(ccx[i].PointA);
                    }
                }

                //Console.WriteLine("Curve");
            }

            if (allIntersectionPoints.Count == 0)
            {
                return allIntersectionPoints;
            }

            if (!unique)
            {
                return allIntersectionPoints;
            }
            else
            {
                return new List<Point3d>(Point3d.CullDuplicates(allIntersectionPoints, 0.1));
            }
        }
    }
}
