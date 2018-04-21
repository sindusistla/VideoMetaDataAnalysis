using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Serialization.Json;
using VideoMetaDataAWD.Services;
using VideoMetaDataAWD.Models;
using Newtonsoft.Json.Linq;
using System.Web.Script.Services;
using System.Web.Services;

namespace VideoMetaDataAWD.Controllers
{
    [ScriptService]
    [Route("api/MetatagsController/{id}")]
    public class MetatagsController : ApiController
    {
        //Name of the Service :MetatagsController
        //About :: Generates tags after analysing the video
        //Api Used : Microsoft API
        //Input : {id}
        //Input type: int
        //Output : Json reponse with VideoID, Video Name, Tags and their respective appearances in the video
        //Output Format: Json
        //Request Example : http://localhost:60279/api/MetatagsController/9a2dab6aa4

        private GetTags metadataContainer ;

        
        public MetatagsController()
            // The controller function which calls the service
            {
                //Calling service to get tags after analysing the video
                this.metadataContainer = new GetTags();
            }

     
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object Get(string ID)
            {

            object response= this.metadataContainer.getallTags(ID);
            return new HttpResponseMessage()
            {
                // Send Json file as response
                Content = new StringContent((string)response, System.Text.Encoding.UTF8, "application/json")
            };

        }

    }

 }

