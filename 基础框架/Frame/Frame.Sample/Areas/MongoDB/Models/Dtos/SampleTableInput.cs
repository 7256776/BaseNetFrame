using Abp.AutoMapper;
using Abp.Domain.Entities;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Frame.MongoDB;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frame.Sample
{
    [AutoMap(typeof(SampleTable))]
    public class SampleTableInput
    {


        //[JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId? Id { get; set; }

        //private object id;
        //public object Id
        //{
        //    get
        //    {
        //        return id;
        //    }
        //    set
        //    {
        //        if (value==null)
        //        {
        //            id = null;
        //            return;
        //        }
        //        ObjectId temId;
        //        ObjectId.TryParse(value.ToString(), out temId);
        //        id = temId;
        //    }
        //}

        public string dataString { get; set; }
        public int? dataNum { get; set; }
        public bool? dataBool { get; set; }
        public DateTime? dataTime { get; set; }
        public List<string> dataArray { get; set; }
        public SampleTableSubInput dataObject { get; set; }

    }

    [AutoMap(typeof(SampleTableSub))]
    public class SampleTableSubInput
    {
        //public ObjectId? Id { get; set; }
        public string dataString { get; set; }
        public int? dataNum { get; set; }
        public bool? dataBool { get; set; }
        public DateTime? dataTime { get; set; }
        public List<string> dataArray { get; set; }

    } 

}