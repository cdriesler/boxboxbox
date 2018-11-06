using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.Core.Geometry;
using Box.Core.Geometry.Modify;
using Box.System.Cicero.Formats.Element;
using Box.System.Cicero.Logic;
using Rhino.Geometry;

namespace Box.System.Cicero.Formats.Manifest
{
    public class SolutionManifest
    {
        public Curve Bounds;

        public List<Curve> InputLines; //Initial input curves
        public List<BoxElement> BoxElements; //BoxElements with solution information.

        public SolutionManifest(List<Curve> lines, List<BoxElement> boxes, Curve bounds)
        {
            Bounds = bounds;

            InputLines = lines;
            BoxElements = boxes;
        }

        /// <summary>
        /// Capture segments of input lines that pass through box bounds.
        /// </summary>
        public void RunContainment()
        {
            foreach (BoxElement box in BoxElements)
            {
                foreach (Curve input in InputLines)
                {
                    if (Core.Geometry.Verify.Curves.CurvesIntersect(input, box.Bounds))
                    {
                        box.InputSegments.Add(Curves.TrimLineWithRegion(box.Bounds, input));
                    }
                }          
            }
        }

        /// <summary>
        /// Perform verb operations on captured segments.
        /// </summary>
        public void RunVerbs()
        {
            foreach (BoxElement box in BoxElements)
            {
                Verbs.Enact(box);
            }

        }

        public void RunAdverbs()
        {

        }
    }
}
