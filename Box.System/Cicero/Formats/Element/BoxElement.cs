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

        public List<Curve> InputSegments; //Segment of input line captured by box.
        public List<Curve> VerbResults; //Result of verb operation on InputContent.
        public List<Curve> AdverbResults; //Result of adverb operation in AdverbContent.

        public List<Curve> ErrorCurves;

        public BoxElement(Curve bounds, string verb, string adverb)
        {
            Bounds = bounds;
            Verb = verb;
            Adverb = adverb;

            Dims = new BoundaryData(bounds);

            InputSegments = new List<Curve>();
            VerbResults = new List<Curve>();

            ErrorCurves = new List<Curve>();
        }
    }
}
