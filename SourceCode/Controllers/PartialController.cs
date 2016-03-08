using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSoft.Models;

namespace WSoft.Controllers
{
    public class PartialController : BaseController
    {
        //
        // GET: /Partial/
        [OutputCache(Duration=3600*24,VaryByParam="*")]
        public ActionResult PartialFooter()
        {
            var param = new QueryObject();
            var query = _dc.FirstOrDefault<ws_Company>(param);

            return PartialView(query);
        }

    }
}
