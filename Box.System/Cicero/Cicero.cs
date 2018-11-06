using System;
using System.Drawing;
using io = System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.Core.Formats.Drawing;
using Box.Core.Formats.Request;
using Box.System.Cicero.Formats.Manifest;
using Box.System.Cicero.Stage;
using Rhino.Geometry;
using Grasshopper;
using Grasshopper.Kernel;

namespace Box.System.Cicero
{
    public class Cicero : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the BuildThreeJs class.
        /// </summary>
        public Cicero() : base("CICERO", "CICERO", "CICERO", "Box", "SYS")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Id", "id", "Id", GH_ParamAccess.item);
            pManager.AddTextParameter("Data", "d", "Data", GH_ParamAccess.item);
            pManager.AddCurveParameter("Inner Bounds", "Bi", "Inner Bounds", GH_ParamAccess.item);
            pManager.AddCurveParameter("Outer Bounds", "Bo", "Outer Bounds", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var id = "";
            var data = "";
            Curve innerBounds = null;
            Curve outerBounds = null;

            DA.GetData(0, ref id);
            DA.GetData(1, ref data);
            DA.GetData(2, ref innerBounds);
            DA.GetData(3, ref outerBounds);

            var req = new CiceroRequest(data, innerBounds, outerBounds);

            SolutionManifest res = Parse.Request(req);

            res.RunContainment();
            res.RunVerbs();
            res.RunAdverbs();

            var dwg = Compose.Drawing(res);

            dwg.Build();

            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cicero\\svg\\" + id + ".svg";

            io.File.WriteAllText(path, dwg.SvgText.ToString());
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("0840df80-a6a2-4f3c-a8ba-baaff0972d10"); }
        }
    }
}
