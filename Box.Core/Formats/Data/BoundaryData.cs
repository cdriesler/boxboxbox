using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace Box.Core.Formats.Data
{
    public class BoundaryData
    {
        public Point3d MaxCorner;
        public Point3d MinCorner;
        public Point3d Center;
        public double Width;

        public BoundaryData(Curve rect)
        {
            var bounds = rect.GetBoundingBox(Plane.WorldXY);

            MaxCorner = bounds.Max;
            MinCorner = bounds.Min;
            Center = new Point3d((MaxCorner.X + MinCorner.X) / 2, (MaxCorner.Y + MinCorner.Y) / 2, 0);

            Width = MaxCorner.X - MinCorner.X;
        }
    }
}
