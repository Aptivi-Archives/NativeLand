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

namespace NativeLand.Tools
{
    /// <summary>
    /// Contains useful functions to get paths relative to target assembly.
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Gets the directory specified assembly is located in.
        /// If the assembly was loaded from memory, returns environment
        /// working directory.
        /// </summary>
        /// <param name="targetAssembly">Assembly to get the directory from.</param>
        public static string GetCurrentDirectory(this Assembly targetAssembly)
        {
            string curDir;
            var ass = targetAssembly.Location;
            if (string.IsNullOrEmpty(ass))
            {
                curDir = Environment.CurrentDirectory;
            }
            else
            {
                curDir = Path.GetDirectoryName(ass);
            }

            return curDir;
        }

        /// <summary>
        /// Combines part of the path with assembly's directory.
        /// </summary>
        /// <param name="targetAssembly">Assembly to get directory from.</param>
        /// <param name="fileName">Right-hand part of the path.</param>
        public static string CombineWithCurrentDirectory(this Assembly targetAssembly, string fileName)
        {
            string curDir = targetAssembly.GetCurrentDirectory();
            return !string.IsNullOrEmpty(curDir) ? Path.Combine(curDir, fileName) : fileName;
        }
    }
}