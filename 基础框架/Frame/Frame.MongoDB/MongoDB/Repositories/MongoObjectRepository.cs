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

    public class MongoObjectRepository : MongoDBRepositoryBase<ObjectEntity, ObjectId>, IMongoObjectRepository
    {
        public MongoObjectRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {

        }

        public async Task MongoObjectAdd(string collectionName, JObject data)
        {
            var dict = data.ToDictionary();
            await base.Database.GetCollection<BsonDocument>(collectionName).InsertOneAsync(new BsonDocument(dict));
        }


        public async Task MongoObjectEdit(string collectionName, MongoUpdateModel data)
        {
            var dict = data.UpdataJson.ToDictionary();
            var query = new BsonDocument(data.FilterJson.ToDictionary());
            await base.Database.GetCollection<BsonDocument>(collectionName).UpdateOneAsync(query, new BsonDocument(dict));
        }

        public async Task MongoObjectReplace(string collectionName, MongoUpdateModel data)
        {
            var dict = data.UpdataJson.ToDictionary();
            var query = new BsonDocument(data.FilterJson.ToDictionary());
            await base.Database.GetCollection<BsonDocument>(collectionName).ReplaceOneAsync(query, new BsonDocument(dict));
        }

        public async Task MongoObjectDel(string collectionName, ObjectId delId)
        {
            Dictionary<string, object> para = new Dictionary<string, object>();
            para.Add("_id", delId);
            var query = new BsonDocument(para);
            await base.Database.GetCollection<BsonDocument>(collectionName).DeleteOneAsync(query);
        }


        public async Task<List<BsonDocument>> MongoObjectFind(string collectionName, JObject data)
        {
            var query = new BsonDocument(data.ToDictionary());

            var result = await base.Database.GetCollection<BsonDocument>(collectionName).FindAsync(query);
            return result.ToList();
        }
    }

    public class MongoRepository<TEntity> : MongoDBRepositoryBase<TEntity, ObjectId>, IMongoRepository<TEntity>
         where TEntity : class, IEntity<ObjectId>
    {
        public MongoRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {

        }
    }
}
