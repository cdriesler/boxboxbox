﻿using System;
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

                Console.WriteLine(data);

                var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cicero\\";
                System.IO.File.WriteAllText(path + "current.dat", data);

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

                End:
                Thread.Sleep(1000);
            }
        }
    }
}
