using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Box.System.Cicero.Formats.Element;
using Rhino.Geometry;

namespace Box.System.Cicero.Logic
{
    public static class Verbs
    {
        static readonly Dictionary<string, Func<BoxElement, bool>> VerbMethods = new Dictionary<string, Func<BoxElement, bool>>()
        {
            { "regulate", Regulate },
        };

        public static void Enact(BoxElement box)
        {
            box.VerbResults = new List<Curve>();

            if (!VerbMethods.TryGetValue(box.Verb, out Func<BoxElement, bool> verbFunc))
            {
                //Verb method is not implemented. Pass input curves as results.
                box.VerbResults.AddRange(box.InputSegments);
            }
            else
            {
                verbFunc(box);
            }
        }

        private static bool Regulate(BoxElement box)
        {
            foreach (Curve crv in box.InputSegments)
            {
                var startPt = crv.PointAtStart;
                var endPt = crv.PointAtEnd;

                var resultCurves = new List<Curve>()
                {
                    new LineCurve(startPt, new Point3d(box.Dims.MinCorner.X, box.Dims.MaxCorner.Y, 0)),
                    new LineCurve(startPt, new Point3d(startPt.X, box.Dims.MinCorner.Y, 0)),
                    new LineCurve(endPt, new Point3d(box.Dims.MaxCorner.X, box.Dims.MinCorner.Y, 0)),
                    new LineCurve(endPt, new Point3d(endPt.X, box.Dims.MaxCorner.Y, 0)),
                };

                box.VerbResults.AddRange(resultCurves);
            }

            return true;
        }
    }
}
