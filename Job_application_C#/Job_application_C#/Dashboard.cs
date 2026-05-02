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
    }
}
