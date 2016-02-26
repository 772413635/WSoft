using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSoft.Models
{
    public class QueryObject
    {
        private string sortOrder = "asc";
        private string sortName = "Id";
        /// <summary>
        /// 页数
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 条数
        /// </summary>
        public int Rp { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortName
        {
            get { return sortName; }
            set { sortName = value; }
        }
        /// <summary>
        /// 排序类型
        /// </summary>
        public string SortOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string WhereStr { get; set; }
    }
}