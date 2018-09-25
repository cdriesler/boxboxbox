using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cicero.Core.Formats;
using Rhino.Geometry;

namespace Cicero.Core.Logic.Solve
{
    public static class Solve
    {
        public static List<BoxResult> Request(Request req, Curve bounds)
        {
            var results = new List<BoxResult>();

            foreach (Curve inputCrv in req.Inputs)
            {
                var intersectsAny = false;

                foreach (BoxInput box in req.Boxes)
                {
                    if (Check.IfCurvesIntersect(inputCrv, box.Bounds))
                    {
                        intersectsAny = true;

                        results.Add(Verbs.Enact(inputCrv, box));

                        //results.Add(Adverbs.Enact(verbRes));
                    }
                }

                if (!intersectsAny)
                {
                    results.Add(new BoxResult(new List<Curve>{inputCrv}, new List<Curve>(), new List<Curve>(), new List<Curve>()));
                }
            }

            return results;
        }
    }
}
