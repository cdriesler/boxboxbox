using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace Box.Core.Transformations
{
    public static class Curves
    {
        /// <summary>
        /// Shatter a curve at its intersections with a region. Return segments either inside or outside the region.
        /// </summary>
        /// <param name="crv">Curve to trim.</param>
        /// <param name="region">Region to trim with.</param>
        /// <param name="inside">Return segments inside the region if true, outside if false.</param>
        /// <returns></returns>
        public static List<Curve> TrimCurveWithRegion(Curve crv, Curve region, bool inside = true)
        {
            return null;
        }
    }
}
