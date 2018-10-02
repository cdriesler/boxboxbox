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
            pManager.AddGenericParameter("Style Pattern", "S", "Pattern of styles to correlate with curves.", GH_ParamAccess.item);

            pManager[1].Optional = true;
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {

        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string path = null;
            DA.GetData(0, ref path);

            Styles style = null;
            DA.GetData(1, ref style);

            var existingData = System.IO.File.ReadAllLines(path).ToList();

            var nameData = path.Split('\\');
            var name = nameData[nameData.Length - 1];

            var targetDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cicero\\staging\\" + name;

            //Style svg.
            var strokeWidthIt = 0;
            var strokeColorIt = 0;
            var fillColorIt = 0;

            foreach (String line in existingData)
            {
                var data = line;

                if (line.Contains("width") && line.Contains("1000") && line.Contains("500"))
                {
                    data = line.Replace("1000", "132vh").Replace("500", "66vh");

                    System.IO.File.AppendAllText(targetDir, data + Environment.NewLine);
                    continue;
                }

                if (line.Contains("width") && line.Contains("1000"))
                {
                    data = line.Replace("1000", "66vh");

                    System.IO.File.AppendAllText(targetDir, data + Environment.NewLine);
                    continue;
                }

                if (line.Contains("stroke") && !line.Contains("width"))
                {
                    if (strokeColorIt >= style.StrokeColor.Count) strokeColorIt = style.StrokeColor.Count - 1;

                    var newVal = $"stroke={style.StrokeColor[strokeColorIt]}";

                    System.IO.File.AppendAllText(targetDir, newVal + Environment.NewLine);

                    strokeColorIt++;
                    continue;
                }

                if (line.Contains("stroke-width"))
                {
                    if (strokeWidthIt >= style.StrokeWeight.Count) strokeWidthIt = style.StrokeWeight.Count - 1;

                    var newVal = $"stroke-width={style.StrokeWeight[strokeWidthIt]}";

                    System.IO.File.AppendAllText(targetDir, newVal + Environment.NewLine);

                    strokeWidthIt++;
                    continue;
                }

                if (line.Contains("fill"))
                {
                    if (fillColorIt >= style.FillColor.Count) fillColorIt = style.FillColor.Count - 1;

                    var newVal = $"stroke-width={style.FillColor[fillColorIt]}";

                    System.IO.File.AppendAllText(targetDir, newVal + Environment.NewLine);

                    fillColorIt++;
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