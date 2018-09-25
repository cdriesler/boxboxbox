using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace Cicero.Core.Formats
{
    public class BoxResult
    {
        public string Verb;
        public string Adverb;

        public List<Curve> Original;
        public List<Curve> External;
        public List<Curve> InternalVerb;
        public List<Curve> InternalAdverb;

        public BoxResult(List<Curve> original, List<Curve> external, List<Curve> internalVerb, List<Curve> internalAdverb)
        {
            Original = original;
            External = external;
            InternalVerb = internalVerb;
            InternalAdverb = internalAdverb;
        }
    }
}
