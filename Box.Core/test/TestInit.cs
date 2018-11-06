using s = System;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.Core.Test
{
    [TestClass]
    public static class TestInit
    {
        static bool initialized = false;
        static string systemDir = null;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            if (initialized)
            {
                throw new s.InvalidOperationException("AssemblyInitialize should only be called once");
            }
            initialized = true;

            context.WriteLine("Assembly init started");

            Rhino.Compute.ComputeServer.ApiToken = "cdriesler.dev@gmail.com";
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            // Shutdown the rhino process at the end of the test run
            //ExitInProcess();
        }
    }
}
