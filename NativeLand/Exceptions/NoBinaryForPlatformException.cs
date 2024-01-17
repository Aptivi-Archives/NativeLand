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
