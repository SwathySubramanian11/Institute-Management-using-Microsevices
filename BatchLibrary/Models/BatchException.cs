using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchLibrary.Models
{
    public class BatchException : Exception
    {
        public BatchException(string? msg) : base(msg) { }

    }
}
