using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Core.Formats
{
    public class Styles
    {
        public List<string> StrokeWeight;
        public List<string> StrokeColor;
        public List<string> FillColor;

        public Styles()
        {
            StrokeWeight = new List<string>();
            StrokeColor = new List<string>();
            FillColor = new List<string>();
        }
    }
}
