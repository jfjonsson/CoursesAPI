using System;

namespace API.Services.Exeptions
{
    public class CreateEntryFailedException : Exception
    {
        public CreateEntryFailedException(Exception e) : base("Creating new object failed: ", e)
        {
        }
    }
}
