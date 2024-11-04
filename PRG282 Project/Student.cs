using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG282_Project
{
    internal class Student
    {
        private int ID, Age;
        private string Name, Course;

        Student(int iD, int age, string name, string course)
        {
            ID1 = iD;
            Age1 = age;
            Name1 = name;
            Course1 = course;
        }

        public int ID1 { get => ID; set => ID = value; }
        public int Age1 { get => Age; set => Age = value; }
        public string Name1 { get => Name; set => Name = value; }
        public string Course1 { get => Course; set => Course = value; }
    }
}
