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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hotel_Management
{
    public partial class Form_RoomInfo : Form
    {
        public Form_RoomInfo()
        {
            InitializeComponent();
            populate();
        }
        static readonly string constring = ConfigurationManager.ConnectionStrings["Hotel_Management.Properties.Settings.HotelConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constring);
        public void populate()
        {
            con.Open();
            string query = "select * from Room";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();

        }
    

        private void label_BackToLogin_Click(object sender, EventArgs e)
        {
            Form_AdminPage login = new Form_AdminPage("admin");
            login.Show();
            this.Hide();
        }

        private void label_Add_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand Command = new SqlCommand("insert into Room values(@RoomID,@RoomPhone,@RoomAvailable)", con);
            Command.Parameters.AddWithValue("@RoomID",txt_RoomNumber.Text);
            Command.Parameters.AddWithValue("@RoomPhone",txt_RoomPhoneNumber.Text.ToString());
            Command.Parameters.AddWithValue("@RoomAvailable",(radioButton_Yes.Checked)?"Free":"Rented");
       

            Command.ExecuteNonQuery();
            MessageBox.Show("Room Added Successfully!!!");
            con.Close();
            populate();
        }

        private void label_Delete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand Command = new SqlCommand("delete from Room where @RoomID=RoomID", con);
            Command.Parameters.AddWithValue("@RoomID", txt_RoomNumber.Text);

            Command.ExecuteNonQuery();
            MessageBox.Show("Room Deleted Successfully!!!");
            con.Close();
            populate();
        }

        private void label_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
