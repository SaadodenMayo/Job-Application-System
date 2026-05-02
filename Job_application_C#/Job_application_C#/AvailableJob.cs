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
    public partial class AvailableJob : UserControl
    {
        public AvailableJob()
        {
            InitializeComponent();
        }



        private async void LoadAvailableJobs()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = "http://localhost:3000/api/jobs";
                    var response = await client.GetStringAsync(url);
                    List<Job> allJobs = new List<Job>();

                    // I-save sa global variable
                    allJobs = JsonConvert.DeserializeObject<List<Job>>(response);

                    dgvJobs.DataSource = allJobs;
                    // ... (ang imong formatting sa columns)
                }
                catch (Exception ex) { /* handle error */ }
            }
        }

        private void UC_ViewJobs_Load(object sender, EventArgs e)
        {
            LoadAvailableJobs();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            List<Job> allJobs = new List<Job>();

            // I-filter ang listahan base sa Job Title o Description
            var filteredList = allJobs.Where(j =>
                j.job_title.ToLower().Contains(searchText) ||
                j.job_description.ToLower().Contains(searchText)
            ).ToList();

            // I-update ang DataGridView
            dgvJobs.DataSource = filteredList;
        }
    }

    public class Job
    {
        public int job_id { get; set; }
        public string job_title { get; set; }
        public string job_description { get; set; }
        public string status { get; set; }
    }
}
