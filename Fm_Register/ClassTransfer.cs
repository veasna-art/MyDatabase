using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Demo_Method
{
    class ClassTransfer
    {
        public DataTable GetData(string ms_access_file, string sql_select)
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



    }
}
