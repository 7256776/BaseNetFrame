namespace Frame.MongoDB
{
    public interface IFrameMongoDBConfiguration
    {
        string ConnectionString { get; set; }

        string DatatabaseName { get; set; }

    }
}
