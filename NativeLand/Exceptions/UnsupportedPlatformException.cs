using System;

namespace NativeLand.Exceptions
{
    /// <summary>
    /// Thrown when platform is not supported.
    /// </summary>
    public class UnsupportedPlatformException : Exception
    {
        /// <inheritdoc />
        public UnsupportedPlatformException()
        {
        }

        /// <inheritdoc />
        public UnsupportedPlatformException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public UnsupportedPlatformException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
