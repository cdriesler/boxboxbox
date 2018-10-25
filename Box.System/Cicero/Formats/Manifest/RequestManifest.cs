using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.System.Cicero.Formats.Element;
using Rhino.Geometry;

namespace Box.System.Cicero.Formats.Manifest
{
    public class RequestManifest
    {
        public List<Curve> Lines;
        public List<BoxElement> Boxes;
    }
}
