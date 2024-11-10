using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace PRG282_Project
{
    internal class DataHandler
    {
        private frmMainMenu menuForm;

        public DataHandler() { }

        public DataHandler(frmMainMenu frm) { menuForm = frm; }

        public List<Student> AddStudent(string iD, string age, string name, string course, List<Student> stdnList, FileHandler fileHandler)
        {
            try
            {
                int stdnID = int.Parse(iD);
                int stdnAge = int.Parse(age);

                Student tempStdn = stdnList.FirstOrDefault(stdn => stdn.ID1 == stdnID); //find student with matching id or return null
                if (tempStdn == null)   //if no student with id exists => create and add new student
                {
                    Student newStudent = new Student(stdnID, stdnAge, name, course);    //create
                    stdnList.Add(newStudent);                                           //add
                    fileHandler.Write(stdnList);                                        //write
                    menuForm.Dt.Rows.Add(stdnID, stdnAge, name, course);                //update grid
                    return stdnList;
                }
                else
                {
                    MessageBox.Show($"Student with ID: '{stdnID}' aready exists!");
                }
            }
            catch
            {
                MessageBox.Show("Please enter all 'Add/Update' values and ensure they are correct");
            }

            return stdnList;
        }

        public List<Student> UpdateStudent(string iD, string age, string name, string course, List<Student> stdnList, FileHandler fileHandler, DataGridView dgv)
        {
            try
            {
                int stdnID = int.Parse(iD);
                int stdnAge = int.Parse(age);
                bool found = false;

                Student studentToUpdate = stdnList.FirstOrDefault(stdn => stdn.ID1 == stdnID);  //find student with matching id or return null

                if (studentToUpdate != null)    //if student exist, update values
                {
                    studentToUpdate.Age1 = stdnAge;
                    studentToUpdate.Name1 = name;
                    studentToUpdate.Course1 = course;

                    fileHandler.Write(stdnList);    //write file

                    for (int i = 0; i < dgv.Rows.Count - 1; i++)        //search student in datagrid
                    {
                        if (dgv.Rows[i].Cells[0].Value.ToString() == iD)    //if match, update values in grid
                        {
                            dgv.Rows[i].Cells[1].Value = age;
                            dgv.Rows[i].Cells[2].Value = name;
                            dgv.Rows[i].Cells[3].Value = course;
                            MessageBox.Show("Update Successful!");
                            found = true;
                            break;
                        }
                    }   
                }
                if (!found)
                {
                    MessageBox.Show($"Student with ID: '{iD}' not found!");
                }
            }
            catch
            {
                MessageBox.Show("Please enter all 'Add/Update' values and ensure they are correct");
            }

            return stdnList; 
        }

        public List<String> Search(List<Student> stdnList, string id)
        {
            bool found = false;
            List<String> values = new List<string>();
            try
            {
                int searchID = int.Parse(id);

                foreach (Student stdn in stdnList)  //search for student
                {
                    if (stdn.ID1 == searchID) //if found, add details to list and return
                    {
                        values.Add(stdn.ID1.ToString());
                        values.Add(stdn.Name1.ToString());
                        values.Add(stdn.Age1.ToString());
                        values.Add(stdn.Course1.ToString());
                        MessageBox.Show("Add Successful!");
                        return values;
                    }
                }
                if (!found)
                {
                    MessageBox.Show($"Student with ID: '{searchID}' not found!");
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid student ID");
                return null;
            }

            return null;
        }

        public List<Student> DeleteStudent(string iD, List<Student> stdnList, FileHandler fileHandler, DataGridView dgv)
        {
            try
            {
                int stdnID = int.Parse(iD);
                bool found = false;


                Student studentToDelete = stdnList.FirstOrDefault(stdn => stdn.ID1 == stdnID);

                if (studentToDelete != null)
                {
                    stdnList.Remove(studentToDelete);               //delete from list
                    fileHandler.Write(stdnList);                    //write to file

                    for (int i = 0; i < dgv.Rows.Count - 1; i++)    //search id in datagrid
                    {
                        if (dgv.Rows[i].Cells[0].Value.ToString() == iD)
                        {
                            dgv.Rows.RemoveAt(i);                   //remove
                            MessageBox.Show("Delete Successful!");
                            found = true;
                            break;
                        }
                    } 
                }
                if (!found)
                {
                    MessageBox.Show($"Student with ID: '{iD}' not found in the list!");
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid ID for deletion.");
            }

            return stdnList;
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
