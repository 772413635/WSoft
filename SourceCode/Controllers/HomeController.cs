using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSoft.Models;

namespace WSoft.Controllers
{
    public class HomeController : BaseController
    {


        public ActionResult Index()
        {

          
            return View();
        }

    }
}
