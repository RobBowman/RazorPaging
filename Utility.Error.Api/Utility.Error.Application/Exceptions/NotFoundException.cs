using System;

namespace Utility.Error.Application.Exceptions
{
    /// <summary>
    /// Not Found Validation.
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}

