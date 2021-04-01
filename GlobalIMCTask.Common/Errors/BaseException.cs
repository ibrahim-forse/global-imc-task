using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalIMCTask.Common.Errors
{
    public class BaseException : Exception
    {
        public string Subject { set; get; }
        public BaseException(string subject, string message): base(message)
        {
            Subject = subject;
        }
    }
}
