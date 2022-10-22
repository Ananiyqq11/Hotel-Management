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

        private void label_Delete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand Command = new SqlCommand("delete from Reservation where @RoomID=RoomID", con);
            Command.Parameters.AddWithValue("@RoomID", comboBox2.Text.ToString());

            Command.ExecuteNonQuery();
            MessageBox.Show("Reservation Deleted Successfully!!!");
            con.Close();
            populate();
        }

        private void label_Edit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            //  SqlCommand cmd = new SqlCommand("[update Client]'" + comboBox1.Text.ToString() + "','" + comboBox2.Text.ToString() + "','" +dateTimePicker1.Text.ToString() + "','" +dateTimePicker2.Text.ToString()+ "'", con);
            SqlCommand cmd = new SqlCommand("update Reservation set (@ClientName,@RoomID,@DateIn,@DateOut)");
            cmd.Parameters.AddWithValue("@ClientName", comboBox1.Text.ToString());
            cmd.Parameters.AddWithValue("@RoomID", comboBox2.Text.ToString());
            cmd.Parameters.AddWithValue("@DateIn", dateTimePicker1.Text.ToString());
            cmd.Parameters.AddWithValue("@DateOut", dateTimePicker2.Text.ToString());
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Client Updated Successfully");
            populate();
        }

        private void label_Search_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("exec dbo.[search Reservation]'" + int.Parse(comboBox2.Text) + "'", con);
            SqlDataAdapter adpter = new SqlDataAdapter(cmd);
            DataTable datble = new DataTable();
            adpter.Fill(datble);
            dataGridView1.DataSource = datble;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                comboBox1.Text= dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                dateTimePicker2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
               
            }
        }
    }
}
