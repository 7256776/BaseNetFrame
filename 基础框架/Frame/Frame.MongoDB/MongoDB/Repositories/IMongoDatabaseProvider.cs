using MongoDB.Driver;

namespace Frame.MongoDB
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMongoDatabaseProvider
    {
        IMongoDatabase Database { get; }
     
    }

}
