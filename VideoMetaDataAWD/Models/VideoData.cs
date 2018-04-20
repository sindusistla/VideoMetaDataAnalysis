using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoMetaDataAWD.Models
{
    public class Appears
    {
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
    public class MetaTag
    {
        public string tagName { get; set; } 
        public List<Appears> Appearances { get; set; }
    }
    public class VideoData
    {
        public string VideoId { get; set; }
        public string VideoName { get; set; }

        public string VideoURL { get; set; }
        public List<MetaTag> Tags
        {
            get;
            set;
        }
    }
}

//get { return Tags; }
// set { Tags = value; }