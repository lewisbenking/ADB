using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework
{
    public partial class Home : Form
    {
        private string movieToSearch;

        public Home()
        {
            InitializeComponent();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text)) movieToSearch = textBox1.Text;
            ResultsViewer results = new ResultsViewer(movieToSearch);
            results.Show();
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

        private void PictureBox6_Click(object sender, EventArgs e)
        {
            movieToSearch = "The Avengers";
            Button5_Click(sender, e);
        }

        private void PictureBox7_Click(object sender, EventArgs e)
        {
            movieToSearch = "John Wick";
            Button5_Click(sender, e);
        }
    }
}