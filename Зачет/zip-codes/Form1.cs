using Npgsql;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Reflection.Emit;
/*using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;*/

namespace zip_codes
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }


        string login;
        string pas;
        string connectionString;
        private DataSet ds = new DataSet();
        private DataSet ds1 = new DataSet();

        private DataTable dt = new DataTable();
        private DataTable dt1 = new DataTable();


        private void Form_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageLogin;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            login = textBoxLogin.Text;
            pas = textBoxPassword.Text;
            connectionString = $"Host=localhost;Port=5432;Database=zip_codes;Server Compatibility Mode=NoTypeLoading;Username={login};Password={pas}";
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            try
            {
                conn.Open();
                labelStatus.Text += $" Подключено.\n";

            }
            catch (Exception)
            {
                labelStatus.Text += " Не подключено.\n";
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Open) { 
                    conn.Close();
                    labelStatus.Text += " Соединение закрыто.\n";
                }
            }
        }

        private void tabPageSearch_Enter(object sender, EventArgs e)
        {
            try
            {
                string countries = "SELECT country_code FROM public.countries;";
                NpgsqlConnection conn = new NpgsqlConnection(connectionString);
                NpgsqlCommand com = new NpgsqlCommand(countries, conn);
                conn.Open();
                NpgsqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(0));
                }
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Подключение не выполнено");
                tabControl1.SelectedTab = tabPageLogin;
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string c_code = comboBox1.SelectedItem.ToString();
            string country = $"SELECT country_name FROM public.countries WHERE country_code = '{c_code}';";
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            NpgsqlCommand com = new NpgsqlCommand(country, conn);
            conn.Open();
            NpgsqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                label4.Text = reader.GetString(0);
            }
            conn.Close();
            string preview = $"SELECT * FROM get_zips_f('{c_code}');;";
            conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(preview, conn);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string c_code = comboBox1.SelectedItem.ToString();
            string zip = textBoxZIP1.Text.ToString();
            string search = $"SELECT * FROM get_address_f('{c_code}', '{zip}')";

            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            NpgsqlCommand com = new NpgsqlCommand(search, conn);
            NpgsqlCommand com1 = new NpgsqlCommand(search, conn);
            conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(search, conn);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void tabPageCorrect_Enter(object sender, EventArgs e)
        {

            string test_rw = $"SELECT EXISTS(SELECT b.rolname rname, c.rolname mname FROM pg_auth_members a, pg_roles b, pg_roles c WHERE a.roleid=b.oid AND a.member = c.oid AND b.rolname = 'zip_rw' AND c.rolname = '{login}' LIMIT 1);";
            NpgsqlConnection conn_check = new NpgsqlConnection(connectionString);
            NpgsqlCommand com_check = new NpgsqlCommand(test_rw, conn_check);
            try
            {
                conn_check.Open();
                NpgsqlDataReader reader_check = com_check.ExecuteReader();
                reader_check.Read();
                bool a = reader_check.GetBoolean(0);
                if (a)
                {
                    MessageBox.Show("Достаточно прав на запись");
                    string countries = "SELECT country_code FROM public.countries;";
                    NpgsqlConnection conn = new NpgsqlConnection(connectionString);
                    NpgsqlCommand com = new NpgsqlCommand(countries, conn);
                    conn.Open();
                    NpgsqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader.GetString(0));
                                            }
                }
                else
                {
                    MessageBox.Show("Недостаточно прав на запись");
                    tabControl1.SelectedTab = tabPageLogin;
                }
                conn_check.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Подключение не выполнено\n");
                tabControl1.SelectedTab = tabPageLogin;
            }

        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            string c_code = comboBox2.SelectedItem.ToString();
            string search_c = $"SELECT country_id FROM countries WHERE country_code ='{c_code}';";
            NpgsqlConnection con3 = new NpgsqlConnection(connectionString);
            NpgsqlCommand com3 = new NpgsqlCommand(search_c, con3);
            con3.Open();
            NpgsqlDataReader reader3 = com3.ExecuteReader();
            reader3.Read();
            int c_id = reader3.GetInt32(0);
            con3.Close();
            string country = $"SELECT country_name FROM public.countries WHERE country_code = '{c_code}';";
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            NpgsqlCommand com = new NpgsqlCommand(country, conn);
            conn.Open();
            NpgsqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                labelCname2.Text = reader.GetString(0);
            }
            conn.Close();
            string preview = $"SELECT * FROM zipaddresses WHERE country_id = '{c_id}' LIMIT 5;";
            conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(preview, conn);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            dataGridView2.DataSource = dt;
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string c_code = comboBox2.SelectedItem.ToString();
            string search_c = $"SELECT country_id FROM countries WHERE country_code ='{c_code}';";
            NpgsqlConnection con3 = new NpgsqlConnection(connectionString);
            NpgsqlCommand com3 = new NpgsqlCommand(search_c, con3);
            con3.Open();
            NpgsqlDataReader reader3 = com3.ExecuteReader();
            reader3.Read();
            int c_id = reader3.GetInt32(0);
            con3.Close();


            string zip = textBoxZIP2.Text.ToString();
            string search = $"SELECT zip_id, city, state, county, community FROM zipaddresses WHERE country_id = '{c_id}' AND zip_code = '{zip}';";
            string search1 = $"SELECT zip_id, lat, lon FROM zipcoords WHERE country_id = '{c_id}' AND zip_code = '{zip}';";

            NpgsqlConnection con4 = new NpgsqlConnection(connectionString);

            con4.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(search, con4);
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(search1, con4);
            ds.Reset();
            da.Fill(ds, "addresses");
            dt = ds.Tables["addresses"];
            dataGridView3.DataSource = dt;
            ds1.Reset();
            da1.Fill(ds1, "coords");
            dt1 = ds1.Tables["coords"];
            dataGridView2.DataSource = dt1;
            NpgsqlCommandBuilder commands2 = new NpgsqlCommandBuilder(da);
            con4.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string c_code = comboBox2.SelectedItem.ToString();
            string search_c = $"SELECT country_id FROM countries WHERE country_code ='{c_code}';";
            NpgsqlConnection con3 = new NpgsqlConnection(connectionString);
            NpgsqlCommand com3 = new NpgsqlCommand(search_c, con3);
            con3.Open();
            NpgsqlDataReader reader3 = com3.ExecuteReader();
            reader3.Read();
            int c_id = reader3.GetInt32(0);
            con3.Close();

            string zip = textBoxZIP2.Text.ToString();
            string search = $"SELECT zip_id, city, state, county, community FROM zipaddresses WHERE country_id = '{c_id}' AND zip_code = '{zip}';";
            string search1 = $"SELECT zip_id, lat, lon FROM zipcoords WHERE country_id = '{c_id}' AND zip_code = '{zip}';";

            NpgsqlConnection con4 = new NpgsqlConnection(connectionString);

            con4.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(search, con4);
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(search1, con4);
            da.Fill(ds, "addresses");
            da1.Fill(ds1, "coords");


            NpgsqlCommand updadr = new NpgsqlCommand("UPDATE zipaddresses SET city = @city, state = @state, county = @county, community = @community WHERE zip_id = @zip_id;", con4);
            updadr.Parameters.Add("@city", NpgsqlTypes.NpgsqlDbType.Varchar, 100, "city");
            updadr.Parameters.Add("@state", NpgsqlTypes.NpgsqlDbType.Varchar, 100, "state");
            updadr.Parameters.Add("@county", NpgsqlTypes.NpgsqlDbType.Varchar, 100, "county");
            updadr.Parameters.Add("@community", NpgsqlTypes.NpgsqlDbType.Varchar, 100, "community");
            updadr.Parameters.Add("@zip_id", NpgsqlTypes.NpgsqlDbType.Integer, 12, "zip_id");
            NpgsqlParameter para_adr = updadr.Parameters.Add("@oldzip_id", NpgsqlTypes.NpgsqlDbType.Integer, 12, "zip_id");
            para_adr.SourceVersion = DataRowVersion.Original;
            da.UpdateCommand = updadr;
            da.Update(ds, "addresses");

            NpgsqlCommand updcoords = new NpgsqlCommand("UPDATE zipcoords SET lat = @lat, lon = @lon WHERE zip_id = @zip_id;", con4);
            updcoords.Parameters.Add("@lat", NpgsqlTypes.NpgsqlDbType.Double, 24, "lat");
            updcoords.Parameters.Add("@lon", NpgsqlTypes.NpgsqlDbType.Double, 24, "lon");
            updcoords.Parameters.Add("@zip_id", NpgsqlTypes.NpgsqlDbType.Integer, 12, "zip_id");
            NpgsqlParameter para_coords = updcoords.Parameters.Add("@oldzip_id", NpgsqlTypes.NpgsqlDbType.Integer, 12, "zip_id");
            para_coords.SourceVersion = DataRowVersion.Original;
            da1.UpdateCommand = updcoords;
            da1.Update(ds1, "coords");

            con4.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string c_code = comboBox2.SelectedItem.ToString();
            string zip = textBoxZIP2.Text.ToString();
            string search_c = $"SELECT country_id FROM countries WHERE country_code ='{c_code}';";
            NpgsqlConnection con3 = new NpgsqlConnection(connectionString);
            NpgsqlCommand com3 = new NpgsqlCommand(search_c, con3);
            con3.Open();
            NpgsqlDataReader reader3 = com3.ExecuteReader();
            reader3.Read();
            int c_id = reader3.GetInt32(0);
            string get_max = $"SELECT MAX(zip_id) FROM zipaddresses;";
            con3.Close();
            con3.Open();
            NpgsqlCommand com4 = new NpgsqlCommand(get_max, con3);
            NpgsqlDataReader reader4 = com4.ExecuteReader();
            reader4.Read();
            int zip_id_max = reader4.GetInt32(0) + 1;
            con3.Close();

            NpgsqlConnection con4 = new NpgsqlConnection(connectionString);
            con4.Open();
            NpgsqlCommand insadr = new NpgsqlCommand($"INSERT INTO zipaddresses (zip_id, country_id, zip_code) VALUES ('{zip_id_max}', '{c_id}', '{zip}') ON CONFLICT DO NOTHING;", con4);
            NpgsqlCommand inscoords = new NpgsqlCommand($"INSERT INTO zipcoords (zip_id, country_id, zip_code, lat, lon) VALUES ('{zip_id_max}', '{c_id}', '{zip}', '0', '0') ON CONFLICT DO NOTHING;", con4);
            try
            {
                insadr.ExecuteNonQuery();
                inscoords.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка: \n" + ex);
            }
            con4.Close();
        }

        private void tabPageDistance_Enter(object sender, EventArgs e)
        {
            try
            {
                string countries = "SELECT country_code FROM public.countries;";
                NpgsqlConnection conn = new NpgsqlConnection(connectionString);
                NpgsqlCommand com = new NpgsqlCommand(countries, conn);
                conn.Open();
                NpgsqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    comboBox3.Items.Add(reader.GetString(0));
                    comboBox4.Items.Add(reader.GetString(0));
                }
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Подключение не выполнено");
                tabControl1.SelectedTab = tabPageLogin;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label12.Text = "Координаты: ";
            label13.Text = "Координаты: ";
            label14.Text = "Растояние: ";
            string zip1 = textBox1.Text.ToString();
            string zip2 = textBox2.Text.ToString();
            string c_code1 = comboBox3.SelectedItem.ToString();
            string c_code2 = comboBox4.SelectedItem.ToString();
            string search_c1 = $"SELECT country_id FROM countries WHERE country_code ='{c_code1}';";
            string search_c2 = $"SELECT country_id FROM countries WHERE country_code ='{c_code2}';";
            NpgsqlConnection con5 = new NpgsqlConnection(connectionString);
            NpgsqlCommand com5 = new NpgsqlCommand(search_c1, con5);
            con5.Open();
            NpgsqlDataReader reader5 = com5.ExecuteReader();
            reader5.Read();
            int c_id1 = reader5.GetInt32(0);
            con5.Close();
            con5.Open();
            NpgsqlCommand com6 = new NpgsqlCommand(search_c2, con5);
            NpgsqlDataReader reader6 = com6.ExecuteReader();
            reader6.Read();
            int c_id2 = reader6.GetInt32(0);
            con5.Close();
            string get_la1 = $"SELECT lat FROM zipcoords WHERE country_id ='{c_id1}' AND zip_code = '{zip1}';";
            string get_lo1 = $"SELECT lon FROM zipcoords WHERE country_id ='{c_id1}' AND zip_code = '{zip1}';";
            string get_la2 = $"SELECT lat FROM zipcoords WHERE country_id ='{c_id2}' AND zip_code = '{zip2}';";
            string get_lo2 = $"SELECT lon FROM zipcoords WHERE country_id ='{c_id2}' AND zip_code = '{zip2}';";
            double la1 = 0.0 ;
            double lo1 = 0.0 ;
            double la2 = 0.0 ;
            double lo2 = 0.0 ;
            label14.Visible = true;
            try
            {
                con5.Open();
                NpgsqlCommand com7 = new NpgsqlCommand(get_la1, con5);
                NpgsqlDataReader reader7 = com7.ExecuteReader();
                reader7.Read();
                la1 = reader7.GetDouble(0);
                con5.Close();
                con5.Open();
                NpgsqlCommand com8 = new NpgsqlCommand(get_lo1, con5);
                NpgsqlDataReader reader8 = com8.ExecuteReader();
                reader8.Read();
                lo1 = reader8.GetDouble(0);
                con5.Close();
                label12.Visible = true;
                label12.Text += la1.ToString() + ", " + lo1.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Координаты 1 не найдены" + ex);
                label14.Visible = false;
            }
            try
            {
                con5.Open();
                NpgsqlCommand com9 = new NpgsqlCommand(get_la2, con5);
                NpgsqlDataReader reader9 = com9.ExecuteReader();
                reader9.Read();
                la2 = reader9.GetDouble(0);
                con5.Close();
                con5.Open();
                NpgsqlCommand com10 = new NpgsqlCommand(get_lo2, con5);
                NpgsqlDataReader reader10 = com10.ExecuteReader();
                reader10.Read();
                lo2 = reader10.GetDouble(0);
                con5.Close();
                label13.Visible = true;
                label13.Text += la2.ToString() + ", " + lo2.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Координаты 2 не найдены" + ex);
                label14.Visible = false;
            }
            //Перевод координат в радианы:
            double la1r = la1 * Math.PI / 180;
            double lo1r = lo1 * Math.PI / 180;
            double la2r = la2 * Math.PI / 180;
            double lo2r = lo2 * Math.PI / 180;
            //Вычисления
            double sin21 = Math.Pow(Math.Sin((la2r - la1r) / 2), 2);
            double sin22 = Math.Pow(Math.Sin((lo2r - lo1r) / 2), 2);
            double cos1 = Math.Cos(la1r);
            double cos2 = Math.Cos(la2r);
            double R = 6371.2;
            double formula1 = Math.Sqrt(Math.Abs(sin21 + cos1 * cos2 * sin22));
            double formula2 = Math.Asin(formula1);
            double d = 2 * R * formula2;
            label14.Text += d.ToString("0.00") + " км.";
        }
    }
}
