using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cicero.Core.Formats;
using Rhino;
using Rhino.Geometry;

namespace Cicero.Core.Logic.Solve
{
    public static class Verbs
    {
        public static BoxResult Enact(Curve input, BoxInput box)
        {
            var pieces = Utils.GetPieces(input, box.Bounds);

            RhinoApp.WriteLine($"Testing {box.Verb} ({box.Adverb})");

            var routes = new Dictionary<string, Func<Curve, BoxInput, List<Curve>, BoxResult>>()
            {
                { "regulate", Regulate },
                { "capture", Capture },
                { "thicken", Thicken },
                { "elevate", Elevate }
            };

            try
            {
                return routes[box.Verb](input, box, pieces);
            }
            catch (Exception e)
            {
                return new BoxResult(input);
            }
        }

        public static BoxResult Template(Curve input, BoxInput box, List<Curve> pieces)
        {
            var res = new BoxResult(pieces[0]);

            var outputs = new List<Curve>();

            //Solving logic.

            res.InternalVerb = outputs;
            res.Verb = "template";

            return res;
        }

        //Verb methods

        public static BoxResult Regulate(Curve input, BoxInput box, List<Curve> pieces)
        {
            var res = new BoxResult(pieces[0]);

            var outputs = new List<Curve>();

            var startPt = pieces[1].PointAtStart;
            var endPt = pieces[1].PointAtEnd;

            outputs.Add(new LineCurve(startPt, new Point3d(box.Dims.MaxCorner.X, startPt.Y, 0)));
            outputs.Add(new LineCurve(startPt, new Point3d(startPt.X, box.Dims.MaxCorner.Y, 0)));
            outputs.Add(new LineCurve(endPt, new Point3d(box.Dims.MinCorner.X, endPt.Y, 0)));
            outputs.Add(new LineCurve(endPt, new Point3d(endPt.X, box.Dims.MinCorner.Y, 0)));

            res.InternalVerb = outputs;
            res.Verb = "regulate";

            return res;
        }

        public static BoxResult Capture(Curve input, BoxInput box, List<Curve> pieces)
        {
            var res = new BoxResult(pieces[0]);

            return res;
        }

        public static BoxResult Thicken(Curve input, BoxInput box, List<Curve> pieces)
        {
            var res = new BoxResult(pieces[0]);

            var outputs = new List<Curve>();

            var startPt = pieces[1].PointAtStart;
            var endPt = pieces[1].PointAtEnd;
            var length = input.GetLength() / 6;

            outputs.Add(new LineCurve(new Point3d(startPt.X + length, startPt.Y + length, 0), new Point3d(endPt.Y + length, endPt.Y + length, 0)));
            outputs.Add(new LineCurve(new Point3d(startPt.X - length, startPt.Y - length, 0), new Point3d(endPt.Y - length, endPt.Y - length, 0)));

            res.InternalVerb = outputs;
            res.Verb = "thicken";

            return res;
        }

        public static BoxResult Elevate(Curve input, BoxInput box, List<Curve> pieces)
        {
            var res = new BoxResult(pieces[0]);

            var outputs = new List<Curve>();

            pieces[0].DivideByCount(5, true).ToList().ForEach(
                x => outputs.Add(new LineCurve(pieces[1].PointAt(x), new Point3d(pieces[1].PointAt(x).X, input.PointAtStart.Y, 0))));

            res.InternalVerb = outputs;
            res.Verb = "elevate";

            return res;
        }
    }
}
