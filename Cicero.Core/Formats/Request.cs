using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace Cicero.Core.Formats
{
    public class Request
    {
        public List<Curve> Inputs;
        public List<BoxInput> Boxes;

        public Request(List<Curve> inputs, List<BoxInput> boxes)
        {
            Inputs = inputs;
            Boxes = boxes;
        }
    }
}
