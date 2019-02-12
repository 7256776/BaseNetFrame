using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Jurassic.Library.Application;
using Newtonsoft.Json;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jurassic.Library.MongoDB;
using MongoDB.Driver;
using System.Linq;

namespace Jurassic.Library.Sample
{
    /// <summary>
    /// 用户基本信息扩展 实现接口 IUserInfoExtens 可以完成登录验证的后台业务重写
    /// </summary>
    public class userinfo : Entity<ObjectId>
    {
        public userinfo()
        {
        }

        /// <summary>构造函数</summary>
        public userinfo(IDictionary<string, object> values) //: base(values)
        {
        }

        //public ObjectId id { get; set; }
        // [JsonIgnore] 设置该标签表示转换json对象时候忽略该属性
        public string name { get; set; }
        public string sex { get; set; }
        public string msg { get; set; }
        public DateTime? create { get; set; }
    }



    public interface IUserInfoManager : IRepository<userinfo, ObjectId>
    {
        userinfo GetUser(ObjectId id);
    }

    public class UserInfoManager : MongoDBRepositoryBase<userinfo, ObjectId>, IUserInfoManager
    {
        public UserInfoManager(IMongoDatabaseProvider databaseProvider):base(databaseProvider)
        {

        }

        public void DoFu(userinfo u)
        {

        }

        public  userinfo GetUser(ObjectId id)
        {
            #region 
            //var query = Builders<userinfo>.Filter.Eq(e => e.msg, "消息1");
            //var entity = Collection.Find(query).First();
            //var entity1 = Collection.Find(query).FirstOrDefault();

            //通过id查询
            //var model1 = base.Get(id);
            //var model = base.GetAsync(id);
            //var model2 = base.Load(id);
            //测试
            //var model3 = Abp.Threading.AsyncHelper.RunSync(() => base.GetAsync(id));

            //查询数据条数
            //var count1 = base.CountAsync();
            //var count = base.Count(e => e.msg == "消息");
            //var count2 = Abp.Threading.AsyncHelper.RunSync(() => base.CountAsync());
            //var count3 = Abp.Threading.AsyncHelper.RunSync(() => base.CountAsync(e => e.msg == "消息"));

            //查询默认第一个
            //var model1 = base.FirstOrDefault(id);
            //var model4 = base.FirstOrDefault(e => e.msg == "消息");
            //var model2 = base.FirstOrDefaultAsync(id);
            //var model3 = base.FirstOrDefaultAsync(e => e.msg == "消息");
            //var model3 = Abp.Threading.AsyncHelper.RunSync(() => base.FirstOrDefaultAsync(e => e.msg == "消息"));

            //查询所有
            //var qList = base.GetAll().Where(e => e.msg == "消息");
            //var qList1 = qList.ToList();
            //var qList1 = base.GetAllIncluding().ToList();
            //base.GetAllList();
            //base.GetAllListAsync();

            #endregion

            var query = Builders<BsonDocument>.Filter.Eq("msg", "消息");
            var aaas3 = base.Database.GetCollection<BsonDocument>("userinfo").Find(query);
            var dics = aaas3.ToList().Select(bs =>
            {
                var dict = bs.ToDictionary();
                //dict.Remove("_id");
                return dict;
            }).ToList();


            var a = base.GetAll();
            var asas = a.ToJson();
            var sa = a.ToList();
            var aa = base.GetAllList(p => p.msg == "消息");

            //新增
            //ObjectId iid = ObjectId.GenerateNewId();
            userinfo user = new userinfo()
            {
                Id = ObjectId.Parse("5b6a53e53f38401d74da3440"),
                name = "名称" + Guid.NewGuid().ToString("D").Substring(0, 5),
                sex = "性123123别",
                create = DateTime.Now
            };



            //base.Insert(user);
            //var obId = base.InsertAndGetId(user);
            //base.InsertAndGetIdAsync(user);
            //var model3 = Abp.Threading.AsyncHelper.RunSync(() => base.InsertAndGetIdAsync(user));
            //base.InsertAsync(user);
            //ObjectId iid = ObjectId.Parse("5b692efe9f660d0b582a4232");
            //base.Update(iid, DoFu);
            //base.InsertOrUpdate(user);
            //base.InsertOrUpdateAsync();
            //ObjectId ss = base.InsertOrUpdateAndGetId(user);
            //base.InsertOrUpdateAndGetIdAsync();

            //base.Update(user);
            //base.UpdateAsync();
            //var model3 = Abp.Threading.AsyncHelper.RunSync(() => base.UpdateAsync(user));


            //base.Delete(d => d.sex == "性别" );
            //base.DeleteAsync();

            //base.Single();
            //base.SingleAsync();




            //var fff = Builders<userinfo>.Filter.Eq("id","");
            //base.Collection.UpdateOne(fff, update);
            return null;
        }

    }






}
