using Frame.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frame.Sample
{

    public class SampleRepository : MongoDBRepositoryBase<SampleTable, ObjectId>, ISampleRepository
    {
        public SampleRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {

        }


        public async Task MongoObjectAdd(JObject data)
        {
            var dict = data.ToDictionary();
            await base.Database.GetCollection<BsonDocument>("CustomTable").InsertOneAsync(new BsonDocument(dict));
        }


        public async Task MongoObjectEdit(MongoUpdateModel data)
        {
            //更新语法必须加$set
            JObject update = new JObject();
            update.Add(new JProperty("$set", data.UpdataJson));
            var dict = update.ToDictionary();

            var query = new BsonDocument(data.FilterJson.ToDictionary());
            //更新所有匹配的记录
            await base.Database.GetCollection<BsonDocument>("CustomTable").UpdateManyAsync(query, new BsonDocument(dict));
            //更新一条匹配到的记录
            //await base.Database.GetCollection<BsonDocument>("CustomTable").UpdateOneAsync(query, new BsonDocument(dict));
        }

        public async Task MongoObjectReplace(MongoUpdateModel data)
        {
            var dict = data.UpdataJson.ToDictionary();
            var query = new BsonDocument(data.FilterJson.ToDictionary());
            await base.Database.GetCollection<BsonDocument>("CustomTable").ReplaceOneAsync(query, new BsonDocument(dict));
        }

        public async Task MongoObjectDel(JObject data)
        {
            var query = new BsonDocument(data.ToDictionary());
            await base.Database.GetCollection<BsonDocument>("CustomTable").DeleteOneAsync(query);
        }


        public async Task<List<BsonDocument>> MongoObjectFind(JObject data)
        {
            var query = new BsonDocument(data.ToDictionary());

            var result = await base.Database.GetCollection<BsonDocument>("CustomTable").FindAsync(query);
            return result.ToList();
        }

    }
}
