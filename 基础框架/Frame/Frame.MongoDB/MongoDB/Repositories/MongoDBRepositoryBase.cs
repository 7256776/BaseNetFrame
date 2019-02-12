using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Frame.MongoDB
{
    //public class MongoDBRepositoryBase<TEntity> : MongoDBRepositoryBase<TEntity, int>, IRepository<TEntity>
    //   where TEntity : class, IEntity<int>
    //{

    //    public MongoDBRepositoryBase(IMongoDatabaseProvider databaseProvider)
    //        : base(databaseProvider)
    //    {

    //    }
    //}

    public class MongoDBRepositoryBase<TEntity, TPrimaryKey> : AbpRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IMongoDatabaseProvider _databaseProvider;

        public MongoDBRepositoryBase(IMongoDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public virtual IMongoDatabase Database
        {
            get { return _databaseProvider.Database; }
        }

        public virtual IMongoCollection<TEntity> Collection
        {
            get
            {
                //return _databaseProvider.Database.GetCollection<TEntity>(typeof(TEntity).Name);
                return Database.GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }

        public override TEntity Get(TPrimaryKey id)
        {
            var query = Builders<TEntity>.Filter.Eq(e => e.Id, id);
            //返回查询到的第一个结果,如果为空会抛出异常
            //var entity = Collection.Find(query).First();
            //返回查询到的第一个结果,如果为空会返回null
            return Collection.Find(query).FirstOrDefault();
        }

        public override Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return Task.FromResult(Get(id));
        }

        public override TEntity Insert(TEntity entity)
        {
            Collection.InsertOne(entity);
            return entity;
        }

        public override TEntity Update(TEntity entity)
        {
            //转换数据对象,并且移除json对象的Id属性
            var data = JObject.FromObject(entity);
            data.Remove("Id");
            //设置数据对象添加$set修改对象关键字
            JObject update = new JObject();
            update.Add(new JProperty("$set", data));
            //封装数据对象
            var up = new BsonDocument(update.ToDictionary());
            //设置查询对象(默认是通过id查询)
            var query = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);
            //修改单条信息
            var result = Collection.UpdateOne(query, up);
            //返回对象
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public override void Delete(TPrimaryKey id)
        {
            var query = Builders<TEntity>.Filter.Eq(e => e.Id, id);
            var result = Collection.DeleteOne(query);
        }

    }
}

