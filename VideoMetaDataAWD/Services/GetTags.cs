using System;
using System.Collections.Generic;
using System.Web;
using VideoMetaDataAWD.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace VideoMetaDataAWD.Services
{
    public class GetTags
    {
        //
        public object getallTags(string ID) {
            // About the Function 
            //Input :{ Video Id } This video Id is produced by Microsoft API 
            //Output : { Json object with Tags }
            // The response consists of Meta Data retrieved from Microsoft API response 
            // Video Id , Video Name, Tags, Appearences of Tags 
            // Youtube Mapping URL

            // Decalre Variables
            // MongoDb Connection Parameters
            var ConnectionString = "mongodb://aditi:swlp@ds161175.mlab.com:61175/tagdatabase";
            var Mongoclient = new MongoClient(ConnectionString);
            var db = Mongoclient.GetDatabase("tagdatabase");
            var vidCol = db.GetCollection<BsonDocument>("videoCollection");
            var tagCol = db.GetCollection<BsonDocument>("tagCollection");

            // Microsoft API Connnection Parameters
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            List<MetaTag> tags = new List<MetaTag>();
            var VideoData = new VideoData();

            //Check if the video is already been analysed
            var VideoData_DB = vidCol.Find(new BsonDocument() {{"id", ID} });

            // Request parameters
            queryString["language"] = "en-US";
            var uri = "https://videobreakdown.azure-api.net/Breakdowns/Api/Partner/Breakdowns/" + ID;


            // Request headers 
            // Request Microsoft API to get the tags
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "39dc76f4c2ec4fb38831bb1fe2de6d9e");
            var response = client.GetAsync(uri).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            JToken json_obj = JObject.Parse(json);

            // Retrieve VideoID VideoData.VideoId =
            VideoData.videoId = (string)json_obj.SelectToken("id");
            VideoData.videoName = (string)json_obj.SelectToken("name");
            JObject firstItemSnippet = JObject.Parse(json_obj["summarizedInsights"].ToString());
            VideoData.videoURL = getUrl(VideoData.videoId);
            var topics = (JArray)firstItemSnippet.SelectToken("topics");


            // var list = new List<>();
            foreach (var item in topics)
            {
                var Tag = new MetaTag();
                Tag.tagName = item["name"].ToString();
                var appears = item["appearances"];
                List<TagAppearances> AppearList = new List<TagAppearances>();
                foreach (var app in appears)
                {
                    var Appearance = new TagAppearances();
                    Appearance.startTime = app["startSeconds"].ToString();
                    Appearance.endTime = app["endSeconds"].ToString();
                    AppearList.Add(Appearance);
                }
                Tag.Appearances = AppearList;

                tags.Add(Tag);
            }

            VideoData.Tags = tags;
            string json_response = JsonConvert.SerializeObject(VideoData);
            BsonDocument document = VideoData.ToBsonDocument();

            vidCol.InsertOne(document);
            Console.WriteLine(VideoData);

            return json_response;   
   
        }

        public string getUrl(string videoId) {
            // Get Youtube URL using the video ID with respect to Microsoft API Video ID
            // Because of the time constraint we have added few videos related to our domain to the microsoft api due to time constraint.
            // In future we would like to expand the scope by allowing the user to directly upload videos from SWLP platform to microsoft api 
            // The microsoft api <-> youtube url mapping would be handled dynamically
         
            string videoUrl="";

            switch (videoId)
            {
                case "d7c293c3ad":
                    videoUrl= "https://youtu.be/9vQTuwObuWo";
                    break;
                case "9a2dab6aa4":
                    videoUrl = "https://youtu.be/PG3Wgt9IcSQ";
                    break;
                case "e494dce87f":
                    videoUrl = "https://youtu.be/WOhAA0kDtuw";
                    break;
                case "c4c1ad4c9a":
                    videoUrl = "https://youtu.be/DU23gG0geiQ";
                    break;
                case "ed6ede78ad":
                    videoUrl = "https://youtu.be/_HEdTiuESro";
                    break;
                case "f4f476432c":
                    videoUrl = "https://youtu.be/CLoNO-XxNXU";
                    break;
                case "c177fae461":
                    videoUrl = "https://youtu.be/dvFu4EghkDs";
                    break;
                case "07ee39c30b":
                    videoUrl = "https://youtu.be/l_3w1WlwTyQ";
                    break;
                case "52fd94d84c":
                    videoUrl = "https://youtu.be/pfAncSK7LwA";
                    break;

                default:
                    Console.WriteLine("Please upload this video in Microsoft API");
                    break;
            }

            return videoUrl;
        }


    }
}