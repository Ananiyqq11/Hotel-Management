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
            if (MessageBox.Show("Are you sure you want to delete ReceptionID " + int.Parse(txt_ReceptionID.Text) + " ?", "Delete", MessageBoxButtons.YesNo).Equals(DialogResult.Yes))
            {

                SqlCommand Command = new SqlCommand("delete from Reception where @ReceptID=ReceptID", con);
                Command.Parameters.AddWithValue("@ReceptID", txt_ReceptionID.Text);

                Command.ExecuteNonQuery();
                MessageBox.Show("Reception Deleted Successfully!!!");
                con.Close();
                populate();
            }
    }

    private void label_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_Search_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("exec dbo.[search Client]'" + int.Parse(txt_ReceptionID.Text) + "'", con);
            SqlDataAdapter adpter = new SqlDataAdapter(cmd);
            DataTable datble = new DataTable();
            adpter.Fill(datble);
            dataGridView1.DataSource = datble;
        }

        private void label_Edit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("[update Reception]'" + int.Parse(txt_ReceptionID.Text) + "','" + txt_ReceptionName.Text.ToString() + "','" + txt_ReceptionPhoneNumber.Text.ToString() + "','" + comboBox1.Text.ToString() +"',"+comboBox1.Text.ToString()+"'"+txt_ReceptionAddress.Text.ToString()+"','"+txt_ReceptionPassword.Text.ToString()+"'" + con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Client Updated Successfully");
            populate();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
               txt_ReceptionID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_ReceptionName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_ReceptionPhoneNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txt_ReceptionAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txt_ReceptionPassword.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
        }
    }
}
