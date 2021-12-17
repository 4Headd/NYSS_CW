using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cw_wpf
{
    public class WrongKeyException : Exception
    {
        public WrongKeyException(string exceptionMessage) : base(exceptionMessage)
        {

        }
    }
}
