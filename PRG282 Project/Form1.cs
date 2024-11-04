using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG282_Project
{
    public partial class frmMainMenu : Form
    {
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private DataTable dt = new DataTable();
        List<Student> students = new List<Student>();

        //Filehandler fh = new Filehandler();               //Implement this when Filehandler is complete

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            //students = fh.read();                         //Implement this when Filehandler is complete


            //-----------------------------Temporary student list for testing-----------------------------
            Student student1 = new Student(1, 20, "Stdn1", "Course1");
            Student student2 = new Student(2, 21, "Stdn2", "Course2");
            Student student3 = new Student(3, 22, "Stdn3", "Course3");

            students.Add(student1);
            students.Add(student2);
            students.Add(student3);
            //--------------------------------------------------------------------------------------------


            dt.Columns.Add("Student ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Course", typeof(string));

            foreach(Student stdn in students)
            {
                dt.Rows.Add(stdn.ID1, stdn.Name1, stdn.Age1, stdn.Course1);
            }

            dgvStudents.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string course = txtCourse.Text;
            int age = int.Parse(txtAge.Text);
            int id = int.Parse(txtID.Text);

            Student tempstudent = new Student(id, age, name, course);
            students.Add(tempstudent);

            dt.Rows.Add(id, name, age, course);

            //fh.Add(students);
        }
    }
}
