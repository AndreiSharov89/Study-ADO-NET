using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace DBConnection
{
    public partial class Form1 : Form
    {

        OleDbConnection connection = new OleDbConnection();
        public Form1()
        {
            InitializeComponent();
            this.connection.StateChange += new System.Data.StateChangeEventHandler(this.connection_StateChange);
        }

        private void connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            стартToolStripMenuItem.Enabled =
                (e.CurrentState == ConnectionState.Closed);
            стопToolStripMenuItem.Enabled =
                (e.CurrentState == ConnectionState.Open);

        }

        static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
                returnValue = settings.ConnectionString;
            return returnValue;
        }
        string testConnect = GetConnectionStringByName("DBConnect.NorthwndConnectionString");

        private void стартToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string testConnect = @"Provider = SQLOLEDB.1; Integrated Security = SSPI; Persist Security Info = False; Initial Catalog = nortwnd; Data Source = (local)";
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = testConnect;
                    connection.Open();
                    MessageBox.Show("Соединение с базой данных выполнено успешно");
                }
                else
                    MessageBox.Show("Соединение с базой данных уже установлено");
            }
            /*catch (OleDbException XcpSQL)
                {
                foreach (OleDbError se in XcpSQL.Errors)
                {
                MessageBox.Show(se.Message,
                "SQL Error code " + se.NativeError,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                }*/
            catch (Exception Xcp)
            {
                MessageBox.Show(Xcp.Message, "Unexpected Exception",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void стопToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                MessageBox.Show("Соединение с базой данных закрыто");
            }
            else
                MessageBox.Show("Соединение с базой данных уже закрыто");
        }

        private void списокСоедToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionStringSettingsCollection settings =
           ConfigurationManager.ConnectionStrings;
            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings) 
                //после перехода к получению строки через файл конфигурации приложения - стало вылезать 6 окон вместо 3 в приложении ранее, почему так?
                {
                    MessageBox.Show("name = " + cs.Name);
                    MessageBox.Show("providerName = " + cs.ProviderName);
                    MessageBox.Show("connectionString = " + cs.ConnectionString);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                label1.Text = "Сколько продуктов";
                MessageBox.Show("Сначала подключитесь к базе");
                return;
            }
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "SELECT COUNT(*) FROM Products";
            int number = (int)command.ExecuteScalar();
            label1.Text = number.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                listView1.Items.Clear();
                //listView1.Items.Add("Список продуктов");
                MessageBox.Show("Сначала подключитесь к базе");
                return;
            }
            OleDbCommand command = connection.CreateCommand();
            command.CommandText = "SELECT ProductName FROM Products";
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                listView1.Items.Add(reader["ProductName"].ToString());
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection(testConnect);
            connection.Open();
            OleDbTransaction OleTran = connection.BeginTransaction();
            OleDbCommand command = connection.CreateCommand();
            command.Transaction = OleTran;
            try
            {
                command.CommandText =
              //"INSERT INTO Products (ProductName) VALUES('Wrong size')"
              "INSERT INTO Products (ProductName1) VALUES('Wrong size')";

                command.ExecuteNonQuery();
                command.CommandText =
               //"INSERT INTO Products (ProductName) VALUES('Wrong color')"
               "INSERT INTO Products2 (ProductName) VALUES('Wrong color')";
                command.ExecuteNonQuery();
                OleTran.Commit();
                MessageBox.Show("Both records were written to database");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                try
                {
                    OleTran.Rollback();
                }
                catch (Exception exRollback)
                {
                    MessageBox.Show(exRollback.Message);
                }
            }
            connection.Close();
        }
    }
}
