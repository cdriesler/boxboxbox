using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.Core.Formats.Data;
using Rhino.Geometry;

namespace Box.System.Cicero.Formats.Element
{
    public class BoxElement
    {
        public Curve Bounds;
        public string Verb;
        public string Adverb;

        public BoundaryData Dims;

        public BoxElement(Curve bounds, string verb, string adverb)
        {
            Bounds = bounds;
            Verb = verb;
            Adverb = adverb;

            Dims = new BoundaryData(bounds);
        }
    }
}
