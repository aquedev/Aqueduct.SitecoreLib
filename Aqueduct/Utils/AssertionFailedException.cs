using System;

namespace Aqueduct.Utils
{
    public class AssertionFailedException : Exception
    {
        public AssertionFailedException (string message) 
            : base (message)
        {
        }
    }
}