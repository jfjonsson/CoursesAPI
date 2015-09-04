using System;

namespace API.Services.Exeptions
{
    public class EntryUpdateFailedException : Exception
    {
        public EntryUpdateFailedException(Exception e) : base("Updating object failed: ", e)
        {
        }
    }
}
