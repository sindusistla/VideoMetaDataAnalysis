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
    [Route("api/ValuesController/{id}")]
    public class ValuesController : ApiController
    {

        // GET api/GetTags/5
        //{id}
           // [Route("api/{controller}/")]
        //int id
            private GetTags VideoMetaDataContainer ;

        
        public ValuesController()
            {
                this.VideoMetaDataContainer = new GetTags();
            }

     
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object Get(string ID)
            {

            object response= this.VideoMetaDataContainer.GetAllTags(ID);
            return new HttpResponseMessage()
            {
                Content = new StringContent((string)response, System.Text.Encoding.UTF8, "application/json")
            };

        }

    }

 }

