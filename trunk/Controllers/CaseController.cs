using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSoft.Models;

namespace WSoft.Controllers
{
    public class CaseController : BaseController
    {
        //
        // GET: /Case/

        public ActionResult Index()
        {
            return View();
        }



        //案例列表
        [HttpPost]
        [OutputCache(Duration = 3600, VaryByParam = "*")]
        public JsonResult ProList(QueryObject param)
        {
            var count = _dc.ConnDbForAcccess.ReturnDataSet("select count(Id) from ws_Products " + param.WhereStr).Tables[0].Rows[0][0];
            var proList = _dc.TableList<ws_Products>(param);
            return Json(new
            {
                proList,
                total = count
            });
        }


        // 显示案例详情
        public ActionResult ShowProducts(string id)
        {
            var param = new QueryObject
            {
                WhereStr = string.Format("where Code='{0}'", id)
            };


            var proModel = _dc.FirstOrDefault<ws_Products>(qo: param);
            var param2 = new QueryObject
            {
                WhereStr = string.Format("where SourceCode='{0}' and IsFirst=1 and IsDelete=1", proModel.Code)
            };
            var param3 = new QueryObject
            {
                WhereStr = string.Format("where SourceCode='{0}' and IsFirst=0 and IsCover=0 and IsDelete=1", proModel.Code)
            };
            var firstImg = _dc.FirstOrDefault<ws_Photos>(qo: param2);

            proModel.FirstImg = (firstImg == null ? "" : imgRootUrl + firstImg.Src);


            proModel.ContentImgList = _dc.TableList<ws_Photos>("select Src from ws_Photos " + param3.WhereStr).Select(c =>imgRootUrl+ c.Src).ToList();


            return View("ShowProduct", proModel);
        }
    }
}
