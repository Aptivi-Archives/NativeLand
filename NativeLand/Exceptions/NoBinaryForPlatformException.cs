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

namespace NativeLand.Exceptions
{
    /// <summary>
    /// Thrown when there is no binary for current platform and bitness.
    /// </summary>
    public class NoBinaryForPlatformException : Exception
    {
        /// <inheritdoc />
        public NoBinaryForPlatformException()
        {
        }

        /// <inheritdoc />
        public NoBinaryForPlatformException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public NoBinaryForPlatformException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
