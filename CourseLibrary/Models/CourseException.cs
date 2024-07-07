using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.Models
{
    public class CourseException : Exception
    {
        public CourseException(string? msg) : base(msg) { }

    }
}
