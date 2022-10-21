using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management
{
    public partial class Form_ReservationInfo : Form
    {
        public Form_ReservationInfo(string user)
        {
            InitializeComponent();
            populate();
            fillClientcombo();
            fillRoomcombo();
            label1.Text = user;
            
        }
        static readonly string constring = ConfigurationManager.ConnectionStrings["Hotel_Management.Properties.Settings.HotelConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constring);
        public void populate()
        {
            con.Open();
            string query = "select * from Reservation";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();

        }
        public void fillClientcombo()
        {
            con.Open();
            SqlCommand command = new SqlCommand("select ClientName from Client", con);
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("ClientName",typeof(string));
            dt.Load(reader);
            comboBox1.ValueMember = "ClientName";
            comboBox1.DataSource = dt;     
            con.Close();
        }
        public void fillRoomcombo()
        {
            con.Open();
            SqlCommand command = new SqlCommand("select RoomID from Room where RoomAvailable = 'Free' ", con);
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RoomID", typeof(int));
            dt.Load(reader);
            comboBox2.ValueMember = "RoomID";
            comboBox2.DataSource = dt;
            con.Close();
        }
        private void label_BackToLogin_Click(object sender, EventArgs e)
        {
            Form_AdminPage login = new Form_AdminPage(label1.Text);
            login.Show();
            this.Hide();
        }

        private void label_Add_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand Command = new SqlCommand("insert into Reservation values(@ClientName,@RoomID,@DateIn,@DateOut)", con);
            Command.Parameters.AddWithValue("@ClientName",comboBox1.Text.ToString());
            Command.Parameters.AddWithValue("@RoomID",comboBox2.Text.ToString());
            Command.Parameters.AddWithValue("@DateIn",dateTimePicker1.Text.ToString());
            Command.Parameters.AddWithValue("@DateOut",dateTimePicker2.Text.ToString());


            Command.ExecuteNonQuery();
            MessageBox.Show("Reservation Done Successfully!!!");
            con.Close();
            populate();
        }

        private void label_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
