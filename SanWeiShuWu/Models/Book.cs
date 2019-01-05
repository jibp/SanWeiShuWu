using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SanWeiShuWu.Models
{
    public class Book
    {

        //[JsonProperty("id")]
        [Key]
        public string Id { get; set; }

        //[JsonProperty("title")]
        public string Title { get; set; }

        //[JsonProperty("subtitle")]
        public string Subtitle { get; set; }

        //[JsonProperty("author")]
        public string Author { get; set; }

       // [JsonProperty("intro")]
        public string Intro { get; set; }

       // [JsonProperty("coverId")]
        public string CoverId { get; set; }

       // [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

       // [JsonProperty("cgs")]
        public string cbs { get; set; }

        //[JsonProperty("cbn")]
        public string cbn { get; set; }

        //[JsonProperty("bben")]
        public string bben { get; set; }

        //[JsonProperty("page")]
        public string page { get; set; }

        //[JsonProperty("filename")]
        public string filename { get; set; }

       // [JsonProperty("tagtype")]
        public int TagType { get; set; }

       // [JsonProperty("isdelete")]
        public int isdelete { get; set; }

       

    }
}
