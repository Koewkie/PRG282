using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PRG282_Project
{
    internal class DataHandler
    {
        public DataHandler() { }

        public List<Student> AddStudent(int iD, int age, string name, string course, List<Student> stdnList, FileHandler fileHandler)
        {
            //-----------IMPLEMENT-------------------
            return new List<Student>(); 
        }

        public List<Student> UpdateStudent(int iD, int age, string name, string course, List<Student> stdnList, FileHandler fileHandler)
        {
            //-----------IMPLEMENT-------------------
            return new List<Student>(); 
        }

        public List<Student> DeleteStudent(int iD, List<Student> stdnList, FileHandler fileHandler)
        {
            //-----------IMPLEMENT-------------------
            return new List<Student>();
        }

        public void DisplaySummary(List<Student> students, System.Windows.Forms.Label totalStudents, System.Windows.Forms.Label avgAge)
        {

            // Set label text to the count (total) of students
            int total = students.Count;
            totalStudents.Text += total.ToString();

            // Calculate average age and set label text
            double average = CalculateAverageAge(students, total);
            avgAge.Text += average.ToString();

        }

        public double CalculateAverageAge(List<Student> students, int total)
        {
            double average = 0.00;
            foreach (Student student in students)
            {
                average += student.Age1;
            }

            return average/total;
        }

    }
}
