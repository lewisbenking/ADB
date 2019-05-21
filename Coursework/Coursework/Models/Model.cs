using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Coursework.Models
{
    [BsonIgnoreExtraElements]
    class Model
    {
        public ObjectId _id;
        public DateTime released = DateTime.Now;
        public Awards awards = new Awards();
        public IMDB imdb = new IMDB();
        public RottenTomatoes tomatoes = new RottenTomatoes();
        public int runtime = 0, num_mflix_comments = 0, year = 0, metacritic = 0;
        public List<String> cast = new List<string>(), countries = new List<string>(), directors = new List<string>(), genres = new List<string>(), languages = new List<string>(), writers = new List<string>();
        public string lastupdated = "N/A", plot = "N/A", fullplot = "N/A", rated = "N/A", type = "N/A", poster = "N/A";
        [BsonRepresentation(BsonType.String)]
        public string title = "";

    }

    [BsonIgnoreExtraElements]
    class Awards
    {
        public int wins = 0, nominations = 0;
        public string text = "";
    }

    [BsonIgnoreExtraElements]
    class IMDB
    {
        public double rating = 0.0; public int votes = 0;
        public int id = 0;
    }

    [BsonIgnoreExtraElements]
    class RottenTomatoes
    {
        public RottenTomatoesCritic critic = new RottenTomatoesCritic();
        public RottenTomatoesViewer viewer = new RottenTomatoesViewer();
        public int fresh = 0, rotten = 0;
        public DateTime lastUpdated = DateTime.Now;
    }

    [BsonIgnoreExtraElements]
    class RottenTomatoesCritic
    {
        public double rating = 0.0;
        public int numReviews = 0, meter = 0;
    }

    [BsonIgnoreExtraElements]
    class RottenTomatoesViewer
    {
        public double rating = 0.0;
        public int numReviews = 0, meter = 0;
    }
}