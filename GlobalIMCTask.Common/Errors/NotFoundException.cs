using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalIMCTask.Common.Errors
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string subject, string message) : base(subject, message)
        {

        }
    }
}
