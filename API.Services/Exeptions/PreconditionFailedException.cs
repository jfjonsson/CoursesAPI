using System;

namespace API.Services.Exeptions
{
    public class PreconditionFailedException : Exception
    {
        public PreconditionFailedException(string message) : base(message)
        {
        }
    }
}
