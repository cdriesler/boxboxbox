using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace Box.Core.Formats.Request
{
    public class CiceroRequest
    {
        public string Payload;
        public Curve InnerBounds;
        public Curve OuterBounds;

        public CiceroRequest(string payload)
        {
            Payload = payload;

            InnerBounds = new Rectangle3d(Plane.WorldXY, new Interval(-10, 10), new Interval(-10, 10)).ToNurbsCurve();
            OuterBounds = new Rectangle3d(Plane.WorldXY, new Interval(-15, 15), new Interval(-15, 15)).ToNurbsCurve();
        }

        public CiceroRequest(string payload, Curve innerBounds, Curve outerBounds)
        {
            Payload = payload;
            InnerBounds = innerBounds;
            OuterBounds = outerBounds;
        }
    }
}
