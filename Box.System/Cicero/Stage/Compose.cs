using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.Core.Aviary;
using Box.Core.Formats.Drawing;
using Box.System.Cicero.Formats.Element;
using Box.System.Cicero.Formats.Manifest;
using Box.System.Cicero.Logic;
using Rhino.Geometry;

namespace Box.System.Cicero.Stage
{
    public static class Compose
    {
        public static SvgDocument Drawing(SolutionManifest res)
        {
            var bounds = res.Bounds.GetBoundingBox(false);

            var svgData = new SvgDocument(new Rectangle3d(Plane.WorldXY, bounds.Min, bounds.Max));

            foreach (BoxElement box in res.BoxElements)
            {
                foreach (Curve crv in box.VerbResults)
                {
                    svgData.Curves.Add(crv);
                    svgData.Styles.Add(Styles.Get(box.Verb));
                }
            }

            return svgData;
        }
    }
}
