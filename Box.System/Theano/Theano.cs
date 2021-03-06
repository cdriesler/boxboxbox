﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Box.System.Theano.Formats;
using Box.System.Theano.Logic;

namespace Box.System.Theano
{
    public class Inbound : GH_Component
    {
        public Inbound() : base("Theano", "Theano", "Draw based on information generated by THEANO interface.", "BOX", "Theano")
        {

        }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("d", "data", "data from interface", GH_ParamAccess.list);
            pManager.AddGeometryParameter("C", "Columns", "Columns", GH_ParamAccess.item);
            pManager.AddGeometryParameter("P", "Plinth", "Plinth", GH_ParamAccess.item);
            pManager.AddGeometryParameter("S", "Swing", "Swing", GH_ParamAccess.item);
            pManager.AddPointParameter("A", "Anchors", "Anchors", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            //.AddGeometryParameter("o", "output", "output geometry", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var data = new List<string>();
            DA.GetDataList(0, data);

            Brep col = null;
            Brep plinth = null;
            Brep swing = null;

            DA.GetData(1, ref col);
            DA.GetData(2, ref plinth);
            DA.GetData(3, ref swing);

            var pts = new List<Point3d>();
            DA.GetDataList(4, pts);

            var containers = Sanitize.InterfaceOutput(data);

            if (!containers.Any()) return;

            var terrain = Terrain.Generate(1.5, 2.5);

            foreach (Armature armature in containers)
            {
                armature.Build(terrain, col, plinth, swing, pts);

                Make.Armature(armature);
            }
        }

        protected override Bitmap Icon
        {
            get
            {
                //return Resources.Icon
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("4cacd8ab-74ff-49cd-ae57-ec89012ea8bd"); }
        }
    }
}
