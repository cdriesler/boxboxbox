using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cicero.Core.Formats;
using Rhino.Geometry;

namespace Cicero.Core.Logic.Solve
{
    public static class Solve
    {
        public static List<BoxResult> Request(Request req, Curve bounds)
        {
            var results = new List<BoxResult>();

            foreach (Curve inputCrv in req.Inputs)
            {
                var intersectsAny = false;

                foreach (BoxInput box in req.Boxes)
                {
                    if (Check.IfCurvesIntersect(inputCrv, box.Bounds))
                    {
                        intersectsAny = true;

                        results.Add(Verbs.Enact(inputCrv, box));

                        //results.Add(Adverbs.Enact(verbRes));
                    }
                }

                if (!intersectsAny)
                {
                    results.Add(new BoxResult(new List<Curve>{inputCrv}, new List<Curve>(), new List<Curve>(), new List<Curve>()));
                }
            }

            return results;
        }

        public static List<BoxResult> CornerCases(List<BoxResult> results, Curve bounds)
        {
            var cornerCases = new List<BoxResult>();

            var allCurves = new List<Curve>();

            foreach (BoxResult result in results)
            {
                allCurves.Add(result.Original[0]);

                foreach (Curve crv in result.InternalVerb)
                {
                    allCurves.Add(crv);
                }
            }

            var boundsBox = bounds.GetBoundingBox(Plane.WorldXY);
            var width = boundsBox.Max.X - boundsBox.Min.X;

            foreach (Curve crv in allCurves)
            {
                var cxResult = Utils.GetAllCurveIntersections(crv, allCurves);

                if (cxResult.Count > 5)
                {
                    foreach (Point3d pt in cxResult)
                    {
                        var errorCurve = new LineCurve(pt, new Point3d(width * 2, pt.Y, 0));

                        var cornerRes = new BoxResult(errorCurve);
                        cornerRes.Verb = "error";

                        cornerCases.Add(cornerRes);
                    }
                }
            }

            results.AddRange(cornerCases);

            return results;
        }
    }
}
