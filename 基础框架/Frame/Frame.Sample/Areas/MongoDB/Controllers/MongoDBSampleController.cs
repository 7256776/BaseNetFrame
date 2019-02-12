using Frame.Core;
using Frame.MongoDB;
using Frame.Web;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Frame.Sample
{
    public class MongoDBSampleController : FrameExtAbpController
    {

        private readonly ISampleAppService _sampleAppService;

        public MongoDBSampleController(ISampleAppService sampleAppService)
        {
            _sampleAppService = sampleAppService;
        }

        // GET: MongoDBSample/MongoDBSample
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexObject()
        {
            return View();
        }

        #region MongoDB数据实体(自定义对象)
        /// <summary>
        /// JObject
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JsonResult> MongoObjectAdd(JObject data)
        {
            await _sampleAppService.MongoObjectAdd(data);
            return Json(true);
        }

        public async Task<JsonResult> MongoObjectEdit(JObject data)
        {
           
            MongoUpdateModel model = new MongoUpdateModel();
            model.FilterJson = JObject.Parse(data["filterJson"].ToString());
            model.UpdataJson = JObject.Parse(data["updataJson"].ToString());

            await _sampleAppService.MongoObjectEdit(model);
            return Json(true);
        }

        public async Task<JsonResult> MongoObjectReplace(JObject data)
        {

            MongoUpdateModel model = new MongoUpdateModel();
            model.FilterJson = JObject.Parse(data["filterJson"].ToString());
            model.UpdataJson = JObject.Parse(data["updataJson"].ToString());
        
            await _sampleAppService.MongoObjectReplace(model);
            return Json(true);
        }

        public async Task<JsonResult> MongoObjectDel(JObject data)
        {
            await _sampleAppService.MongoObjectDel(data);
            return Json(true);
        }

        public async Task<JsonResult> MongoObjectFind(JObject data)
        {
            var result = await _sampleAppService.MongoObjectFind(data);
            //转换格式为dic键值对
            var dics = result.Select(bs =>
            {
                var dict = bs.ToDictionary();
                    //dict.Remove("_id");
                    return dict;
            }).ToList();
            return Json(dics);
        }
        #endregion

        #region MongoDB数据示例(实体对象)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult GetMongoList(PagingDto pagingDto, string val)
        {
            var reslut = _sampleAppService.GetMongoList(pagingDto, val);
            return Json(reslut);
        }

        public async Task<JsonResult> GetMongoModel(string id)
        {
            var reslut = await _sampleAppService.GetMongoModel(id);
            return Json(reslut);
        }

        public async Task<JsonResult> DelMongoModel(string id)
        {
            await _sampleAppService.DelMongoModel(id);
            return Json(true);
        }

        /// <summary>
        /// JObject
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JsonResult> MongoSave(SampleTableInput model)
        {
            var reslut = await _sampleAppService.MongoSave(model);
            return Json(reslut);
        }

        #endregion

    }
}