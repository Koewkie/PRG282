using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace PRG282_Project
{
    internal class FileHandler
    {
        private string path = @"students.txt";

        public FileHandler() { }

        public List<Student> Read()
        {

            // Create return list var
            List<Student> students = new List<Student>();

            // Start a filestream and streamreader
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);

            using (StreamReader sr = new StreamReader(fs))
            {
                // Read file to end
                string text;
                while ((text = sr.ReadLine()) != null)
                {
                    // Split read line by commas (ID,Age,Name,Course)
                    string[] strings = text.Split(',');

                    // Create a student obj with retrieved data
                    Student newStudent = new Student(int.Parse(strings[0]), int.Parse(strings[1]), strings[2], strings[3]);

                    // Add new created student to the return list of students
                    students.Add(newStudent);
                }
            }

            // Close filestream
            fs.Close();
            
            // Return list of students read from file
            return students;

        }

        public void Write(List<Student> stdnList)
        {

            // Create filestream for streamwriter
            FileStream fs = new FileStream(path, FileMode.Create);

            // Temporarily use sw as StreamWriter (this will automatically close)
            using (StreamWriter sw = new StreamWriter(fs))
            {
                // Loop through the given student list
                string text;
                foreach (Student student in stdnList)
                {
                    // Build string text
                    text = student.ID1 + "," + student.Age1 + "," + student.Name1 + "," + student.Course1;

                    // Write string text to file
                    sw.WriteLine(text);
                }
            }

            // Close filestream
            fs.Close();

        }

        public void GenerateSummary(List<Student> stdnList, DataHandler dh)
        {

            // Define the file path for the summary
            string summarypath = @"summary.txt";

            // Create filestream for streamwriter
            FileStream fs = new FileStream(summarypath, FileMode.Create);

            // Temporarily use sw as StreamWriter (this will automatically close)
            using (StreamWriter sw = new StreamWriter(fs))
            {

                int total = stdnList.Count;
                double average = dh.CalculateAverageAge(stdnList, total);
                sw.WriteLine("Date: " + DateTime.Now.ToString() + "\tTotal Students: " + total.ToString() + "\tAverage Age: " + average.ToString());

            }

            // Close filestream
            fs.Close();

            // User Feedback
            MessageBox.Show("Summary saved to file");

        }
    }
}
