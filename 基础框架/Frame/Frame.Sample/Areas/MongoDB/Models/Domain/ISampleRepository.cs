using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Frame.Application;
using Newtonsoft.Json;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frame.MongoDB;
using MongoDB.Driver;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Frame.Sample
{

    public interface ISampleRepository : IRepository<SampleTable, ObjectId>
    {
        Task MongoObjectAdd(JObject data);

        Task MongoObjectEdit(MongoUpdateModel data);

        Task MongoObjectReplace(MongoUpdateModel data);

        Task MongoObjectDel(JObject data);

        Task<List<BsonDocument>> MongoObjectFind(JObject data);

    }


}
