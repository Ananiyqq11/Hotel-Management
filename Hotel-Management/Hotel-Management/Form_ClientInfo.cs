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
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Hotel_Management
{
    public partial class Form_ClientInfo : Form
    {
        public Form_ClientInfo(string user)
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
            string query = "select * from Client";
            SqlDataAdapter adapter = new SqlDataAdapter(query,con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
           
        }
        private bool validateinput()
        {
            Regex regexphoneETH = new Regex("^[+][0-9]{11}$");
            Regex regexphoneKen = new Regex("^[+][0-9]{10}$");
            Regex regexphoneGha = new Regex("^[+][0-9]{8}$");
            Regex regexphoneEgt = new Regex("^[+][0-9]{7}$");
            Regex regexphoneIsr = new Regex("^[+][0-9]{10}$");
            Regex regexphoneChi = new Regex("^[+][0-9]{12}$");
            Regex regexphoneUs = new Regex("^[+][0-9]{10}$");
            Regex regexphoneCan = new Regex("^[+][0-9]{10}$");
            Regex regexphoneUk = new Regex("^[+][0-9]{9}$");
            bool valid = true;
            if (txt_ClientPhoneNumber.Text.Equals(string.Empty) || 
                regexphoneETH.IsMatch(txt_ClientPhoneNumber.Text)||
                regexphoneKen.IsMatch(txt_ClientPhoneNumber.Text) ||
                regexphoneGha.IsMatch(txt_ClientPhoneNumber.Text) ||
                regexphoneEgt.IsMatch(txt_ClientPhoneNumber.Text) ||
                regexphoneIsr.IsMatch(txt_ClientPhoneNumber.Text) ||
                regexphoneChi.IsMatch(txt_ClientPhoneNumber.Text) ||
                regexphoneUs.IsMatch(txt_ClientPhoneNumber.Text) ||
                regexphoneCan.IsMatch(txt_ClientPhoneNumber.Text) ||
                regexphoneUk.IsMatch(txt_ClientPhoneNumber.Text) 
                )
         //country digits   //kenya 10 // ghana 8  //egypt 7 //israel 10 //china 11  //us 10 //canada 10 //uk 9
            {
                valid = false;
                txt_ClientPhoneNumber.Focus();
                errorProviderforclient.SetError(txt_ClientPhoneNumber, "Invalid ENTRY, Please Enter valid phone number ");
            }
            if (txt_ClientID.Text.Equals(string.Empty))
            {
                valid = false;
                txt_ClientID.Focus();
                errorProviderforclient.SetError(txt_ClientID, "Invalid ENTRY, Please Enter client id ");
            }
            if (txt_ClientName.Text.Equals(string.Empty))
            {
                valid = false;
                txt_ClientID.Focus();
                errorProviderforclient.SetError(txt_ClientName, "Invalid ENTRY, Please Enter your name ");
            }
            if (comboBox1.SelectedItem==null)
            {
                valid = false;
                comboBox1.Focus();
                errorProviderforclient.SetError(comboBox1, "Invalid ENTRY, Please Enter Among the countries");
            }
            return valid;
        }
        public void demo()
        {
            if (comboBox1.SelectedItem.ToString().Equals("Ethiopia"))
            {
                txt_ClientPhoneNumber.Text = "+251";
            }
            if (comboBox1.SelectedItem.ToString().Equals("Kenya"))
            {
                txt_ClientPhoneNumber.Text = "+254";
            }
            if (comboBox1.SelectedItem.ToString().Equals("Ghana"))
            {
                txt_ClientPhoneNumber.Text = "+233";
            }
            if (comboBox1.SelectedItem.ToString().Equals("Egypt"))
            {
                txt_ClientPhoneNumber.Text = "+20";
            }
            if (comboBox1.SelectedItem.ToString().Equals("Israel"))
            {
                txt_ClientPhoneNumber.Text = "+972";
            }
            if (comboBox1.SelectedItem.ToString().Equals("China"))
            {
                txt_ClientPhoneNumber.Text = "+86";
            }
            if (comboBox1.SelectedItem.ToString().Equals("United States"))
            {
                txt_ClientPhoneNumber.Text = "+1";
            }
            if (comboBox1.SelectedItem.ToString().Equals("Canada"))
            {
                txt_ClientPhoneNumber.Text = "+1";
            }
            if (comboBox1.SelectedItem.ToString().Equals("United Kingdom"))
            {
                txt_ClientPhoneNumber.Text = "+44";
            }
        }
        private void label_BackToLogin_Click(object sender, EventArgs e)
        {
            Form_AdminPage login = new Form_AdminPage(label1.Text);
            login.Show();
            this.Hide();
        }

        private void label_Add_Click(object sender, EventArgs e)
        {
            if (validateinput())
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
        }

        private void label_Delete_Click(object sender, EventArgs e)
        {
            if (validateinput())
            {
                SqlConnection con = new SqlConnection(constring);
                con.Open();
                if (MessageBox.Show("Are you sure you want to delete ClientID " + int.Parse(txt_ClientID.Text) + " ?", "Delete", MessageBoxButtons.YesNo).Equals(DialogResult.Yes))
                {

                    SqlCommand Command = new SqlCommand("exec dbo.[delete Client]'" + int.Parse(txt_ClientID.Text) + "'", con);

                    Command.ExecuteNonQuery();
                    MessageBox.Show("Client Deleted Successfully!!!");
                    con.Close();
                    populate();
                }
            }
        }

        private void label_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            demo();
        }

        private void label_Edit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("[update Client]'" + int.Parse(txt_ClientID.Text)+"','"+txt_ClientName.Text.ToString()+"','"+ txt_ClientPhoneNumber.Text.ToString() + "','"+comboBox1.Text.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Client Updated Successfully");
            populate();
        }

        private void label_Search_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("exec dbo.[search Client]'" + int.Parse(txt_ClientID.Text) + "'", con);
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
                txt_ClientID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
               txt_ClientName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
               txt_ClientPhoneNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
               comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
        }
    }
}
