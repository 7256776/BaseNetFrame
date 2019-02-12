using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Web.Models;
using Frame.Core;
using Frame.MongoDB;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Sample
{
    public interface ISampleAppService : IApplicationService
    {
        #region MongoDB数据实体(自定义对象)

        Task MongoObjectAdd(JObject data);

        Task MongoObjectEdit(MongoUpdateModel data);

        Task MongoObjectReplace(MongoUpdateModel data);

        Task MongoObjectDel(JObject data);

        Task<List<BsonDocument>> MongoObjectFind(JObject data);
        #endregion

        #region MongoDB数据示例(实体对象)

        Task<SampleTableInput> GetMongoModel(string id);

        PagedResultDto<SampleTableInput> GetMongoList(PagingDto pagingDto, string val);

        Task DelMongoModel(string id);

        Task<SampleTable> MongoSave(SampleTableInput jobject);
        #endregion

    }
}
