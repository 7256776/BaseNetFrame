using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Newtonsoft.Json;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jurassic.Library.MongoDB;
using MongoDB.Driver;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Jurassic.Library.MongoDB
{
    public interface IMongoRepository<TEntity> : IRepository<TEntity, ObjectId>
        where TEntity : class, IEntity<ObjectId>
    {

    }

    public interface IMongoObjectRepository : IRepository<ObjectEntity, ObjectId>
    {
        Task MongoObjectAdd(string collectionName, JObject data);

        Task MongoObjectEdit(string collectionName, MongoUpdateModel data);

        Task MongoObjectReplace(string collectionName, MongoUpdateModel data);

        Task MongoObjectDel(string collectionName, ObjectId data);

        Task<List<BsonDocument>> MongoObjectFind(string collectionName, JObject data);

    }
}
