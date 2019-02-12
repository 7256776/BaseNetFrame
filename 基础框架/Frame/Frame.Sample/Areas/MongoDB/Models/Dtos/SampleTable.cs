using Abp.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frame.Sample
{
    public class SampleTable : Entity<ObjectId>
    {

        public string dataString { get; set; }
        public int? dataNum { get; set; }
        public bool? dataBool { get; set; }
        public DateTime? dataTime { get; set; }
        public List<string> dataArray { get; set; }
        public SampleTableSub dataObject { get; set; }

    }

    public class SampleTableSub 
    {

        public string dataString { get; set; }
        public int? dataNum { get; set; }
        public bool? dataBool { get; set; }
        public DateTime? dataTime { get; set; }
        public List<string> dataArray { get; set; }
        public SampleTable dataObject { get; set; }

    }


}