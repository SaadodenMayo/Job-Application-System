using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Job_application_C_
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private async void btnCreateAccount_Click(object sender, EventArgs e)
        {
            var userData = new
            {
                username = txtCreateUsername.Text,
                password = txtCreatePassword.Text,
                email = "" // Pwede nimo pun-an og txtEmail kung gusto ka
            };

            string json = JsonConvert.SerializeObject(userData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsync("http://localhost:3000/api/register", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Account Created!");
                    // Balhin balik sa Login Form
                    this.Hide();
                    Form1 login = new Form1();
                    login.Show();
                }
            }
        }

        private void checkboxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtCreatePassword.UseSystemPasswordChar = !checkboxShowPassword.Checked;
        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }
    }
}
