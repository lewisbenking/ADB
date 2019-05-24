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
        private About about;
        private ResultsViewer resultsViewer;
        private SearchByTitle searchByTitle;
        private SearchForActor searchForActor;

        public Home()
        {
            InitializeComponent();
        }       

        private void Button2_Click(object sender, EventArgs e)
        {
            about = new About();
            about.Show(); this.Hide();
        }

        private void Button3_Click(object sender, EventArgs e) { MessageBox.Show("Please select one of the options to begin!"); }

        private void Button4_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Are you sure you want to exit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.Yes) Application.Exit();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            searchByTitle = new SearchByTitle();
            searchByTitle.Show();
            this.Hide();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            resultsViewer = new ResultsViewer();
            resultsViewer.SearchTopRated();
            resultsViewer.Show();
            this.Hide();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            resultsViewer = new ResultsViewer();
            resultsViewer.SearchRandomMovie();
            resultsViewer.Show();
            this.Hide();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            searchForActor = new SearchForActor();
            searchForActor.Show();
            this.Hide();
        }
    }
}