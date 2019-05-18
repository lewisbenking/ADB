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
    class Model
    {
        public ObjectId _id;
        public DateTime released = DateTime.Now;
        public Awards awards;
        public IMDB imdb;
        public RottenTomatoes tomatoes;
        public int runtime = 0, num_mflix_comments = 0, year = 0, metacritic = 0;
        public List<String> cast, countries, directors, genres, languages, writers;
        public string lastupdated = "", plot = "", fullplot = "", rated = "", title = "", type = "", poster = "";
    }
    
    class Awards
    {
        public int wins = 0, nominations = 0;
        public string text = "";
    }

    [BsonIgnoreExtraElements]
    class IMDB
    {
        public double rating = 0.0;
        public int id = 0, votes = 0;
    }

    [BsonIgnoreExtraElements]
    class RottenTomatoes
    {
        public RottenTomatoesCritic critic;
        public RottenTomatoesViewer viewer;
        public int fresh = 0, rotten = 0;
        public DateTime lastUpdated = DateTime.Now;
    }

    class RottenTomatoesCritic
    {
        public double rating = 0.0;
        public int numReviews = 0, meter = 0;
    }

    class RottenTomatoesViewer
    {
        public double rating = 0.0;
        public int numReviews = 0, meter = 0;
    }
}