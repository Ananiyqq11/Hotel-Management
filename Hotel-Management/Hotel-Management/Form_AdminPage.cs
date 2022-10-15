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
    public partial class Form_AdminPage : Form
    {
        public Form_AdminPage(string user)
        {
            InitializeComponent();
            if (user != "admin")
            {
                label_Backtologin.Hide();
            }
        }

        private void label_X_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_Client_Click(object sender, EventArgs e)
        {
            Form_ClientInfo client = new Form_ClientInfo();
            client.Show();
            this.Hide();
        }

        private void label_Staff_Click(object sender, EventArgs e)
        {
            Form_StaffInfo staff = new Form_StaffInfo("admin");
            staff.Show();
            this.Hide();
        }

        private void label_Room_Click(object sender, EventArgs e)
        {
            Form_RoomInfo room = new Form_RoomInfo();
            room.Show();
            this.Hide();
        }

        private void label_Reservation_Click(object sender, EventArgs e)
        {
            Form_ReservationInfo reservation = new Form_ReservationInfo();
            reservation.Show();
            this.Hide();
        }

        private void label_Reception_Click(object sender, EventArgs e)
        {
            Form_ReceptionInfo reception = new Form_ReceptionInfo("admin");
            reception.Show();
            this.Hide();
        }

        private void label_Backtologin_Click(object sender, EventArgs e)
        {
            Form_Login a = new Form_Login();
            a.Show();
            this.Hide();
        }
    }
}
