using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalIMCTask.Common.Errors
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string subject, string message) : base(subject, message)
        {

        }
    }
}
