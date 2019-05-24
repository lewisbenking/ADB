using System;
using System.Windows.Forms;

namespace Coursework
{
    public partial class SearchForActor : Form
    {
        private string actorToSearch;
        private ResultsViewer resultsViewer;

        public SearchForActor()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show(); this.Hide();
        }

        private void Button3_Click(object sender, EventArgs e) { MessageBox.Show("Please either enter a name to search for, or select one below. Please spell the name correctly so we can find them for you!"); }

        private void Button4_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Are you sure you want to exit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.Yes) Application.Exit();
        }

        private void PictureBox8_Click(object sender, EventArgs e)
        {
            actorToSearch = "Robert Downey Jr.";
            ResultsViewer();
        }

        private void PictureBox9_Click(object sender, EventArgs e)
        {
            actorToSearch = "Keanu Reeves";
            ResultsViewer();
        }

        private void PictureBox10_Click(object sender, EventArgs e)
        {
            actorToSearch = "Sandra Bullock";
            ResultsViewer();
        }

        private void PictureBox12_Click(object sender, EventArgs e)
        {
            actorToSearch = "Emma Watson";
            ResultsViewer();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            actorToSearch = textBox1.Text;
            if (String.IsNullOrWhiteSpace(actorToSearch))
            {
                MessageBox.Show("Enter a value to search!");
            }
            else
            {
                actorToSearch = textBox1.Text;
                ResultsViewer();
            }
        }

        private void ResultsViewer()
        {
            resultsViewer = new ResultsViewer();
            resultsViewer.SearchByActor(actorToSearch);
            resultsViewer.Show();
            this.Hide();
        }
    }
}