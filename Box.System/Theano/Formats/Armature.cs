using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.Geometry;

namespace Box.System.Theano.Formats
{
    public enum CapType { Bottom, Middle, Top };

    public class Armature
    {
        //Data
        public int Anchor;
        public CapType CapType;
        public double BottomRotation;
        public double TopRotation;

        //Geometry
        public Brep Column;
        public Brep Plinth;
        public Brep Swing;

        public Armature(int anchor, CapType cap, double botRot, double topRot)
        {
            Anchor = anchor;
            CapType = cap;
            BottomRotation = botRot;
            TopRotation = topRot;
        }

        /// <summary>
        /// Generate and place geometry for armatures.
        /// </summary>
        public void Build(List<Point3d> terrain, Brep col, Brep plinth, Brep swing, List<Point3d> pts)
        {
            var rotate = Transform.Rotation(BottomRotation * .255, Point3d.Origin);
            var move = Transform.Translation(new Vector3d(terrain[Anchor]));

            var newCol = col.Duplicate();
            var newPlinth = plinth.Duplicate();
            var newSwing = swing.Duplicate();

            for (int i = 0; i < pts.Count; i++)
            {
                pts[i] = new Point3d(pts[i].X, pts[i].Y, pts[i].Z);
            }

            newCol.Transform(rotate);
            newCol.Transform(move);

            newPlinth.Transform(rotate);
            newPlinth.Transform(move);

            newSwing.Transform(rotate);
            newSwing.Transform(move);

            foreach (Point3d pt in pts)
            {
                pt.Transform(rotate);
                pt.Transform(move);
            }

            var bounds = newSwing.GetBoundingBox(false);
            var line = new LineCurve(bounds.Min, bounds.Max);
            line.Reverse();

            var swingRot = Transform.Rotation(TopRotation * (Math.PI / 180), line.PointAtNormalizedLength((int)CapType * .25));
            newSwing.Transform(swingRot);

            Column = newCol as Brep;
            Plinth = newPlinth as Brep;
            Swing = newSwing as Brep;
        }
    }
}
