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
    public partial class Form_ReceptionInfo : Form
    {
        public Form_ReceptionInfo(string user)
        {
            {
                InitializeComponent();
                populate();
                label1.Text = user;

            }
        }
        static readonly string constring = ConfigurationManager.ConnectionStrings["Hotel_Management.Properties.Settings.HotelConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constring);
        public void populate()
        {
            con.Open();
            string query = "select * from Reception";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
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
            SqlCommand Command = new SqlCommand("insert into Reception values(@ReceptID,@ReceptName,@ReceptPhone,@ReceptGender,@ReceptAddress,@ReceptPassword)", con);
            Command.Parameters.AddWithValue("@ReceptID", txt_ReceptionID.Text);
            Command.Parameters.AddWithValue("@ReceptName", txt_ReceptionName.Text);
            Command.Parameters.AddWithValue("@ReceptPhone", txt_ReceptionPhoneNumber.Text);
            Command.Parameters.AddWithValue("@ReceptGender", comboBox1.SelectedItem.ToString());
            Command.Parameters.AddWithValue("@ReceptAddress",txt_ReceptionAddress.Text);
            Command.Parameters.AddWithValue("@ReceptPassword",txt_ReceptionPassword.Text);


            Command.ExecuteNonQuery();
            MessageBox.Show("Reception Added Successfully!!!");
            con.Close();
            populate();
        }

        private void label_Delete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand Command = new SqlCommand("delete from Reception where @ReceptID=ReceptID", con);
            Command.Parameters.AddWithValue("@ReceptID", txt_ReceptionID.Text);

            Command.ExecuteNonQuery();
            MessageBox.Show("Reception Deleted Successfully!!!");
            con.Close();
            populate();
        }

        private void label_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
