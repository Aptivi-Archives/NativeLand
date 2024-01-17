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

using NativeLand.Tools;
using System;
using System.Runtime.InteropServices;

namespace NativeLand
{
    /// <summary>
    /// Library binaries for specified platform and bitness.
    /// </summary>
    public class LibraryItem
    {
        /// <summary>
        /// Makes a new instance of this class
        /// </summary>
        /// <param name="platform">Binary platform.</param>
        /// <param name="bitness">Binary bitness.</param>
        /// <param name="files">A collection of files for this bitness and platform.</param>
        public LibraryItem(Platform platform, Architecture bitness, params LibraryFile[] files)
        {
            Platform = platform;
            Bitness = bitness;
            Files = files;
        }

        /// <summary>
        /// Library files.
        /// </summary>
        public LibraryFile[] Files { get; set; }

        /// <summary>
        /// Platform for which this binary is used.
        /// </summary>
        public Platform Platform { get; set; }

        /// <summary>
        /// Bitness for which this binary is used.
        /// </summary>
        public Architecture Bitness { get; set; }

        /// <summary>
        /// Unpacks the library and directly loads it if on Windows.
        /// </summary>
        /// <param name="targetDirectory">Target directory to which library is extracted.</param>
        /// <param name="loadLibrary">Load library explicitly.</param>
        public virtual void LoadItem(string targetDirectory, bool loadLibrary)
        {
            throw new InvalidOperationException("This item was never added to the LibraryManager. Create a LibraryManager, add this item and then call LibraryManager.LoadNativeLibrary().");
        }
    }
}
