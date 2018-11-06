using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.Core.Formats.Style;
using Rhino.Geometry;

namespace Box.Core.Aviary
{
    public class SvgDocument
    {
        public Rectangle3d Bounds;
        public List<Curve> Curves;
        public List<Style> Styles;

        public StringBuilder SvgText = new StringBuilder();

        public SvgDocument(Rectangle3d bounds)
        {
            Bounds = bounds;

            Curves = new List<Curve>();
            Styles = new List<Style>();
        }

        public void Build()
        {
            double X = Bounds.X.Min;
            double Y = Bounds.Y.Min;
            double W = Bounds.Width;
            double H = Bounds.Height;

            SvgText.Append("<svg ");
            SvgText.Append("width =\"" + "71vh" + "\" ");
            SvgText.Append("height =\"" + "71vh" + "\" ");
            SvgText.Append("viewBox=\"" + X + " " + Y + " " + W + " " + H + "\" ");
            //SvgText.Append("shape-rendering=\"" + RenderQuality + "\" ");
            SvgText.Append("xmlns = \"http://www.w3.org/2000/svg\" > " + Environment.NewLine);

            SvgText.Append("<g class=\"Canvas\" id=\"Canvas\" transform=\"translate(0," + Y + ") scale(1,-1) translate(0," + (-1) * (Y + H) + ")\">" + Environment.NewLine);

            /*
            SvgText.Append("<defs> " + Environment.NewLine);
            SvgText.Append("<clipPath id=\"Frame\">" + Environment.NewLine);
            SvgText.Append("<rect x=\"0\" y=\"0\" width=\"" + Width + "\" height=\"" + Height + "\" />" + Environment.NewLine);
            SvgText.Append("</clipPath>" + Environment.NewLine);
            SvgText.Append("</defs> " + Environment.NewLine);
            */

            for (int i = 0; i < Curves.Count; i++)
            {
                var crvText = CurveToSvg(Curves[i], Styles[i]);

                SvgText.Append(crvText);
            }

            SvgText.Append(" </g>" + Environment.NewLine);

            SvgText.Append(" </svg>");

        }

        private string CurveToSvg(Curve crv, Style style)
        {
            //No curved lines for midterm.
            if (crv.Degree > 1) return null;

            //Parse geometry
            var svgData = "<polyline points=\"";

            for (int i = 0; i < crv.SpanCount; i++)
            {
                var span = crv.SpanDomain(i);

                var pt = crv.PointAt(span.Min);

                svgData = svgData + $"{pt.X},{pt.Y} ";

                if (i == crv.SpanCount - 1)
                {
                    var endPt = crv.PointAt(span.Max);

                    svgData = svgData + $"{endPt.X},{endPt.Y}\"";
                }
            }

            //Parse style
            svgData = svgData + "style=\"";

            svgData = svgData + $"fill:{style.Fill};";
            svgData = svgData + $"stroke:{style.Stroke};";
            svgData = svgData + $"stroke-width:{style.StrokeWidth}";

            svgData = svgData + "\"/>";

            return svgData;
        }
    }
}
