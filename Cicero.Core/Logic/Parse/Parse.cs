﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Cicero.Core.Formats;
using Rhino.Geometry;

namespace Cicero.Core.Logic.Parse
{
    public static class Parse
    {
        public static Request RequestFromPayload(string payload, Curve bounds)
        {
            //payload = "#input_line:((6,1),(0,4))#input_line:((4,6),(6,1))#input_line:((1,6),(3,0))#box_paired/thicken:((4,4),(5,1))#box_decorated/regulate:((2,3),(4,5))#box_lazy/elevate:((1,4),(5,3))";

            var data = payload.Split('#').ToList();

            var inputs = data.Where(x => x.Split('_')[0] == "input").ToList();
            var boxes = data.Where(x => x.Split('_')[0] == "box").ToList();

            var inputCurves = new List<Curve>();
            var boxCurves = new List<BoxInput>();

            foreach (string crvData in inputs)
            {
                inputCurves.Add(LineFromString(crvData, bounds));
            }

            foreach (string boxData in boxes)
            {
                boxCurves.Add(BoxFromString(boxData, bounds));
            }

            return new Request(inputCurves, boxCurves);
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

        public static BoxInput BoxFromString(string data, Curve bounds)
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

            return new BoxInput(boxGeo, verb, adverb);
        }

        public static Solution SolutionFromResults(List<BoxResult> res)
        {
            var solution = new Solution();
            var style = new Styles();

            foreach (BoxResult result in res)
            {
                if (result.Verb != "error")
                {
                    solution.OutputCurves.Add(result.Original[0]);
                    style.FillColor.Add("none");
                    style.StrokeWeight.Add("0.06");
                    style.StrokeColor.Add("black");
                }
                else
                {
                    solution.OutputCurves.Add(result.Original[0]);

                    Style.Style.ApplyFromDictionary(result, style);
                }


                foreach (Curve crv in result.InternalVerb)
                {
                    solution.OutputCurves.Add(crv);

                    Style.Style.ApplyFromDictionary(result, style);
                }
            }

            solution.OutputStyles = style;

            return solution;
        }
    }
}
