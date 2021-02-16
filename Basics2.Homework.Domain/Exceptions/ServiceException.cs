using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basics2.Homework.Domain.Exceptions
{
    public class ServiceException : Exception
    {
        public string Message { get; set; }

        public ServiceException(string message)
        {
            Message = message;
        }
    }
}
