using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.Core.Aviary;
using Box.Core.Formats.Style;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Geometry;

namespace Box.Core.Test.Aviary
{
    public class TestGeometry
    {
        public static SvgDocument DiagonalLine()
        {
            var env = new SvgDocument(new Rectangle3d(Plane.WorldXY, new Point3d(0,0,0), new Point3d(1,1,0) ));

            env.Curves.Add(new LineCurve(new Point3d(0,0,0), new Point3d(1,1,0)));

            var style = new Style();

            style.Fill = "none";
            style.Stroke = "black";
            style.StrokeWidth = "0.1";

            env.Styles.Add(style);

            return env;
        }

        public static SvgDocument PolylineV()
        {
            var env = new SvgDocument(new Rectangle3d(Plane.WorldXY, new Point3d(0, 0, 0), new Point3d(1, 1, 0)));

            var crv = new PolylineCurve(new List<Point3d>()
            {
                new Point3d(0,1,0),
                new Point3d(0.5, 0, 0),
                new Point3d(1, 1, 0)
            });

            env.Curves.Add(crv);

            var style = new Style
            {
                Fill = "none",
                Stroke = "red",
                StrokeWidth = "0.1"
            };

            env.Styles.Add(style);

            return env;
        }
    }

    [TestClass]
    public class SvgBuildTests
    {
        [TestMethod]
        public void DiagonalLine_ViewData()
        {
            var env = TestGeometry.DiagonalLine();

            env.Build();

            Console.Write(env.SvgText);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Polyline_ViewData()
        {
            var env = TestGeometry.PolylineV();

            env.Build();

            Console.Write(env.SvgText);

            Assert.IsTrue(true);
        }

    }
}
