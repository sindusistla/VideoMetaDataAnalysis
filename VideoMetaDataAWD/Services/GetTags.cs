using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoMetaDataAWD.Models;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using System.Xml.XPath;
using Newtonsoft.Json.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;

namespace VideoMetaDataAWD.Services
{
    public class GetTags
    {
        //
        public VideoData GetAllTags(string ID) {

            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            List<MetaTag> Tags = new List<MetaTag>();
            var VideoData = new VideoData();
            

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "39dc76f4c2ec4fb38831bb1fe2de6d9e");

            // Request parameters
            queryString["language"] = "en-US";
            var uri = "https://videobreakdown.azure-api.net/Breakdowns/Api/Partner/Breakdowns/08da529a3f";
            var ConnectionString = "mongodb://aditi:swlp@ds161175.mlab.com:61175/tagdatabase";
            var Mongoclient = new MongoClient(ConnectionString);
            var db = Mongoclient.GetDatabase("tagdatabase");
            var vidCol = db.GetCollection<BsonDocument>("videoCollection");
            var tagCol = db.GetCollection<BsonDocument>("tagCollection");

            var response = client.GetAsync(uri).Result;

            var json = response.Content.ReadAsStringAsync().Result;


            JToken json_obj = JObject.Parse(json);

            // Retrieve VideoID VideoData.VideoId =
            VideoData.VideoId = (string)json_obj.SelectToken("id");
            VideoData.VideoName= (string)json_obj.SelectToken("name");
            JObject firstItemSnippet = JObject.Parse(json_obj["summarizedInsights"].ToString());
            var topics = (JArray)firstItemSnippet.SelectToken("topics");

          
            // var list = new List<>();
            foreach (var item in topics)
            {
                var Tag = new MetaTag();
                Tag.tagName = item["name"].ToString();
                var appears = item["appearances"];
                List<Appears> AppearList = new List<Appears>();
                foreach (var app in appears)
                {
                    var Appearance = new Appears();
                    Appearance.startTime = app["startSeconds"].ToString();
                    Appearance.endTime = app["endSeconds"].ToString();
                    AppearList.Add(Appearance);
                }
                Tag.Appearances = AppearList;

                Tags.Add(Tag);               
            }

            VideoData.Tags = Tags;

            BsonDocument document = VideoData.ToBsonDocument();
           
            vidCol.InsertOne(document);
            Console.WriteLine(VideoData);
            return VideoData;
        }



    }
}