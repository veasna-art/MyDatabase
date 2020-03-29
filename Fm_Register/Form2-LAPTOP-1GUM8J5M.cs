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
using Fm_View;

namespace Fm_Register
{
    public partial class Form2 : Form
    {
        Form1 f1 = new Form1();
        OleDbConnection con = new OleDbConnection();
        //OleDbCommand cmd;

        public Form2()
        {
            InitializeComponent();
            con.ConnectionString =
                 "Provider=Microsoft.ACE.OLEDB.12.0;" +
                 "Data Source=";
        }
        public void Clear()
        {
            txtID.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            cboGender.SelectedIndex = -1;

        }
        private DataTable GetData(string ms_access_file, string sql_select)
        {
            //prepare
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
        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public DataGridViewRow editing_row;

        private void btnSave_Click(object sender, EventArgs e)
        {
            string ms_access_file = "|DataDirectory|\\DatabaseFile\\demo.accdb";
            if (editing_row == null)
            {
                string sql_insert = "INSERT INTO " +
                    " Students(Id, StudentName, Gender, Phone, Email) " +
                    " VALUES (" +
                    txtID.Text + "," +
                    "'" + txtName.Text + "'," +
                    "'" + cboGender.Text + "'," +
                    "'" + txtPhone.Text + "'," +
                    "'" + txtEmail.Text + "'" +
                    ")";
                int result_count = RunSqlCommand(ms_access_file, sql_insert);
                MessageBox.Show("Data saved");
                //ClearForm();
                Clear();
            }
            else
            {
                //update database

                string sql_update = "UPDATE Students SET " +
                    "StudentName = '" + txtName.Text.Trim() + "', " +
                    "Gender = '" + cboGender.Text.Trim() + "', " +
                    "Phone = '" + txtPhone.Text.Trim() + "', " +
                    "Email = '" + txtEmail.Text.Trim() + "'" +
                    " Where Id = " + txtID.Text;
                RunSqlCommand(ms_access_file, sql_update);
                //update datagridview

                editing_row.Cells[1].Value = txtName.Text.Trim();
                editing_row.Cells[2].Value = cboGender.Text.Trim();
                editing_row.Cells[3].Value = txtPhone.Text.Trim();
                editing_row.Cells[4].Value = txtEmail.Text.Trim();
                editing_row = null;

                //f1.dataGridView1.CurrentRow.Cells[1].Value = txtName.Text.Trim();
                //f1.dataGridView1.CurrentRow.Cells[2].Value = cboGender.Text.Trim();
                //f1.dataGridView1.CurrentRow.Cells[3].Value = txtPhone.Text.Trim();
                //f1.dataGridView1.CurrentRow.Cells[4].Value = txtEmail.Text.Trim();


            }

        }

    }
}
