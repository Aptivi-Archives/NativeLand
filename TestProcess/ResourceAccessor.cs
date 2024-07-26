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
    /// A helper class to load resources from an assembly.
    /// </summary>
    public class ResourceAccessor
    {
        private readonly Assembly _assembly;
        private readonly string _assemblyName;

        /// <summary>
        /// Creates a resource accessor for the specified assembly.
        /// </summary>
        public ResourceAccessor(Assembly assembly)
        {
            _assembly = assembly;
            _assemblyName = _assembly.GetName().Name;
        }

        /// <summary>
        /// Gets a resource with specified name as an array of bytes.
        /// </summary>
        /// <param name="name">Resource name with folders separated by dots.</param>
        /// <exception cref="InvalidOperationException">
        /// When resource is not found.
        /// </exception>
        public byte[] Binary(string name)
        {
            using var stream = new MemoryStream();
            var resource = _assembly.GetManifestResourceStream(GetName(name)) ??
                throw new InvalidOperationException("Resource not available.");
            resource.CopyTo(stream);

            return stream.ToArray();
        }

        private string GetName(string name) =>
            name.StartsWith(_assemblyName) ? name : $"{_assemblyName}.{name}";
    }
}
