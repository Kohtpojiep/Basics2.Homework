using System;
using System.Collections.Generic;
using System.Text;

namespace Basics2.Homework.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public string Message { get; set; }

        public ValidationException(string message)
        {
            Message = message;
        }
    }
}
