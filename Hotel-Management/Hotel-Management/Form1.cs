using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (txt_username.Text == "admin" && txt_password.Text == "admin")
            {
                Form_AdminPage admin = new Form_AdminPage();
                admin.Show();
                this.Hide();
            }
            else if (txt_username.Text == "reception" && txt_password.Text == "reception")
            {
                Form_ReceptionInfo reception = new Form_ReceptionInfo();
                reception.Show();
                this.Hide();
            }
            else if (txt_username.Text == "staff" && txt_password.Text == "staff")
            {
                Form_StaffInfo staff = new Form_StaffInfo();
                staff.Show();
                this.Hide();
            }
        }
    }
}
