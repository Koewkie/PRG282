using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRG282_Project
{
    public partial class frmMainMenu : Form
    {
        private bool success = false;

        private DataTable dt = new DataTable();
        public DataTable Dt { get => dt; set => dt = value; }

        List<Student> students = new List<Student>();

        FileHandler fh = new FileHandler();
        DataHandler dh;

        public frmMainMenu()
        {
            dh = new DataHandler(this);
            InitializeComponent();
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            students = fh.Read();                         

            dt.Columns.Add("Student ID", typeof(int));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Course", typeof(string));

            foreach(Student stdn in students)
            {
                dt.Rows.Add(stdn.ID1, stdn.Age1, stdn.Name1, stdn.Course1);
            }

            dgvStudents.DataSource = dt;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCourse.Text = "";
            txtAge.Text = "";
            txtID.Text = "";
            txtName.Text = "";
            txtSearch.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            students = dh.AddStudent(txtID.Text, txtAge.Text, txtName.Text, txtCourse.Text, students, fh);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            students = dh.UpdateStudent(txtID.Text, txtAge.Text, txtName.Text, txtCourse.Text, students, fh, dgvStudents);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<String> values = dh.Search(students, txtSearch.Text);

            if (values != null)
            {
                txtID.Text = values[0];
                txtName.Text = values[1];
                txtAge.Text = values[2];
                txtCourse.Text = values[3];
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool found = false;
                int ID = int.Parse(txtID.Text);
                
                for (int i = 0; i < dgvStudents.Rows.Count - 1; i++)
                {
                    if (dgvStudents.Rows[i].Cells[0].Value.ToString() == txtID.Text)
                    {
                        dgvStudents.Rows.RemoveAt(i);
                        dh.DeleteStudent(ID, students, fh);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    MessageBox.Show($"Student with ID: '{txtID.Text}' not found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter a valid student ID");
            }
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            // Reset label to their default property settings
            lblTotalStdn.Text = "Total Number of Students: ";
            lblAvg.Text = "Average age of Students: ";

            // Call Display and file save
            dh.DisplaySummary(students, lblTotalStdn, lblAvg);
            fh.GenerateSummary(students, dh);
        }
    }
}
