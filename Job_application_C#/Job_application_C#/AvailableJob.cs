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

        List<Job> allJobs = new List<Job>();

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

                    // 2. Ayaw na pag-declare og 'List<Job>' diri, gamita ang naa sa taas
                    allJobs = JsonConvert.DeserializeObject<List<Job>>(response);

                    dgvJobs.DataSource = null; // Clear sa una
                    dgvJobs.DataSource = allJobs;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // 3. Siguraduha nga kini nga event "Connected" sa Designer Load event
        

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (allJobs == null) return;

            string searchText = txtSearch.Text.ToLower();

            // 4. Ayaw na pag-himo og bag-ong 'allJobs' diri kay ma-empty ang listahan
            var filteredList = allJobs.Where(j =>
                j.job_title.ToLower().Contains(searchText) ||
                j.job_description.ToLower().Contains(searchText)
            ).ToList();

            dgvJobs.DataSource = filteredList;
        }

        private void AvailableJob_Load_1(object sender, EventArgs e)
        {
            LoadAvailableJobs();
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
