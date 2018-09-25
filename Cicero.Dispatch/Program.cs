using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Cicero.Dispatch
{
    class Program
    {
        static void Main(string[] args)
        {
            FirestoreDb db = FirestoreDb.Create("cicero-box");
            CollectionReference queue = db.Collection("queue");
            CollectionReference results = db.Collection("results");

            while (true)
            {
                Console.WriteLine($"Loop at {DateTime.Now.ToString()}.");

                var docs = queue.GetSnapshotAsync();
                docs.Wait();

                var numDocs = docs.Result.Count;

                Console.WriteLine($"{numDocs.ToString()} documents in queue.");

                if (numDocs == 0) goto End;

                var id = docs.Result[0].Id;
                var data = docs.Result[0].GetValue<string>("payload");

                string[] dataToWrite = {id, data};

                Console.WriteLine(data);

                var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cicero\\";
                System.IO.File.WriteAllLines(path + "current.dat", dataToWrite);

                var rhinoIsDone = false;

                var res = queue.Document(id).DeleteAsync();
                res.Wait();

                while (!rhinoIsDone)
                {
                    if (System.IO.File.Exists(path + "svg\\" + id + ".svg"))
                    {
                        rhinoIsDone = true;
                    }

                    Console.WriteLine("Rhino is computing...");
                    Thread.Sleep(1000);
                }

                //Dispatch svg to database.
                var svgDoc = results.Document(id);
                var svgData = System.IO.File.ReadAllText(path + "svg\\" + id + ".svg");

                Dictionary<string, object> docData = new Dictionary<string, object>
                {
                    {"svg", svgData }
                };

                var x = svgDoc.SetAsync(docData);
                x.Wait();

                Console.WriteLine($"Dispatched results for {id} to firestore.");

                End:
                Thread.Sleep(1000);
            }
        }
    }
}
