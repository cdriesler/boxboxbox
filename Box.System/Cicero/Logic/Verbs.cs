using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Box.System.Cicero.Formats.Element;
using Rhino.Geometry;

namespace Box.System.Cicero.Logic
{
    public static class Verbs
    {
        static readonly Dictionary<string, Func<BoxElement, bool>> VerbMethods = new Dictionary<string, Func<BoxElement, bool>>()
        {
            { "regulate", Regulate },
        };

        public static void Enact(BoxElement box)
        {
            box.VerbResults = new List<Curve>();

            if (!VerbMethods.TryGetValue(box.Verb, out Func<BoxElement, bool> verbFunc))
            {
                //Verb method is not implemented. Pass input curves as results.
                box.VerbResults.AddRange(box.InputSegments);
            }
            else
            {
                verbFunc(box);
            }
        }

        private static bool Regulate(BoxElement box)
        {
            return true;
        }
    }
}
