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

            return new List<Curve>{new LineCurve(input.PointAtStart, ccx[0].PointA), new LineCurve(ccx[0].PointA, ccx[1].PointA)};
        }
    }
}
