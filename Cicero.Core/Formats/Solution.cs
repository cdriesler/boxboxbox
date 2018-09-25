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
        public Styles OutputStyles;

        public Solution()
        {
            OutputCurves = new List<Curve>();
        }

        public Solution(List<Curve> crvs, Styles styles)
        {
            OutputCurves = crvs;
            OutputStyles = styles;
        }
    }
}
