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
    public partial class Form_StaffInfo : Form
    {
        public Form_StaffInfo()
        {
            InitializeComponent();
            populate();
        }
        static readonly string constring = ConfigurationManager.ConnectionStrings["Hotel_Management.Properties.Settings.HotelConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constring);
        public void populate()
        {
            con.Open();
            string query = "select * from Staff";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
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
            this.Close();
        }

        private void label_Add_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand Command = new SqlCommand("insert into Staff values(@StaffID,@StaffName,@StaffPhone,@StaffGender,@StaffPassword)", con);
            Command.Parameters.AddWithValue("@StaffID", txt_StaffID.Text);
            Command.Parameters.AddWithValue("@StaffName",txt_StaffName.Text);
            Command.Parameters.AddWithValue("@StaffPhone",txt_StaffPhoneNumber.Text);
            Command.Parameters.AddWithValue("@StaffGender", comboBox1.SelectedItem.ToString());
            Command.Parameters.AddWithValue("@StaffPassword", txt_StaffPassword.Text);


            Command.ExecuteNonQuery();
            MessageBox.Show("Staff Added Successfully!!!");
            con.Close();
            populate();
        }
    }
}
