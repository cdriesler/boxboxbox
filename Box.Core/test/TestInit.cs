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

            Rhino.Compute.ComputeServer.AuthToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJwIjoiUEtDUyM3IiwiYyI6IkFFU18yNTZfQ0JDIiwiYjY0aXYiOiJiRkUrYVFRMnRXeVcvMmQwS2kraXdBPT0iLCJiNjRjdCI6InJkaEdCT3Q2Ry9leE5PQVh1anFndENQUTFFN1ZUbWZrOENlenBSaDNLaVFnaVV0ejFpLzhZRWEyWWt5WGFaVU1xU0RLVDhUQjRsQXZUNTNZRXZCTTZGYUhIcW50YUUveERUWDJmTmFPSFh2c3VSeERMTEtlVEgxRnArYk5aMSs0d2VvT2xPZXh4TkFXZkVSd2R3Zm9BdW9hQlp3cjlITG41Zno0aGRCTGY1L1J2UUxBTjVCRnZJQVpsUVc0VG5YU3p6akJtanB4cVVWN2ZWemtrYnNJVEE9PSIsImlhdCI6MTU0MTQ4NzEwOX0.N6ufPFSLuxiTnUsMiFrTd58Qs2KpVG6PmwjL-LksWEw";
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            // Shutdown the rhino process at the end of the test run
            //ExitInProcess();
        }
    }
}
