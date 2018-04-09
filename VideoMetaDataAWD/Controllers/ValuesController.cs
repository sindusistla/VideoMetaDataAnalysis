using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using VideoMetaDataAWD.Services;
using VideoMetaDataAWD.Models;

namespace VideoMetaDataAWD.Controllers
{
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

            public VideoData Get(string ID)
            {
                
                return this.VideoMetaDataContainer.GetAllTags(ID);

            }

    }

 }

