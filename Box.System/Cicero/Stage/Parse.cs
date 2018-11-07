using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.Core.Formats.Request;
using Box.System.Cicero.Formats.Element;
using Box.System.Cicero.Formats.Manifest;
using Rhino.Geometry;

namespace Box.System.Cicero.Stage
{
    public static class Parse
    {
        public static SolutionManifest Request(CiceroRequest req)
        {
            //payload = "#input_line:((6,1),(0,4))#input_line:((4,6),(6,1))#input_line:((1,6),(3,0))#box_paired/thicken:((4,4),(5,1))#box_decorated/regulate:((2,3),(4,5))#box_lazy/elevate:((1,4),(5,3))";
            var payload = req.Payload;
            var bounds = req.InnerBounds;

            var data = payload.Split('#').ToList();

            var inputs = data.Where(x => x.Split('_')[0] == "input").ToList();
            var boxes = data.Where(x => x.Split('_')[0] == "box").ToList();

            var inputCurves = new List<Curve>();
            var boxCurves = new List<BoxElement>();

            foreach (string crvData in inputs)
            {
                inputCurves.Add(LineFromString(crvData, bounds));
            }

            foreach (string boxData in boxes)
            {
                boxCurves.Add(BoxFromString(boxData, bounds));
            }

            return new SolutionManifest(inputCurves, boxCurves, bounds);
        }

        public static Curve LineFromString(string data, Curve bounds)
        {
            var dims = bounds.GetBoundingBox(Plane.WorldXY);
            var maxCorner = dims.Max;
            var minCorner = dims.Min;
            var range = maxCorner.X - minCorner.X;
            var rangeInc = range / 6;

            var mid = (maxCorner + minCorner) / 2;
            var reflectionPlane = Plane.WorldZX;
            reflectionPlane.Origin = mid;

            var xform = Transform.Mirror(reflectionPlane);

            var coords = data.Split(':')[1].Replace("(", "").Replace(")", "").Split(',');

            var startPt = new Point3d(Convert.ToDouble(coords[0]) * rangeInc, Convert.ToDouble(coords[1]) * rangeInc, 0);
            var endPt = new Point3d(Convert.ToDouble(coords[2]) * rangeInc, Convert.ToDouble(coords[3]) * rangeInc, 0);

            startPt = startPt + minCorner;
            endPt = endPt + minCorner;

            var output = new LineCurve(startPt, endPt);
            output.Transform(xform);

            return output;
        }

        public static BoxElement BoxFromString(string data, Curve bounds)
        {
            var dims = bounds.GetBoundingBox(Plane.WorldXY);
            var maxCorner = dims.Max;
            var minCorner = dims.Min;
            var range = maxCorner.X - minCorner.X;
            var rangeInc = range / 6;

            var mid = (maxCorner + minCorner) / 2;
            var reflectionPlane = Plane.WorldZX;
            reflectionPlane.Origin = mid;

            var xform = Transform.Mirror(reflectionPlane);

            var splitData = data.Split(':');

            var lang = splitData[0].Split('_')[1].Split('/');

            var adverb = lang[0];
            var verb = lang[1];

            var coords = splitData[1].Replace("(", "").Replace(")", "").Split(',');

            var startPt = new Point3d(Convert.ToDouble(coords[0]) * rangeInc, Convert.ToDouble(coords[1]) * rangeInc, 0);
            var endPt = new Point3d(Convert.ToDouble(coords[2]) * rangeInc, Convert.ToDouble(coords[3]) * rangeInc, 0);

            startPt = startPt + minCorner;
            endPt = endPt + minCorner;

            var boxGeo = new Rectangle3d(Plane.WorldXY, startPt, endPt).ToNurbsCurve();
            boxGeo.Transform(xform);

            return new BoxElement(boxGeo, verb, adverb);
        }
    }
}
