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
        private string movieInJson = "", movieToSearch, actorToSearch;
        private MongoClient mongoClient;
        private IMongoDatabase mongoDatabase;
        private IMongoCollection<Models.Model> collection;
        private IMongoQueryable<Models.Model> exactInputMatchModel, partialInputMatchModel, imdbTopRatedModel, metacriticTopRatedModel, rottenTomatoesTopRatedModel;
        private System.Collections.Generic.List<Models.Model> model; 

        public ResultsViewer()
        {
            InitializeComponent();
            panel2.Visible = false; panel7.Visible = false; panel8.Visible = false; panel9.Visible = false;
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
            if (panel1.Visible == true) { MessageBox.Show("Unfortunately we don't recognise your input. Please try again. Please note we may not have what you are looking for on our dataset."); }
            else if (panel2.Visible == true) { MessageBox.Show("Here are the top rated movies in our dataset from IMDB, Metacritic and Rotten Tomatoes. You can select any of the films below to view more details. For Rotten Tomatoes: C = Critic Rating, and V = Viewer Rating"); }
            else { MessageBox.Show("Here are the details of the movie, due to spacing we can't show you the complete details from our dataset. If you would like to view the full details, you can save the complete result to a file."); }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Are you sure you want to exit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.Yes) Application.Exit();
        }

        private void Button5_Click(object sender, EventArgs e) { WriteToTextFile(); }


        private void LabelActorFilm1_Click(object sender, EventArgs e) { SearchByTitleAndYear(labelActorFilm1.Text, labelActorFilm1Year.Text, labelActorName.Text); }
        private void LabelActorFilm2_Click(object sender, EventArgs e) { SearchByTitleAndYear(labelActorFilm2.Text, labelActorFilm2Year.Text, labelActorName.Text); }
        private void LabelActorFilm3_Click(object sender, EventArgs e) { SearchByTitleAndYear(labelActorFilm3.Text, labelActorFilm3Year.Text, labelActorName.Text); }
        private void LabelActorFilm4_Click(object sender, EventArgs e) { SearchByTitleAndYear(labelActorFilm4.Text, labelActorFilm4Year.Text, labelActorName.Text); }
        private void LabelActorFilm5_Click(object sender, EventArgs e) { SearchByTitleAndYear(labelActorFilm5.Text, labelActorFilm5Year.Text, labelActorName.Text); }

        private void LabelFilmMatch1_Click(object sender, EventArgs e) { SearchByTitleAndYear(labelFilmMatch1.Text, labelFilmMatch1Year.Text, ""); }
        private void LabelFilmMatch2_Click(object sender, EventArgs e) { SearchByTitleAndYear(labelFilmMatch2.Text, labelFilmMatch2Year.Text, ""); }

        private void LabelFilm1IMDB_Click(object sender, EventArgs e) { SearchByTitle(labelFilm1IMDB.Text); }
        private void LabelFilm2IMDB_Click(object sender, EventArgs e) { SearchByTitle(labelFilm2IMDB.Text); }
        private void LabelFilm3IMDB_Click(object sender, EventArgs e) { SearchByTitle(labelFilm3IMDB.Text); }

        private void LabelFilm1MC_Click(object sender, EventArgs e) { SearchByTitle(labelFilm1MC.Text); }
        private void LabelFilm2MC_Click(object sender, EventArgs e) { SearchByTitle(labelFilm2MC.Text); }
        private void LabelFilm3MC_Click(object sender, EventArgs e) { SearchByTitle(labelFilm3MC.Text); }

        private void LabelFilm1RT_Click(object sender, EventArgs e) { SearchByTitle(labelFilm1RT.Text); }
        private void LabelFilm2RT_Click(object sender, EventArgs e) { SearchByTitle(labelFilm2RT.Text); }
        private void LabelFilm3RT_Click(object sender, EventArgs e) { SearchByTitle(labelFilm3RT.Text); }

        private void LabelFilmMatch3_Click(object sender, EventArgs e) { SearchByTitleAndYear(labelFilmMatch3.Text, labelFilmMatch3Year.Text, ""); }
        private void LabelFilmMatch4_Click(object sender, EventArgs e) { SearchByTitleAndYear(labelFilmMatch4.Text, labelFilmMatch4Year.Text, ""); }
        private void LabelFilmMatch5_Click(object sender, EventArgs e) { SearchByTitleAndYear(labelFilmMatch5.Text, labelFilmMatch5Year.Text, ""); }

        public void SearchByActor(string actorToSearch)
        {
            this.actorToSearch = actorToSearch;
            exactInputMatchModel = (from item in collection.AsQueryable() where item.cast.Contains(actorToSearch) orderby item.year descending select item).Take(5);
            ActorMovieListHandler(exactInputMatchModel);
        }

        public void SearchByTitle(string movieToSearch)
        {
            this.movieToSearch = movieToSearch;
            exactInputMatchModel = (from item in collection.AsQueryable() where item.title == movieToSearch orderby item.title select item).Take(5);
            if (exactInputMatchModel.Count () == 0) partialInputMatchModel = (from item in collection.AsQueryable() where item.title.Contains(movieToSearch) orderby item.title select item).Take(5);
            ResponseHandler();
        }

        public void SearchByTitleAndYear(string movieToSearch, string yearToSearch, string actorToSearch)
        {
            this.movieToSearch = movieToSearch; this.actorToSearch = actorToSearch;
            if (!String.IsNullOrWhiteSpace(actorToSearch)) exactInputMatchModel = (from item in collection.AsQueryable() where item.title == movieToSearch && item.cast.Contains(actorToSearch) && item.year == int.Parse(yearToSearch) orderby item.title select item).Take(5);
            else exactInputMatchModel = (from item in collection.AsQueryable() where item.title == movieToSearch && item.year == int.Parse(yearToSearch) orderby item.title select item).Take(5);
            ResponseHandler();
        }

        public void SearchRandomMovie()
        {
            exactInputMatchModel = collection.AsQueryable().Sample(1);
            ResponseHandler();
        }

        public void SearchTopRated()
        {
            imdbTopRatedModel = (from item in collection.AsQueryable() where item.imdb.rating >= 8.0 orderby item.imdb.rating descending select item).Take(3);
            metacriticTopRatedModel = (from item in collection.AsQueryable() where item.metacritic >= 80 orderby item.metacritic descending select item).Take(3);
            rottenTomatoesTopRatedModel = (from item in collection.AsQueryable() where item.tomatoes.critic.meter >= 80 && item.tomatoes.viewer.meter >= 80 orderby item.tomatoes.viewer.rating descending select item).Take(3);
            TopRatedHandler(imdbTopRatedModel, metacriticTopRatedModel, rottenTomatoesTopRatedModel);
        }

        private void ActorMovieListHandler(IMongoQueryable<Models.Model> actor)
        {
            model = actor.ToList();
            if (model.Count >= 1)
            {
                panel7.Visible = true;
                labelActorName.Text = actorToSearch;
                labelActorFilm1.Text = model[0].title.ToString(); labelActorFilm1Year.Text = model[0].year.ToString();
                labelActorFilm1.Visible = true; labelActorFilm1Year.Visible = true;
                if (model.Count >= 2)
                {
                    labelActorFilm2.Text = model[1].title.ToString(); labelActorFilm2Year.Text = model[1].year.ToString();
                    labelActorFilm2.Visible = true; labelActorFilm2Year.Visible = true;

                    if (model.Count >= 3)
                    {
                        labelActorFilm3.Text = model[2].title.ToString(); labelActorFilm3Year.Text = model[2].year.ToString();
                        labelActorFilm3.Visible = true; labelActorFilm3Year.Visible = true;

                        if (model.Count >= 4)
                        {
                            labelActorFilm4.Text = model[3].title.ToString(); labelActorFilm4Year.Text = model[3].year.ToString();
                            labelActorFilm4.Visible = true; labelActorFilm4Year.Visible = true;

                            if (model.Count >= 5)
                            {
                                labelActorFilm5.Text = model[4].title.ToString(); labelActorFilm5Year.Text = model[4].year.ToString();
                                labelActorFilm5.Visible = true; labelActorFilm5Year.Visible = true;
                            }
                        }
                    }
                }
            }
            else
            {
                panel1.Visible = true;
            }
        }

        private void TopRatedHandler(IMongoQueryable<Models.Model> imdb, IMongoQueryable<Models.Model> metacritic, IMongoQueryable<Models.Model> rottenTomatoes)
        {
            panel2.Visible = true; panel4.Visible = true;
            panel9.Visible = true;
            model = imdb.ToList();
            labelFilm1IMDB.Text = model[0].title.ToString();
            labelFilm1IMDBRating.Text = model[0].imdb.rating.ToString();
            labelFilm2IMDB.Text = model[1].title.ToString();
            labelFilm2IMDBRating.Text = model[1].imdb.rating.ToString();
            labelFilm3IMDB.Text = model[2].title.ToString();
            labelFilm3IMDBRating.Text = model[2].imdb.rating.ToString();

            model = metacritic.ToList();
            labelFilm1MC.Text = model[0].title.ToString();
            labelFilm1MCRating.Text = model[0].metacritic.ToString();
            labelFilm2MC.Text = model[1].title.ToString();
            labelFilm2MCRating.Text = model[1].metacritic.ToString();
            labelFilm3MC.Text = model[2].title.ToString();
            labelFilm3MCRating.Text = model[2].metacritic.ToString();

            model = rottenTomatoes.ToList();
            labelFilm1RT.Text = model[0].title.ToString();
            labelFilm1RTRating.Text = $"C:{model[0].tomatoes.critic.meter.ToString()}, V:{model[0].tomatoes.viewer.meter.ToString()}";
            labelFilm2RT.Text = model[1].title.ToString();
            labelFilm2RTRating.Text = $"C:{model[1].tomatoes.critic.meter.ToString()}, V:{model[1].tomatoes.viewer.meter.ToString()}";
            labelFilm3RT.Text = model[2].title.ToString();
            labelFilm3RTRating.Text = $"C:{model[2].tomatoes.critic.meter.ToString()}, V:{model[2].tomatoes.viewer.meter.ToString()}";

            Console.WriteLine(labelFilm1IMDB.Text);
        }

        private void ResponseHandler()
        {
            model = exactInputMatchModel.ToList();
            if (model.Count == 0)
            {
                if (partialInputMatchModel != null)
                {
                    if (partialInputMatchModel.Count() == 0) panel1.Visible = true;
                    else MoreThanOneResultHandler(false);
                }
            }
            else
            {
                if (model.Count >= 2)
                {
                    MoreThanOneResultHandler(true);
                }
                else
                {
                    AssignLabelsWithMovieDetails();
                }
            }
        }

        private void MoreThanOneResultHandler(bool exactMatch)
        {
            panel8.Visible = true;
            if (exactMatch) model = exactInputMatchModel.ToList();
            else model = partialInputMatchModel.ToList();
            if (model.Count >= 1)
            {
                panel7.Visible = true;
                labelFilmMatch1.Text = model[0].title.ToString(); labelFilmMatch1Year.Text = model[0].year.ToString();
                labelFilmMatch1.Visible = true; labelFilmMatch1Year.Visible = true;

                if (model.Count >= 2)
                {
                    labelFilmMatch2.Text = model[1].title.ToString(); labelFilmMatch2Year.Text = model[1].year.ToString();
                    labelFilmMatch2.Visible = true; labelFilmMatch2Year.Visible = true;    
                    
                    if (model.Count >= 3)
                    {
                        labelFilmMatch3.Text = model[2].title.ToString(); labelFilmMatch3Year.Text = model[2].year.ToString();
                        labelFilmMatch3.Visible = true; labelFilmMatch3Year.Visible = true;

                        if (model.Count >= 4)
                        {
                            labelFilmMatch4.Text = model[3].title.ToString(); labelFilmMatch4Year.Text = model[3].year.ToString();
                            labelFilmMatch4.Visible = true; labelFilmMatch4Year.Visible = true;

                            if (model.Count >= 5)
                            {
                                labelFilmMatch5.Text = model[4].title.ToString(); labelFilmMatch5Year.Text = model[4].year.ToString();
                                labelFilmMatch5.Visible = true; labelFilmMatch5Year.Visible = true;
                            }
                        }
                    }
                }
            }
            else
            {
                panel1.Visible = true;
            }
        }

        private void AssignLabelsWithMovieDetails()
        {
            model = exactInputMatchModel.ToList();
            panel2.Visible = false; panel7.Visible = false; panel8.Visible = false; panel9.Visible = false;
            Console.WriteLine(model[0].title.ToString());
            richTextBox1.Text = ""; richTextBox2.Text = "";
            labelTitle.Text = model[0].title;
            labelRuntime.Text = model[0].runtime.ToString();
            labelRating.Text = model[0].rated;
            labelGenre.Text = model[0].genres[0].ToString();
            labelYear.Text = model[0].year.ToString();
            foreach (var item in model[0].directors) { richTextBox1.AppendText(item + "\n"); }
            foreach (var item in model[0].cast) { richTextBox2.AppendText(item + "\n"); }
            richTextBox3.Text = model[0].fullplot;
            richTextBox4.Text = $"IMDB:  {model[0].imdb.rating}\nMetacritic: {model[0].metacritic}\nRT Critics: {model[0].tomatoes.critic.meter}\nRT Viewers: {model[0].tomatoes.viewer.meter}";
            movieInJson = JsonConvert.SerializeObject(model);
        }

        private void WriteToTextFile()
        {
            movieInJson = movieInJson.Replace(",\"", ",\n\"");

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