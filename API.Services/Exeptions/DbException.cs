using System;

namespace API.Services.Exeptions
{
    public class DbException : Exception
    {
        public DbException(Exception e) : base("Database something failed: ", e)
        {
        }
    }
}
