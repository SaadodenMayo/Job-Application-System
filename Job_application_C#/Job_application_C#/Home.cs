using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Job_application_C_
{
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }


        private void Home_Load(object sender, EventArgs e)
        {
            // Kuhaon ang ngalan gikan sa UserSession class nga atong gihimo
            string username = UserSession.Username;

            // Welcome Message depende sa oras (Optional pero nindot ni)
            string greeting = "";
            int hour = DateTime.Now.Hour;

            if (hour < 12) greeting = "Good Morning";
            else if (hour < 18) greeting = "Good Afternoon";
            else greeting = "Good Evening";

            lblWelcome.Text = $"{greeting}, {username}!";

            lblInstructions.Text = "Welcome aboard! Ready for your next career move?" + Environment.NewLine + Environment.NewLine +
                          "Check out our latest job listings and track your applications easily." + Environment.NewLine +
                          "Just click 'Available Jobs' to begin your journey.";
        }
    }
}
