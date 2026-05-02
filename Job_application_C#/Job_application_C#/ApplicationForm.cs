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
    public partial class ApplicationForm : UserControl
    {
        public ApplicationForm()
        {
            InitializeComponent();
        }

        private async void LoadJobsToCombo()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = "http://localhost:3000/api/jobs";
                    var response = await client.GetStringAsync(url);
                    List<Job> jobs = JsonConvert.DeserializeObject<List<Job>>(response);

                    cmbJobTitle.DataSource = jobs;
                    cmbJobTitle.DisplayMember = "job_title"; // Ang makita sa user
                    cmbJobTitle.ValueMember = "job_id";      // Ang ID nga i-save sa DB
                }
                catch (Exception ex) { MessageBox.Show("Error loading jobs: " + ex.Message); }
            }
        }

        private void UC_Apply_Load(object sender, EventArgs e)
        {
            LoadJobsToCombo();
        }

        private async void btnSubmitApplication_Click(object sender, EventArgs e)
        {
            int selectedJobId = (int)cmbJobTitle.SelectedValue;

            var applicationData = new
            {
                user_id = UserSession.UserId, // Gikan sa imong UserSession class
                job_id = selectedJobId,
                full_name = txtFullname.Text,
                email = txtEmail.Text,
                contact_no = txtContact.Text,
                school = txtSchool.Text,
                skills = txtSkills.Text
            };

            string json = JsonConvert.SerializeObject(applicationData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync("http://localhost:3000/api/applicants", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Application submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Failed to submit application.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void ClearForm()
        {
            txtFullname.Clear();
            txtEmail.Clear();
            txtContact.Clear();
            txtSchool.Clear();
            txtSkills.Clear();
        }

        private void btnCancelSubmittion_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}
