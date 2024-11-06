using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG282_Project
{
    public class StudentException : Exception
    {
        public StudentException() { }

        public StudentException(string message) : base(message) { }
    }
}
