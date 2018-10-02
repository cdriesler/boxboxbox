using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cicero.Core.Formats;
using Rhino;

namespace Cicero.Core.Logic.Style
{
    public static class Style
    {
        public static Styles ApplyFromDictionary(BoxResult res, Styles style)
        {
            if (res.Verb == "")
            {
                style.FillColor.Add("none");
                style.StrokeColor.Add("gainsboro");
                style.StrokeWeight.Add("0.03");
            }

            Dictionary<string, Func<Styles, Styles>> styleDict = new Dictionary<string, Func<Styles, Styles>>
            {
                { "default", Default },
                { "elevate", Medium },
                { "hide", Heavy_LightGrey },
                { "error", Error }
            };

            try
            {
                return styleDict[res.Verb](style);
            }
            catch (Exception e)
            {
                return styleDict["default"](style);
            }
        }

        public static Styles Default(Styles style)
        {
            style.StrokeColor.Add("black");
            style.FillColor.Add("none");
            style.StrokeWeight.Add("0.01");

            return style;
        }

        public static Styles Error(Styles style)
        {
            RhinoApp.WriteLine("Error line generated and styled.");

            style.StrokeColor.Add("darkred");
            style.FillColor.Add("none");
            style.StrokeWeight.Add("0.02");

            return style;
        }

        public static Styles Medium(Styles style)
        {
            style.StrokeColor.Add("grey");
            style.FillColor.Add("none");
            style.StrokeWeight.Add("0.02");

            return style;
        }

        public static Styles Heavy_LightGrey(Styles style)
        {
            style.StrokeColor.Add("gainsboro");
            style.FillColor.Add("none");
            style.StrokeWeight.Add("0.03");

            return style;
        }
    }
}
