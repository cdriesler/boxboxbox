using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.Core.Formats.Drawing;
using Box.Core.Formats.Request;
using Box.System.Cicero.Formats.Manifest;
using Box.System.Cicero.Stage;
using Rhino.Geometry;

namespace Box.System.Cicero
{
    public static class Cicero
    {
        public static Drawing Run(CiceroRequest req)
        {
            SolutionManifest res = Parse.Request(req);

            res.RunContainment();
            res.RunVerbs();
            res.RunAdverbs();

            Drawing dwg = Compose.Drawing(res);

            return dwg;
        }
    }
}
