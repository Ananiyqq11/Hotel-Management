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
    public partial class Form_Login : Form
    {
        public Form_Login()
        {
            InitializeComponent();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool validateinput()
        {
            bool valid = true;
            if (txt_username.Text.Equals(string.Empty))
            {
                valid = false;
                txt_username.Focus();
                errorProviderforuserinput.SetError(txt_username, "Invalid ENTRY, Please Enter username");
            }
            if (txt_password.Text.Equals(string.Empty))
            {
                valid = false;
                txt_password.Focus();
                errorProviderforuserinput.SetError(txt_password, "Invalid ENTRY, Please Enter password");
            }
            return valid;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {

            validateinput();

            if (txt_username.Text == "admin" && txt_password.Text == "admin")
            {
                Form_AdminPage admin = new Form_AdminPage("admin");
                admin.Show();
                this.Hide();
            }
            else if (txt_username.Text == "reception" && txt_password.Text == "reception")
            {
                Form_ReceptionInfo reception = new Form_ReceptionInfo("reception");
                reception.Show();
                this.Hide();
            }
            else if (txt_username.Text == "staff" && txt_password.Text == "staff")
            {
                Form_StaffInfo staff = new Form_StaffInfo("staff");
                staff.Show();
                this.Hide();
            }
        }
    }
}
