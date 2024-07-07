using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibrary.Models
{
    public class StudentException : Exception
    {
        public StudentException(string? msg) : base(msg) { }

    }
}
