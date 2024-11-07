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

        FileHandler fh = new FileHandler();
        DataHandler dh = new DataHandler();

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            students = fh.Read();                         //Implement this when Filehandler is complete

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
            try
            {
                students = dh.AddStudent(int.Parse(txtID.Text), int.Parse(txtAge.Text), txtName.Text, txtCourse.Text, students, fh);
            }
            catch (StudentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch
            {
                MessageBox.Show("Please enter all 'Add/Update' values and ensure they are correct");
            }
            
            dt.Rows.Add(int.Parse(txtID.Text), txtName.Text, int.Parse(txtAge.Text), txtCourse.Text);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                bool found = false;
                int ID = int.Parse(txtID.Text);
                int Age = int.Parse(txtAge.Text);

                for (int i = 0; i < dgvStudents.Rows.Count - 1; i++)
                {
                    if (dgvStudents.Rows[i].Cells[0].Value.ToString() == txtID.Text)
                    {
                        dgvStudents.Rows[i].Cells[1].Value = txtName.Text;
                        dgvStudents.Rows[i].Cells[2].Value = txtAge.Text;
                        dgvStudents.Rows[i].Cells[3].Value = txtCourse.Text;
                        students = 
                            dh.UpdateStudent(ID, Age, txtName.Text, txtCourse.Text, students, fh);
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
                MessageBox.Show("Please enter all 'Add/Update' values and ensure they are correct");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bool found = false;
            try
            {
                int searchID = int.Parse(txtSearch.Text);

                foreach (Student stdn in students)
                {
                    if (stdn.ID1 == searchID)
                    {
                        txtID.Text = stdn.ID1.ToString();
                        txtName.Text = stdn.Name1.ToString();
                        txtAge.Text = stdn.Age1.ToString();
                        txtCourse.Text = stdn.Course1.ToString();
                        found = true;
                        break;
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
