using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.DocObjects;
using Rhino.Geometry;
using Box.System.Theano.Formats;

namespace Box.System.Theano.Logic
{
    public static class Make
    {
        /// <summary>
        /// Place geometry in appropriate layers for styling.
        /// </summary>
        /// <param name="armature"></param>
        public static void Armature(Armature armature)
        {
            var doc = RhinoDoc.ActiveDoc;

            var colAtts = new ObjectAttributes
            {
                LayerIndex = doc.Layers.FindName("Column").Index,
            };
            doc.Objects.AddBrep(armature.Column, colAtts);

            var plinthAtts = new ObjectAttributes
            {
                LayerIndex = doc.Layers.FindName("Plinth").Index,
            };
            doc.Objects.AddBrep(armature.Plinth, plinthAtts);

            var swingAtts = new ObjectAttributes
            {
                LayerIndex = doc.Layers.FindName("Swing").Index,
            };
            doc.Objects.AddBrep(armature.Swing, swingAtts);
        }
    }
}
