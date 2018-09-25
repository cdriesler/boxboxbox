using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cicero.Core.Formats;
using Cicero.Core.Logic.Parse;
using Cicero.Core.Logic.Solve;
using Rhino;
using Rhino.Geometry;
using Grasshopper.Kernel;

namespace Cicero.Core.Components
{
    public class Styler : GH_Component
    {
        public Styler() : base("Styler", "Styler", "Black Box styler.", "Cicero", "Main")
        {

        }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Path", "P", "File path of svg to style.", GH_ParamAccess.item);
            pManager.AddTextParameter("Style Pattern", "S", "Pattern of styles to correlate with curves.", GH_ParamAccess.list);

            pManager[1].Optional = true;
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {

        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string path = null;
            DA.GetData(0, ref path);

            //List<string> pattern = null;
            //DA.GetDataList(1, pattern);

            var existingData = System.IO.File.ReadAllLines(path).ToList();

            var nameData = path.Split('\\');
            var name = nameData[nameData.Length - 1];

            var targetDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cicero\\staging\\" + name;

            //Style svg.
            foreach (String line in existingData)
            {
                var data = line;

                if (line.Contains("width") && line.Contains("1000"))
                {
                    data = line.Replace("1000", "66vh");

                    System.IO.File.AppendAllText(targetDir, data + Environment.NewLine);
                    continue;
                }

                if (line.Contains("stroke-width"))
                {
                    data = line.Replace("1", "0.01");

                    System.IO.File.AppendAllText(targetDir, data + Environment.NewLine);
                    continue;
                }

                if (line.Contains("fill"))
                {
                    data = line.Replace("rgb(0,0,0)", "none");
                    System.IO.File.AppendAllText(targetDir, data + Environment.NewLine);
                    continue;
                }

                System.IO.File.AppendAllText(targetDir, data + Environment.NewLine);
            }

            //Copy to /svg
            var finalData = System.IO.File.ReadAllLines(targetDir);
            System.IO.File.WriteAllLines(targetDir.Replace("staging", "svg"), finalData);

            System.IO.File.Delete(path);
            System.IO.File.Delete(targetDir);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("a5c89219-d429-41dc-831a-bf8ade70d400"); }
        }
    }
}