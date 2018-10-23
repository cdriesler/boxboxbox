using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Theano.Rep.Formats;

namespace Theano.Rep.Logic
{
    public static class Sanitize
    {
        public static List<Armature> InterfaceOutput(List<string> data)
        {
            var output = data.Skip(data.IndexOf(data.Find(x => x.Contains("START"))) + 1).Take(4).ToList();

            //output.ForEach(x => RhinoApp.WriteLine(x));

            double angle = Convert.ToDouble(output[0]);

            output.RemoveAt(0);

            var containers = new List<Armature>();

            for (int i = 0; i < 3; i ++)
            {
                var vals = output[i].Split(',').ToList().Select(x => Convert.ToInt32(x)).ToList();

                if (vals[0] == -1 && vals[1] == -1)
                {
                    continue;
                }

                var anchor = ConvertToModelCoordinates(vals.Max());

                RhinoApp.WriteLine($"{output[i]} => {anchor.ToString()}");

                var baseRot = 0;

                var solo = vals.Any(x => x < 0);

                if (solo)
                {
                    baseRot = 0;

                    if (anchor < 9)
                    {
                        anchor = anchor - 4;
                    }

                    containers.Add(new Armature(anchor, (CapType)(i+1), baseRot, angle));
                    continue;
                }
                else
                {
                    //Check baserot in relation to other val
                    var modelCoord = vals.Max();
                    var otherCoord = vals.Min();

                    if ((modelCoord + otherCoord) % 2 == 0)
                    {
                        containers.Add(new Armature(anchor, (CapType)(i+1), 0, angle));
                        continue;
                    }
                    else
                    {
                        var toRight = otherCoord > (modelCoord - 4);

                        baseRot = toRight ? -1 : 1;

                        containers.Add(new Armature(anchor, (CapType)(i+1), baseRot, angle));
                    }
                }
            }

            return containers;
        }

        public static int ConvertToModelCoordinates(int interfaceCoord)
        {
            var relation = new Dictionary<int, int>()
            {
                {0 , 9},
                {1, 10},
                {2, 11},
                {3, 12},
                {4, 5},
                {5, 6},
                {6, 7},
                {7, 8},
                {-1, -1}
            };

            return relation[interfaceCoord];
        }
    }
}
