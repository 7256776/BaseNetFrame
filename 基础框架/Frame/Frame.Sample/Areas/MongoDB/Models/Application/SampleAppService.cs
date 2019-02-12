using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Notifications;
using Abp.UI;
using Abp.Web.Models;
using Frame.Application;
using Frame.Core;
using Frame.MongoDB;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frame.Sample
{
    [Audited]
    public class SampleAppService :   FrameExtApplicationService, ISampleAppService
    {
        private readonly ISampleRepository _sampleRepository;
        public SampleAppService(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        #region MongoDB数据实体(自定义对象)

        public async Task MongoObjectAdd(JObject data)
        {
            await _sampleRepository.MongoObjectAdd(data);
        }

        public async Task MongoObjectEdit(MongoUpdateModel data)
        {
            await _sampleRepository.MongoObjectEdit(data);
        }

        public async Task MongoObjectReplace(MongoUpdateModel data)
        {
            await _sampleRepository.MongoObjectReplace(data);
        }

        public async Task MongoObjectDel(JObject data)
        {
            await _sampleRepository.MongoObjectDel(data);
        }

        public async Task<List<BsonDocument>> MongoObjectFind(JObject data)
        {
            return await _sampleRepository.MongoObjectFind(data);
        }
        #endregion


        #region MongoDB数据示例(实体对象)

        public PagedResultDto<SampleTableInput> GetMongoList(PagingDto pagingDto, string val)
        { 
            var dataQuery = _sampleRepository.GetAll().Where(w => w.dataString.Contains(val));
            //分页查询
            var data = dataQuery.GetPagingData<SampleTable, SampleTableInput>(pagingDto);
            return data;
        }

        public async Task<SampleTableInput> GetMongoModel(string id)
        {
            var oid = ObjectId.Parse(id);
            //查询模块以及所包含的授权动作
            var data = await _sampleRepository.GetAsync(oid);
            return data.MapTo<SampleTableInput>();
        }

        public async Task DelMongoModel(string id)
        {
            var oid = ObjectId.Parse(id);
            //查询模块以及所包含的授权动作
            await _sampleRepository.DeleteAsync(oid);
        }

        public async Task<SampleTable> MongoSave(SampleTableInput model)
        {
            /*
             * 接收请求参数方式一:
             * 接收参数类型是 JObject
             * 因此需要转换json对象到实体对象方式如下:
             * 首先设置id的标签  [JsonConverter(typeof(ObjectIdConverter))]
             * 然后通过对象转换 var model = jobject.ToObject<SampleTableInput>();
             * 
             *  接收请求参数方式二:
             *  接收参数类型是 SampleTableInput
             *  这种方式请求会自动转换数据id为ObjectId类型,系统已经默认注册了转换对象
             */

            //SampleTableInput model = new SampleTableInput();

            if (!model.Id.HasValue)
            {
                SampleTable modelInput = model.MapTo<SampleTable>();
                // JObject model
                return await _sampleRepository.InsertAsync(modelInput);
            }
            else
            {
                //获取需要更新的数据
                //SampleTable data = await _sampleRepository.GetAsync(ObjectId.Parse(model.Id.ToString()));
                SampleTable data = await _sampleRepository.GetAsync(model.Id.Value);

                //映射需要修改的数据对象
                SampleTable m = ObjectMapper.Map(model, data);
                //修改菜单主数据
                return await _sampleRepository.UpdateAsync(m);
            }

        }

        #endregion


    }
}
