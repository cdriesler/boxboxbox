using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace Cicero.Core.Formats
{
    public class BoxInput
    {
        public Curve Bounds;
        public string Verb;
        public string Adverb;

        public BoxInput(Curve crv, string verb, string adverb)
        {
            Bounds = crv;
            Verb = verb;
            Adverb = adverb;
        }
    }
}
