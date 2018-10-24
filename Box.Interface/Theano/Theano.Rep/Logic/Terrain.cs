using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace Theano.Rep.Logic
{
    public static class Terrain
    {

        public static List<Point3d> Generate(double xDim, double yDim)
        {
            var pts = new List<Point3d>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    pts.Add(new Point3d(j * xDim, i * yDim, 0));
                }
            }

            return pts;
        }
    }
}
