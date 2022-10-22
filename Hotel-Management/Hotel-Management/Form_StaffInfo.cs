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
        public Form_StaffInfo(string user)
        {
            InitializeComponent();
            populate();
            label1.Text = user;
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
            Form_AdminPage login = new Form_AdminPage(label1.Text);
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

        private void label_Delete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            if (MessageBox.Show("Are you sure you want to delete StaffID " + int.Parse(txt_StaffID.Text) + " ?", "Delete", MessageBoxButtons.YesNo).Equals(DialogResult.Yes))
            {

                SqlCommand Command = new SqlCommand("exec dbo.[delete Staff]'" + int.Parse(txt_StaffID.Text) + "'", con);
               
                Command.ExecuteNonQuery();
                MessageBox.Show("Staff Deleted Successfully!!!");
                con.Close();
                populate();
            }
        }

        private void label_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_Edit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("[update Client]'" + int.Parse(txt_StaffID.Text) + "','" + txt_StaffName.Text.ToString() + "','" +txt_StaffPhoneNumber.Text.ToString()+"','"+comboBox1.Text.ToString()+"','"+ txt_StaffPassword.Text.ToString() +  "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Client Updated Successfully");
            populate();
        }

        private void label_Search_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("exec dbo.[search Client]'" + int.Parse(txt_StaffID.Text) + "'", con);
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
                txt_StaffID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_StaffName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_StaffPhoneNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_StaffPassword.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
        }
    }
}
