using System;
using System.Collections.Generic;

namespace WSoft.Models
{
    /// <summary>
    /// 企业表
    /// </summary>
    public class ws_Company
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Business { get; set; }
        public string ICP { get; set; }

    }
    /// <summary>
    /// 产品表
    /// </summary>
    public class ws_Products
    {
        public int Id
        {
            get;
            set;
        }

        public string Code
        {
            get;
            set;
        }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Remark { get; set; }

        public decimal Money { get; set; }

        public string  FirstImg { get; set; }

        public List<string> ContentImgList { get; set; }

        public DateTime CreaTime { get; set; }
        public int IsDelete { get; set; }

    }
    /// <summary>
    /// 图片表
    /// </summary>
    public class ws_Photos
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string SourceCode { get; set; }

        public string Src { get; set; }

        public int IsCover { get; set; }

        public int IsFirst { get; set; }
        public int IsDelete { get; set; }
    }
}
