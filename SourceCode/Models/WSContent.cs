using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace WSoft.Models
{
    public class WSContent
    {
        private ConnDbForAcccess _dc;

        public WSContent()
        {
            _dc = new ConnDbForAcccess();
        }



        /// <summary>
        /// 返回Access数据库对象
        /// </summary>
        /// <returns></returns>
        public ConnDbForAcccess ConnDbForAcccess
        {
            get { return _dc; }
        }



        /// <summary>
        /// 返回数据集
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public List<T> TableList<T>(string sql) where T : class,new()
        {
            var ds = _dc.ReturnDataSet(sql);
            if (ds.Tables.Count > 1)
            {
                throw new Exception("数据集中不止一张表");
            }
            else
            {
                return LinqChange.ConvertToList<T>(ds.Tables[0]);
            }
        }

        /// <summary>
        /// 返回实体的数据集
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="qo">查询对象</param>
        /// <returns></returns>
        public List<T> TableList<T>(QueryObject qo) where T : class, new()
        {
            var sql = SqlText(typeof(T).Name, qo);
            var ds = _dc.ReturnDataSet(sql);
            if (ds.Tables.Count > 1)
            {
                throw new Exception("数据集中不止一张表");
            }
            else
            {
                return LinqChange.ConvertToList<T>(ds.Tables[0]);
            }
        }

        /// <summary>
        /// 返回实体的数据集
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="obejctName">表对象名称或子查询</param>
        /// <param name="qo">查询对象</param>
        /// <returns></returns>
        public List<T> TableList<T>(string obejctName, QueryObject qo) where T : class, new()
        {
            var sql = SqlText(obejctName, qo);
            var ds = _dc.ReturnDataSet(sql);
            if (ds.Tables.Count > 1)
            {
                throw new Exception("数据集中不止一张表");
            }
            else
            {
                return LinqChange.ConvertToList<T>(ds.Tables[0]);
            }
        }




        /// <summary>
        /// 返回一个实体对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="qo">查询对象</param>
        /// <returns></returns>
        public T FirstOrDefault<T>(QueryObject qo) where T : class, new()
        {
            var sql = SqlFirstText(typeof(T).Name, qo);
            var dt = _dc.ReturnDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return LinqChange.ConvertToList<T>(dt)[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 返回一个实体对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="objcetName">表对象名称或子查询</param>
        /// <param name="qo">查询对象</param>
        /// <returns></returns>
        public T FirstOrDefault<T>(string objcetName, QueryObject qo) where T : class, new()
        {
            var sql = SqlFirstText(objcetName, qo);
            var dt = _dc.ReturnDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return LinqChange.ConvertToList<T>(dt)[0];
            }
            else
            {
                return null;
            }
        }




        private string SqlText(string objectName, QueryObject qo)
        {
            var sb = new StringBuilder();
            if (qo.Page == 0 || qo.Rp == 0) //不分页
            {

                sb.AppendFormat("select * from ({0}) {1} Order by {2} {3}", objectName, qo.WhereStr, qo.SortName,
                    qo.SortOrder);

            }
            else
            {
                var skip = (qo.Page - 1) * qo.Rp;
                if (skip == 0)
                {
                    sb.AppendFormat("select top {3} * from ({0}) {4} order by {1} {2}", objectName, qo.SortName, qo.SortOrder, qo.Rp,qo.WhereStr);
                }
                else
                {
                    sb.AppendFormat("select top {3} * from ({0}) where Id >(select top 1 max(Id) from (select top {4} Id from ({0}) {5} order by {1} {2})) {6}", objectName, qo.SortName, qo.SortOrder, qo.Rp, skip,qo.WhereStr,qo.WhereStr.Replace("where","and"));
                }


            }
            return sb.ToString();
        }


        private string SqlFirstText(string objectName, QueryObject qo)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("select top 1 * from ({0}) {1} Order by {2} {3}", objectName, qo.WhereStr, qo.SortName,
                qo.SortOrder);
            return sb.ToString();
        }

    }
}