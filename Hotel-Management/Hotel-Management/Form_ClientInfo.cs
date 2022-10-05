using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
namespace Hotel_Management
{
    public partial class Form_ClientInfo : Form
    {
        public Form_ClientInfo()
        {
            InitializeComponent();
            populate();
        }
        static readonly string constring = ConfigurationManager.ConnectionStrings["Hotel_Management.Properties.Settings.HotelConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constring);
        public void populate()
        {
            con.Open();
            string query = "select * from Client";
            SqlDataAdapter adapter = new SqlDataAdapter(query,con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
            
        }
        private void label_BackToLogin_Click(object sender, EventArgs e)
        {
            Form_Login login = new Form_Login();
            login.Show();
            this.Hide();
        }

        private void label_Add_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand Command = new SqlCommand("insert into Client values(@ClientID,@ClientName,@ClientPhone,@ClientCountry)", con);
            Command.Parameters.AddWithValue("@ClientID", txt_ClientID.Text);
            Command.Parameters.AddWithValue("@ClientName", txt_ClientName.Text);
            Command.Parameters.AddWithValue("@ClientPhone", txt_ClientPhoneNumber.Text);
            Command.Parameters.AddWithValue("@ClientCountry", comboBox1.SelectedItem.ToString());

            Command.ExecuteNonQuery();
            MessageBox.Show("Client Added Successfully!!!");
            con.Close();
            populate();
        }

        private void label_Delete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand Command = new SqlCommand("delete from Client where @ClientID=ClientID", con);
            Command.Parameters.AddWithValue("@ClientID", txt_ClientID.Text);

            Command.ExecuteNonQuery();
            MessageBox.Show("Client Deleted Successfully!!!");
            con.Close();
            populate();
        }
    }
}
