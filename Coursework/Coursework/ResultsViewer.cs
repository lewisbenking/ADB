using MongoDB.Driver;
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
    public partial class ResultsViewer : Form
    {
        MongoInteraction mongo = new MongoInteraction();

        public ResultsViewer(string movieToSearch)
        {
            InitializeComponent();
            MongoDBInteraction(movieToSearch);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Are you sure you want go home? You will have to resubmit your data again.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.Yes)
            {
                Home home = new Home();
                home.Show(); this.Hide();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Are you sure you want go to the about screen? You will have to resubmit your data again.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.Yes)
            {
                About about = new About();
                about.Show(); this.Hide();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }

        private void MongoDBInteraction(string movieToSearch)
        {
            var mongoClient = new MongoClient("mongodb+srv://kingl:REMOVEDPASSWORD@adb-lk-cluster-no3pl.mongodb.net/test?retryWrites=true");
            var mongoDataBase = mongoClient.GetDatabase("sample_mflix");
            var collection = mongoDataBase.GetCollection<Models.Model>("movies");
            var movieList = collection.AsQueryable().Where(model => model.title == movieToSearch);
            foreach (var movie in movieList)
            {
                labelTitle.Text = movie.title;
                labelRuntime.Text = movie.runtime.ToString();
                labelRating.Text = movie.rated;
                foreach (var item in movie.directors) { richTextBox1.AppendText(item + "\n"); }
                foreach (var item in movie.cast) { richTextBox2.AppendText(item + "\n"); }
                richTextBox3.Text = movie.fullplot;
                richTextBox4.Text = $"IMDB:  {movie.imdb.rating}\nMetacritic: {movie.metacritic}\nRT Critics: {movie.tomatoes.critic.meter}\nRT Viewers: {movie.tomatoes.viewer.meter}";
            }
        }
    }
}
