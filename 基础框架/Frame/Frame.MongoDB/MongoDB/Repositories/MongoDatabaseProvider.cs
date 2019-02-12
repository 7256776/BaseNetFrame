using Abp.Dependency;
using MongoDB.Driver;

namespace Frame.MongoDB
{
    /// <summary>
    /// 
    /// </summary>
    public class MongoDatabaseProvider : IMongoDatabaseProvider, ITransientDependency
    {
        private readonly IFrameMongoDBConfiguration _frameMongoDBConfiguration;
        
        public MongoDatabaseProvider(IFrameMongoDBConfiguration frameMongoDBConfiguration)
        {
            _frameMongoDBConfiguration = frameMongoDBConfiguration;
        }

        /// <summary>
        /// MongoDB数据库对象
        /// </summary>
        public IMongoDatabase Database {
            get
            {
                #region 创建客户端设置
                /*   
                //创建凭证
                var credential = MongoCredential.CreateCredential("", "", "");
                  credential = MongoCredential.CreateGssapiCredential("");
                  credential = MongoCredential.CreateMongoX509Credential("");
                  credential = MongoCredential.CreatePlainCredential("", "", "");
                var settings = new MongoClientSettings
                {
                    Credential= credential,
                    MaxConnectionPoolSize = 15000,//设定最大连接池
                    WaitQueueSize = 500,//设定等待队列数

                    Server = new MongoServerAddress("连接IP", Convert.ToInt32("端口号"))
                };
                IMongoClient mongoClient = new MongoClient(settings);
                return mongoClient.GetDatabase("MongoDB库名称");
                 */
                #endregion

                //创建客户端设置
                IMongoClient mongoClient = new MongoClient(_frameMongoDBConfiguration.ConnectionString);
                return mongoClient.GetDatabase(_frameMongoDBConfiguration.DatatabaseName);
            }
        }



    }
}
