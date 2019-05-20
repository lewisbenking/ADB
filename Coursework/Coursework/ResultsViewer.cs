using Newtonsoft.Json;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Coursework
{
    public partial class ResultsViewer : Form
    {
        private string movieInJson = "";
        private MongoClient mongoClient;
        private IMongoDatabase mongoDatabase;
        private IMongoCollection<Models.Model> collection;
        private IMongoQueryable<Models.Model> movieList;

        public ResultsViewer()
        {
            InitializeComponent();
            MongoDBConfiguration();
        }

        private void MongoDBConfiguration()
        {
            mongoClient = new MongoClient("mongodb+srv://kingl:Whey.0127@adb-lk-cluster-no3pl.mongodb.net/test?retryWrites=true");
            mongoDatabase = mongoClient.GetDatabase("sample_mflix");
            collection = mongoDatabase.GetCollection<Models.Model>("movies");
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
            MessageBox.Show("ToDo");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Are you sure you want to exit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.Yes) Application.Exit();
        }

        public void SearchByTitle(string movieToSearch)
        {
            movieList = collection.AsQueryable().Where(model => model.title.ToLower() == movieToSearch.ToLower());
            HandleMongoDBResponse(movieList);
        }

        public void SearchRandomMovie()
        {
            movieList = collection.AsQueryable().Sample(1);
            HandleMongoDBResponse(movieList);
        }

        private void HandleMongoDBResponse(IMongoQueryable<Models.Model> movieList)
        {
            if (movieList.Count() == 0)
            {
                panel1.Visible = true;
            }
            else
            {
                foreach (var movie in movieList)
                {
                    richTextBox1.Text = ""; richTextBox2.Text = "";
                    labelTitle.Text = movie.title;
                    labelRuntime.Text = movie.runtime.ToString();
                    labelRating.Text = movie.rated;
                    labelGenre.Text = movie.genres[0].ToString();
                    foreach (var item in movie.directors) { richTextBox1.AppendText(item + "\n"); }
                    foreach (var item in movie.cast) { richTextBox2.AppendText(item + "\n"); }
                    richTextBox3.Text = movie.fullplot;
                    richTextBox4.Text = $"IMDB:  {movie.imdb.rating}\nMetacritic: {movie.metacritic}\nRT Critics: {movie.tomatoes.critic.meter}\nRT Viewers: {movie.tomatoes.viewer.meter}";
                    movieInJson = JsonConvert.SerializeObject(movie);
                    //WriteToTextFile();
                }
            }
        }


        private void WriteToTextFile()
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(sfd.FileName))
                {
                    writer.Write(movieInJson);
                    writer.Close();
                }
            }
        }
    }
}