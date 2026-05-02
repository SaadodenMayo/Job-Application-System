using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;

namespace Job_application_C_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            var loginData = new { username = txtUsername.Text, password = txtPassword.Text };
            string json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync("http://localhost:3000/api/login", content);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        dynamic user = JsonConvert.DeserializeObject(result);

                        // I-save ang data sa session
                        UserSession.UserId = user.user.user_id;
                        UserSession.Username = user.user.username;

                        MessageBox.Show("Welcome " + UserSession.Username);

                        // Balhin sa Dashboard
                        Dashboard dash = new Dashboard();
                        dash.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Username or Password.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void checkboxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !checkboxShowPassword.Checked;
        }

        private void linkCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Signup signup = new Signup();
            signup.Show();
            this.Hide();
        }
    }
}
