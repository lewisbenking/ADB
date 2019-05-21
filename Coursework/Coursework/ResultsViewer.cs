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
            mongoClient = new MongoClient("mongodb+srv://kingl:jF3QaPw9KYDh17xu@adb-lk-cluster-no3pl.mongodb.net/test?retryWrites=true");
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
            SingleMovieHandler(movieList);
        }

        public void SearchRandomMovie()
        {
            movieList = collection.AsQueryable().Sample(1);
            SingleMovieHandler(movieList);
        }

        public void SearchTopRated()
        {
            panel2.Show();
            var imdbTopRated = (from item in collection.AsQueryable() where item.imdb.rating >= 8.0 orderby item.imdb.rating descending select item).Take(3);
            var metacriticTopRated = (from item in collection.AsQueryable() where item.metacritic >= 80 orderby item.metacritic descending select item).Take(3);
            var rottenTomatoesTopRated = (from item in collection.AsQueryable() where item.tomatoes.critic.meter >= 80 && item.tomatoes.viewer.meter >= 80 orderby item.tomatoes.viewer.rating descending select item).Take(3);
            TopRatedHandler(imdbTopRated, metacriticTopRated, rottenTomatoesTopRated);
        }

        private void TopRatedHandler(IMongoQueryable<Models.Model> imdb, IMongoQueryable<Models.Model> metacritic, IMongoQueryable<Models.Model> rottenTomatoes)
        {
            var imdbList = imdb.ToList();
            labelIMDB1.Text = imdbList[0].title.ToString();
            labelIMDB2.Text = imdbList[1].title.ToString();
            labelIMDB3.Text = imdbList[2].title.ToString();

            var metacriticList = metacritic.ToList();
            labelMetacritic1.Text = metacriticList[0].title.ToString();
            labelMetacritic2.Text = metacriticList[1].title.ToString();
            labelMetacritic3.Text = metacriticList[2].title.ToString();

            var rtList = rottenTomatoes.ToList();
            labelRT1.Text = rtList[0].title.ToString();
            labelRT2.Text = rtList[1].title.ToString();
            labelRT3.Text = rtList[2].title.ToString();
        }

        private void SingleMovieHandler(IMongoQueryable<Models.Model> movieList)
        {
            panel2.Visible = false;
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

        private void LabelIMDB1_Click(object sender, EventArgs e) { SearchByTitle(labelIMDB1.Text); }
        private void LabelIMDB2_Click(object sender, EventArgs e) { SearchByTitle(labelIMDB2.Text); }
        private void LabelIMDB3_Click(object sender, EventArgs e) { SearchByTitle(labelIMDB3.Text); }

        private void LabelMetacritic1_Click(object sender, EventArgs e) { SearchByTitle(labelMetacritic1.Text); }
        private void LabelMetacritic2_Click(object sender, EventArgs e) { SearchByTitle(labelMetacritic2.Text); }
        private void LabelMetacritic3_Click(object sender, EventArgs e) { SearchByTitle(labelMetacritic3.Text); }

        private void LabelRT1_Click(object sender, EventArgs e) { SearchByTitle(labelRT1.Text); }
        private void LabelRT2_Click(object sender, EventArgs e) { SearchByTitle(labelRT2.Text); }
        private void LabelRT3_Click(object sender, EventArgs e) { SearchByTitle(labelRT3.Text); }
    }
}