using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace Theano.Dispatch
{
    class Program
    {
        static SerialPort _serialPort;
        public static void Main()
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = "COM6";//Set your board COM
            _serialPort.BaudRate = 9600;
            _serialPort.Open();

            var record = false;

            while (true)
            {
                string a = _serialPort.ReadExisting();

                var recordData = new List<string>();

                if (a.Contains("START")) record = true;

                Console.WriteLine(a);

                if (record)
                {
                    recordData.Add(a);

                    if (a.Contains("END"))
                    {
                        //Write to file.

                        Console.WriteLine("Dispatching data:");
                        recordData.ForEach(x => Console.WriteLine(x));

                        WriteData(recordData);

                        recordData.Clear();
                        record = false;

                        Console.WriteLine("----");
                    }
                }

                Thread.Sleep(200);
            }
        }

        private static void WriteData(List<string> data)
        {
            File.WriteAllLines(@"C:\Users\cdrie\AppData\Roaming\boxboxbox\Theano\session.txt", data);
        }
    }
}
