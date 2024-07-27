//
// NativeLand  Copyright (C) 2023-2024  Aptivi
//
// This file is part of NativeLand
//
// NativeLand is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// NativeLand is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using NativeLand;
using Serilog;
using Serilog.Extensions.Logging;
using SpecProbe.Software.Platform;

namespace TestProcess
{
    internal class Program
    {
        private static ILoggerFactory _factory;

        internal static int Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            var factory = new LoggerFactory([new SerilogLoggerProvider()]);

            _factory = factory;
            try
            {
                File.Delete("libTestLib.dylib");
                File.Delete("libTestLib.so");
                File.Delete("TestLib.dll");
            }
            catch (Exception e)
            {
                Log.ForContext<Program>().Warning(e, $"Failed to cleanup libraries before running a test: {e.Message}");
            }

            try
            {
                CanLoadLibraryAndCallFunction();
                return 0;
            }
            catch (Exception e)
            {
                Log.ForContext<Program>().Error($"Test failed with exception: {e.GetType().Name}, {e.Message}");
                return 1;
            }
        }

        private static int CanLoadLibraryAndCallFunction()
        {
            // Make a temp directory
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempDir);

            // Copy libraries
            var accessor = new ResourceAccessor(Assembly.GetExecutingAssembly());
            if (PlatformHelper.IsOnWindows())
                File.WriteAllBytes(tempDir + @"\TestLib.dll", accessor.Binary("TestLib.dll"));
            else if (PlatformHelper.IsOnMacOS())
                File.WriteAllBytes(tempDir + @"/libTestLib.dylib", accessor.Binary("libTestLib.dylib"));
            else if (PlatformHelper.IsOnUnix())
            {
                if (PlatformHelper.IsOnArm64())
                    File.WriteAllBytes(tempDir + @"/libTestLib_Arm64.so", accessor.Binary("libTestLib_Arm64.so"));
                else
                    File.WriteAllBytes(tempDir + @"/libTestLib.so", accessor.Binary("libTestLib.so"));
            }

            // Now, create a library manager
            var libManager = new LibraryManager(
                new LibraryItem(Platform.Windows, Architecture.X64,
                    new LibraryFile(tempDir + @"\TestLib.dll")),
                new LibraryItem(Platform.MacOS, Architecture.X64,
                    new LibraryFile(tempDir + @"/libTestLib.dylib")),
                new LibraryItem(Platform.Linux, Architecture.X64,
                    new LibraryFile(tempDir + @"/libTestLib.so")),
                new LibraryItem(Platform.Linux, Architecture.Arm64,
                    new LibraryFile(tempDir + @"/libTestLib_Arm64.so")));

            // Load the library
            libManager.LoadNativeLibrary();

            // Get the hello result from the delegate
            int result;
            try
            {
                var @delegate = libManager.GetNativeMethodDelegate<Hello>("hello");
                result = @delegate.Invoke();
            }
            catch (Exception ex)
            {
                Log.ForContext<Program>().Error(ex, "Fatal exception with the SpecProbe library, most likely becuase the library wasn't loaded.");
                throw;
            }

            Log.ForContext<Program>().Information($"Function result is {result}");

            return result == 42 ? 0 : 1;
        }
        
        private delegate int Hello();
    }
}
