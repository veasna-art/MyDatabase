using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Demo_Method;
using Fm_Register;

namespace Fm_View
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private DataTable GetData(string ms_access_file, string sql_select)
        {
            //prepare 1
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString =
                "Provider=Microsoft.ACE.OLEDB.12.0;" +
                "Data Source=" + ms_access_file;
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            //SELECT * FROM table_name;
            command.CommandText = sql_select;//Sql
            DataTable table_student = new DataTable("Students");
            //connect
            connection.Open();
            //execute SQL
            OleDbDataReader data_reader = command.ExecuteReader();
            table_student.Load(data_reader);//while loop
            //close connection
            connection.Close();
            //connect table_student to DataGridView
            return table_student;//generate column
                                 //load data
        }
        private int RunSqlCommand(string ms_access_file, string sql)
        {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString =
                "Provider=Microsoft.ACE.OLEDB.12.0;" +
                "Data Source=" + ms_access_file;//Error1
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = sql;//Error2
            //connect
            connection.Open();//Error1
            //run SQL statement
            int result_count = command.ExecuteNonQuery();//run//Error2
            connection.Close();
            return result_count;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //ReadData();
            dataGridView1.DataSource = GetData(
                                    "|DataDirectory|\\DatabaseFile\\demo.accdb",
                                    "SELECT * FROM Students;");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bool conversion_result = int.TryParse(txtSearch.Text.Trim(), out int id);
            string sql_select = string.Empty;
            if (conversion_result == true)
            {
                sql_select = "SELECT * FROM Students WHERE Id = " + txtSearch.Text.Trim() + ";";

            }
            else
            {
                sql_select = "SELECT * FROM Students WHERE StuddentName LIKE '%" + txtSearch.Text.Trim() + "%' OR Phone = '" + txtSearch.Text.Trim() + "';";
            }

            string ms_access_file = "|DataDirectory|\\DatabaseFile\\demo.accdb";
            dataGridView1.DataSource = GetData(ms_access_file, sql_select);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.ShowDialog();
            this.Show();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();

            f2.editing_row = this.dataGridView1.SelectedRows[0];
            f2.txtID.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            f2.txtName.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            f2.cboGender.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            f2.txtPhone.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            f2.txtEmail.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();

            f2.txtID.ReadOnly = true;
            f2.txtName.SelectAll();
            f2.txtName.Focus();
            f2.ShowDialog();
            this.Show();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetData(
                                    "|DataDirectory|\\DatabaseFile\\demo.accdb",
                                    "SELECT * FROM Students;");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();

            f2.editing_row = this.dataGridView1.SelectedRows[0];
            f2.txtID.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            f2.txtName.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            f2.cboGender.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            f2.txtPhone.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            f2.txtEmail.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();

            f2.txtID.ReadOnly = true;
            f2.txtName.SelectAll();
            f2.txtName.Focus();
            f2.ShowDialog();
            this.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to delete?",
               "Confirms", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //delete from database
                string ms_access_file = "|DataDirectory|\\DatabaseFile\\demo.accdb";
                string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//
                string sql_delete = "DELETE FROM Students WHERE Id = " + id;
                int result_count = RunSqlCommand(ms_access_file, sql_delete);
                if (result_count > 0)
                {
                    dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                }
            }

        }

        private void txtSearch_DoubleClick(object sender, EventArgs e)
        {

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
