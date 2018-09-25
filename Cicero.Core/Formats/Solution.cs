using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace Cicero.Core.Formats
{
    public class Solution
    {
        public List<Curve> OutputCurves;
        public List<string> OutputStyles;

        public Solution(List<Curve> crvs, List<string> styles)
        {
            OutputCurves = crvs;
            OutputStyles = styles;
        }
    }
}
