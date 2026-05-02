using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Job_application_C_
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            mainpanel.Controls.Clear();
            mainpanel.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Home uc = new Home();
            addUserControl(uc);
        }

        private void btnAvailableJob_Click(object sender, EventArgs e)
        {
             AvailableJob uc = new AvailableJob();
            addUserControl(uc);
        }

        private void btnApplicationForm_Click(object sender, EventArgs e)
        {
            ApplicationForm uc = new ApplicationForm();
            addUserControl(uc);
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?",
                                        "Confirm Logout",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                
                UserSession.UserId = 0;
                UserSession.Username = "";

                Form1 login = new Form1();
                login.Show();

               
                this.Close();
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            btnHome.PerformClick();

            Home uc = new Home();
            addUserControl(uc);

            btnHome.Checked = true;
        }
    }
}
