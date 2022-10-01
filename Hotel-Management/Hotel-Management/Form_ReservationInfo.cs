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
        public Form_ReservationInfo()
        {
            InitializeComponent();
            populate();
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

        private void label_BackToLogin_Click(object sender, EventArgs e)
        {
            Form_Login login = new Form_Login();
            login.Show();
            this.Hide();
        }
    }
}
