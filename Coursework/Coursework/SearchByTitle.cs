using System;
using System.Windows.Forms;

namespace Coursework
{
    public partial class SearchByTitle : Form
    {
        private string movieToSearch;

        public SearchByTitle()
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

        private void Button3_Click(object sender, EventArgs e) { MessageBox.Show("Please either enter a movie title to search for, or select an example movie. Please put the exact title, otherwise it may not find the movie you wanted."); }

        private void Button4_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Are you sure you want to exit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.Yes) Application.Exit();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            movieToSearch = textBox1.Text;
            if (String.IsNullOrWhiteSpace(movieToSearch))
            {
                MessageBox.Show("Enter a value to search!");
            }
            else
            {
                movieToSearch = textBox1.Text;
                ResultsViewer();
            }
        }

        private void PictureBox6_Click(object sender, EventArgs e)
        {
            movieToSearch = "The Dark Knight";
            ResultsViewer();
        }

        private void PictureBox7_Click(object sender, EventArgs e)
        {
            movieToSearch = "John Wick";
            ResultsViewer();
        }

        private void ResultsViewer()
        {
            ResultsViewer resultsViewer = new ResultsViewer();
            resultsViewer.SearchByTitle(movieToSearch);
            resultsViewer.Show();
            this.Hide();
        }
    }
}