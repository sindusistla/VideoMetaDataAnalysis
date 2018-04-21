using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoMetaDataAWD.Models
{
    // Data objects related to Video Meta Data
    public class TagAppearances
        // Tag Appearence Details
    {
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
    public class MetaTag
        // Tag Details 
    {
        public string tagName { get; set; } 
        public List<TagAppearances> Appearances { get; set; }
    }
    public class VideoData
        // Video Details
    {
        public string videoId { get; set; }
        public string videoName { get; set; }

        public string videoURL { get; set; }

        public List<MetaTag> Tags
        {
            get;
            set;
        }
    }
}

//get { return Tags; }
// set { Tags = value; }