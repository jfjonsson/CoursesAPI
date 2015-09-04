using System;

namespace API.Services.Exeptions
{
    public class EntryRemovalFailedException : Exception
    {
        public EntryRemovalFailedException(Exception e) : base("Removing object failed: ", e)
        {
        }
    }
}
