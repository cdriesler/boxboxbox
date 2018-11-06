using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.Core.Formats.Style;

namespace Box.System.Cicero.Logic
{
    public static class Styles
    {
        static readonly Dictionary<string, Func<Style>> StyleDictionary = new Dictionary<string, Func<Style>>()
        {
            { "default", Default },
            { "regulate", Regulate },
        };

        public static Style Get(string styleName)
        {
            if (!StyleDictionary.TryGetValue(styleName, out Func<Style> styleFunc))
            {
                //Style method is not implemented. Return default style.
                return StyleDictionary["default"]();
            }
            else
            {
                return styleFunc();
            }
        }

        public static Style Default()
        {
            var style = new Style()
            {
                Fill = "none",
                Stroke = "black",
                StrokeWidth = "0.1"
            };

            return style;
        }

        public static Style Regulate()
        {
            var style = new Style()
            {
                Fill = "none",
                Stroke = "black",
                StrokeWidth = "0.17"
            };

            return style;
        }
    }
}
