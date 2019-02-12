using Newtonsoft.Json.Linq;

namespace Frame.MongoDB
{
    public class MongoUpdateModel
    {
        public JObject FilterJson { get; set; }

        public JObject UpdataJson { get; set; }

    }
}
