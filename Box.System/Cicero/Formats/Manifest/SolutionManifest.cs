using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.System.Cicero.Formats.Element;
using Rhino.Geometry;

namespace Box.System.Cicero.Formats.Manifest
{
    public class SolutionManifest
    {
        public List<Curve> InputLines; //Initial input curves
        public List<BoxElement> BoxElements; //BoxElements with solution information.

        public SolutionManifest(List<Curve> lines, List<BoxElement> boxes)
        {
            InputLines = lines;
            BoxElements = boxes;
        }

        /// <summary>
        /// Capture segments of input lines that pass through box bounds.
        /// </summary>
        public void RunContainment()
        {
            
        }

        /// <summary>
        /// Perform verb operations on captured segments.
        /// </summary>
        public void RunVerbs()
        {

        }

        public void RunAdverbs()
        {

        }
    }
}
