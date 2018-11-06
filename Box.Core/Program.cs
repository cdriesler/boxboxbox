using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Rhino.Compute;
using Rhino.Geometry;

namespace Box.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            FirestoreDb db = FirestoreDb.Create("cicero-box");
            CollectionReference queue = db.Collection("queue");
            CollectionReference results = db.Collection("results");

            ComputeServer.AuthToken = "cdriesler.iv@gmail.com";

            while (true)
            {
                Console.WriteLine($"Loop at {DateTime.Now.ToString()}.");

                var docs = queue.GetSnapshotAsync();
                docs.Wait();

                var numDocs = docs.Result.Count;

                Console.WriteLine($"{numDocs.ToString()} documents in queue.");

                if (numDocs == 0)
                {
                    Thread.Sleep(3000);
                    continue;

                    /*
                    var circleA = new Circle(new Point3d(0, 1, 0), 1.5).ToNurbsCurve();
                    var circleB = new Circle(new Point3d(0, -1, 0), 1.75).ToNurbsCurve();

                    var joinedCircles = Rhino.Compute.CurveCompute.CreateBooleanUnion(new Curve[] {circleA, circleB});
                    Console.WriteLine(joinedCircles[0].GetLength().ToString());
                    */
                };

                var id = docs.Result[0].Id;
                var data = docs.Result[0].GetValue<string>("payload");

                string[] dataToWrite = { id, data };

                Console.WriteLine(data);

                var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cicero\\";
                System.IO.File.WriteAllLines(path + "current.dat", dataToWrite);

                var rhinoIsDone = false;
                var loopCount = 0;

                var res = queue.Document(id).DeleteAsync();
                res.Wait();

                while (!rhinoIsDone)
                {
                    if (System.IO.File.Exists(path + "svg\\" + id + ".svg"))
                    {
                        rhinoIsDone = true;
                    }

                    if (loopCount > 30)
                    {
                        break;
                    }

                    Console.WriteLine("Rhino is computing...");
                    loopCount++;
                    Thread.Sleep(1000);
                }

                //Dispatch svg to database.
                var svgDoc = results.Document(id);
                var svgData = loopCount > 30 ? "The system failed to generate a solution, please try again." : System.IO.File.ReadAllText(path + "svg\\" + id + ".svg");

                Dictionary<string, object> docData = new Dictionary<string, object>
                {
                    {"svg", svgData }
                };

                var x = svgDoc.SetAsync(docData);
                x.Wait();

                Console.WriteLine($"Dispatched results for {id} to firestore.");

                Thread.Sleep(3000);
            }
        }
    }
}
