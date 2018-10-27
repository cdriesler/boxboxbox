using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace Box.Core.Formats.Drawing
{
    public class Drawing
    {
        public List<DrawingLayer> Layers;
        public List<string> LayerOrder;
        public Dictionary<string, List<Curve>> LayerGeometry; //Layer name, geometry

        public Curve Bounds;
        public Dictionary<string, string> LayerSvg;

        public Drawing()
        {

        }

        public void BuildSvg()
        {

        }
    }
}
