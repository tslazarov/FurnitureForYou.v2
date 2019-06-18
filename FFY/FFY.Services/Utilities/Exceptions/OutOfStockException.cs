using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Services.Utilities.Exceptions
{
    public class OutOfStockException : Exception
    {
        public OutOfStockException(string message) : base(message)
        {

        }
    }
}
