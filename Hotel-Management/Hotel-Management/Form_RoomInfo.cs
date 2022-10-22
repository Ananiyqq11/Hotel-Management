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
        public Form_RoomInfo(string user)
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
            Form_AdminPage login = new Form_AdminPage(label1.Text);
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
            if (MessageBox.Show("Are you sure you want to delete RoomID " + int.Parse(txt_RoomNumber.Text) + " ?", "Delete", MessageBoxButtons.YesNo).Equals(DialogResult.Yes))
            {
                SqlCommand Command = new SqlCommand("delete from Room where @RoomID=RoomID", con);
                Command.Parameters.AddWithValue("@RoomID", txt_RoomNumber.Text);

                Command.ExecuteNonQuery();
                MessageBox.Show("Room Deleted Successfully!!!");
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
          string roomavail = " ";
            SqlConnection con = new SqlConnection(constring);
            if (radioButton_Yes.Checked == true)
            {
                roomavail = "Free";
            }
            else
            {
                roomavail = "Rented";
            }
            con.Open();
            SqlCommand cmd = new SqlCommand("[update Room]'" + int.Parse(txt_RoomNumber.Text) + "','" + txt_RoomPhoneNumber.Text.ToString() + "','" + roomavail + "'", con);
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
                txt_RoomNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_RoomPhoneNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()=="Rented") {
                    radioButton_No.Checked = true;
                    radioButton_Yes.Checked = false;
                }
                else
                {
                    radioButton_No.Checked = false;
                    radioButton_Yes.Checked = true;
                }
                //if (radioButton_Yes==true)
                //{
                  //  radioButton_Yes.Checked == true
                //} ? "Free" : "Rented"
                //Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void label_Search_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("exec dbo.[search Room]'" + int.Parse(txt_RoomNumber.Text) + "'", con);
            SqlDataAdapter adpter = new SqlDataAdapter(cmd);
            DataTable datble = new DataTable();
            adpter.Fill(datble);
            dataGridView1.DataSource = datble;
        }
    }
}
