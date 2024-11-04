using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG282_Project
{
    internal class FileHandler
    {
        private string path = "students.txt";

        public FileHandler() { }

        public List<Student> Read()
        {
            //-----------IMPLEMENT-------------------
            return new List<Student>(); 
        }

        public void Write(List<Student> stdnList)
        {
            //-----------IMPLEMENT-------------------
        }

        public void GenerateSummary(List<Student> stdnList)
        {
            //-----------IMPLEMENT-------------------
        }
    }
}
