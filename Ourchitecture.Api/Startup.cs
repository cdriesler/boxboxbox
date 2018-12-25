﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.AspNetCore.Builder;
using Nancy.Owin;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Ourchitecture.Api
{
    public class Startup
    {
        static string systemDir = null;

        [AssemblyInitialize]
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy());

            Console.WriteLine("Initializing!");

            // Set path to rhino system directory
            string envPath = Environment.GetEnvironmentVariable("path");
            string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            systemDir = System.IO.Path.Combine(programFiles, "Rhino WIP", "System");

            // Add rhino system directory to path (for RhinoLibrary.dll)
            Environment.SetEnvironmentVariable("path", envPath + ";" + systemDir);

            // Add hook for .Net assmbly resolve (for RhinoCommmon.dll)
            AppDomain.CurrentDomain.AssemblyResolve += ResolveRhinoCommon;

            // Start headless Rhino process
            LaunchInProcess(0, 0);
        }

        private static Assembly ResolveRhinoCommon(object sender, ResolveEventArgs args)
        {
            var name = args.Name;

            if (!name.StartsWith("RhinoCommon"))
            {
                return null;
            }

            var path = System.IO.Path.Combine(systemDir, "RhinoCommon.dll");
            return Assembly.LoadFrom(path);
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            // Shotdown the rhino process at the end of the test run
            ExitInProcess();
        }

        [DllImport("RhinoLibrary.dll")]
        internal static extern int LaunchInProcess(int reserved1, int reserved2);

        [DllImport("RhinoLibrary.dll")]
        internal static extern int ExitInProcess();
    }
}