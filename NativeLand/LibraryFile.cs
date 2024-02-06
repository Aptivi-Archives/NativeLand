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

namespace NativeLand
{
	/// <summary>
	/// A class to store the information about native library file.
	/// </summary>
	public class LibraryFile
	{
		/// <summary>
		/// Makes a new instance of this class
		/// </summary>
		/// <param name="fileName">Filename to use when extracting the library.</param>
		/// <param name="resource">Library binary.</param>
		public LibraryFile(string fileName, byte[] resource)
		{
			FileName = fileName;
			Resource = resource;
		}

		/// <summary>
		/// Filename to use when extracting the library.
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// Library binary.
		/// </summary>
		public byte[] Resource { get; set; }

		/// <summary>
		/// Specifies whether this file is a shared library, which can be loaded explicitly with
		/// <code>LoadLibraryEx</code> on Windows and <code>dlopen</code> on Linux and MacOs.
		///
		/// Default is <code>True</code>, but explicit loading is disabled by default with
		/// <see cref="LibraryManager.LoadLibraryExplicit"/>.
		///
		/// Set this to <code>False</code> if this file is not a library, but a supporting file which
		/// shouldn't be loaded explicitly when <see cref="LibraryManager.LoadLibraryExplicit"/> is <code>True</code>. 
		/// </summary>
		public bool CanLoadExplicitly { get; set; } = true;
	}
}