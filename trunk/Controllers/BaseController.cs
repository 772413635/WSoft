using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSoft.Models;
using System.Text;
using System.Configuration;
using System.Net;
namespace WSoft.Controllers
{
    public class BaseController : Controller
    {
        protected readonly WSContent _dc;
        protected readonly string imgRootUrl;
        public BaseController()
        {
            _dc = new WSContent();
            imgRootUrl = ConfigurationManager.AppSettings["imgRootUrl"];
        }


        /// <summary>
        /// 显示图片
        /// </summary>
        /// <param name="code">资源</param>
        /// <param name="type"></param>
        [HttpGet]
        public void WriteImg(string code,int type)
        {
            Response.ContentType = "application/octet-stream";
            Response.Buffer = false;
            Response.ContentEncoding = Encoding.UTF8;
            Response.AddHeader("Connection", "Keep-Alive");
            
          
            var param = new QueryObject
            {
                WhereStr = string.Format("where SourceCode='{0}' and IsCover={1} and IsDelete=1", code, type)
            };
            var query = _dc.FirstOrDefault<ws_Photos>(param);
            if (query == null)
            {
                HttpContext.Response.WriteFile("");
            }
            else
            {
                Response.AddHeader("Content-Disposition", "attachment;filename=" + query.Src);
                var wb = new WebClient();
                var bytes=wb.DownloadData(imgRootUrl + query.Src);
                Response.AddHeader("Content-Length", bytes.Length.ToString(CultureInfo.InvariantCulture));
                HttpContext.Response.BinaryWrite(bytes);
            }

        }


    }



    //扩展html.lable
    public static class HttpHelper
    {
        public static MvcHtmlString LableValueFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var name = metadata.PropertyName;
            TModel t = htmlHelper.ViewData.Model;
            if (t != null)
            {
                var value = t.GetType().GetProperty(name).GetValue(t, null) ?? "";
                value = value.ToString();
                return MvcHtmlString.Create("<lable>" + value + "</lable>");
            }
            return MvcHtmlString.Create("<lable></lable>");
        }

        public static MvcHtmlString LableValueFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                     System.Linq.Expressions.Expression
                                                                         <Func<TModel, TProperty>> expression,
                                                                     object htmlCss)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            var name = metadata.PropertyName;
            TModel t = htmlHelper.ViewData.Model;
            StringBuilder classAndStyle = new StringBuilder();
            foreach (var p in htmlCss.GetType().GetProperties())
            {
                classAndStyle.AppendFormat("{0}=\"{1}\"", p.Name, p.GetValue(htmlCss, null));
            }
            if (t != null)
            {
                var value = t.GetType().GetProperty(name).GetValue(t, null) ?? "";
                value = value.ToString();
                return MvcHtmlString.Create("<lable " + classAndStyle + ">" + value + "</lable>");
            }
            return MvcHtmlString.Create("<lable " + classAndStyle + "></lable>");
        }





        public static MvcHtmlString PValueFor<TModel, TProperty>(
    this HtmlHelper<TModel> htmlHelper,
    System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var name = metadata.PropertyName;
            TModel t = htmlHelper.ViewData.Model;
            if (t != null)
            {
                var value = t.GetType().GetProperty(name).GetValue(t, null) ?? "";
                value = value.ToString();
                return MvcHtmlString.Create("<p>" + value + "</p>");
            }
            return MvcHtmlString.Create("<p></p>");
        }
        public static MvcHtmlString PValueFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                    System.Linq.Expressions.Expression
                                                                        <Func<TModel, TProperty>> expression,
                                                                    object htmlCss)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            var name = metadata.PropertyName;
            TModel t = htmlHelper.ViewData.Model;
            StringBuilder classAndStyle = new StringBuilder();
            foreach (var p in htmlCss.GetType().GetProperties())
            {
                classAndStyle.AppendFormat("{0}=\"{1}\"", p.Name, p.GetValue(htmlCss, null));
            }
            if (t != null)
            {
                var value = t.GetType().GetProperty(name).GetValue(t, null) ?? "";
                value = value.ToString();
                return MvcHtmlString.Create("<p " + classAndStyle + ">" + value + "</p>");
            }
            return MvcHtmlString.Create("<P " + classAndStyle + "></P>");
        }



        public static MvcHtmlString TextValueFor<TModel, TProperty>(
           this HtmlHelper<TModel> htmlHelper,
           System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var name = metadata.PropertyName;
            TModel t = htmlHelper.ViewData.Model;
            if (t != null)
            {
                var value = t.GetType().GetProperty(name).GetValue(t, null) ?? "";
                value = value.ToString();
                return MvcHtmlString.Create(value.ToString());
            }
            return MvcHtmlString.Create("");
        }

    }
}