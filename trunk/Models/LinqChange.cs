using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Data;
using System.Data.Linq;
using System.Text;

namespace WSoft.Models
{
    /// <summary>
    /// dataTable和list<T>的转换
    /// </summary>
    public class LinqChange
    {
        /// <summary>
        /// 拷贝对象的属性值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="tSource">源对象</param>
        /// <param name="tDestination">设置属性值的对象</param>
        public static void CopyObjectProperty<T>(dynamic tSource, T tDestination) where T : class
        {
            //获得所有property的信息
            var properties = tSource.GetType().GetProperties();
            var propertiest = tDestination.GetType().GetProperties();
            foreach (var p in properties)
            {
                foreach (var t in propertiest)
                {
                    if (t.Name == p.Name)
                    {
                        t.SetValue(tDestination, p.GetValue(tSource, null), null);//设置tDestination的属性值    
                    }
                }
                         
            }
        }

        //表中有数据或无数据时使用,可排除DATASET不支持System.Nullable错误
        public static DataTable ConvertToDataSet<T>(IList<T> list)
        {
            if (list == null || list.Count <= 0)
            {
                var result = new DataTable();
                if (list != null && list.Count > 0)
                {
                    var propertys = list[0].GetType().GetProperties();
                    foreach (var pi in propertys)
                    {
                        {
                            result.Columns.Add(pi.Name, pi.PropertyType);
                        }

                    }

                    foreach (var t in list)
                    {
                        var tempList = new ArrayList();
                        foreach (var obj in propertys.Select(pi => pi.GetValue(t, null)))
                        {
                            tempList.Add(obj);
                        }
                        var array = tempList.ToArray();
                        result.LoadDataRow(array, true);
                    }
                }
                return result;
            }
            var ds = new DataSet();
            var dt = new DataTable(typeof(T).Name);

            var myPropertyInfo =
                typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var t in list)
            {
                if (t == null) continue;
                DataRow row = dt.NewRow();

                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    var pi = myPropertyInfo[i];
                    String name = pi.Name;

                    if (dt.Columns[name] == null)
                    {
                        DataColumn column;
                        if (pi.PropertyType.UnderlyingSystemType.ToString() == "System.Nullable`1[System.Int32]")
                        {
                            column = new DataColumn(name, typeof(Int32));
                            dt.Columns.Add(column);
                            if (pi.GetValue(t, null) != null)
                                row[name] = pi.GetValue(t, null);
                            else
                                row[name] = DBNull.Value;
                        }
                        else
                        {
                            column = new DataColumn(name, pi.PropertyType);
                            dt.Columns.Add(column);
                            row[name] = pi.GetValue(t, null);
                        }
                    }
                    else
                    {
                        row[name] = pi.GetValue(t, null);
                    }
                }
                dt.Rows.Add(row);
            }
            ds.Tables.Add(dt);
            return ds.Tables[0];
        }

        public static void Detatch<TEntity>(TEntity entity)
        {
            Type t = entity.GetType();
            PropertyInfo[] properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(EntitySet<>))
                {
                    property.SetValue(entity, null, null);
                }
            }
            FieldInfo[] fields = t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                if (field.FieldType.IsGenericType &&
                field.FieldType.GetGenericTypeDefinition() == typeof(EntityRef<>))
                {
                    field.SetValue(entity, null);
                }
            }
            EventInfo eventPropertyChanged = t.GetEvent("PropertyChanged");
            EventInfo eventPropertyChanging = t.GetEvent("PropertyChanging");
            if (eventPropertyChanged != null)
            {
                eventPropertyChanged.RemoveEventHandler(entity, null);
            }
            if (eventPropertyChanging != null)
            {
                eventPropertyChanging.RemoveEventHandler(entity, null);
            }
        }


        /// <summary>  
        /// DataTable转list<T>  
        /// </summary>  
        /// <param name="dt">DataTable</param>  
        /// <returns></returns>  
        public static List<T> ConvertToList<T>(DataTable dt) where T : class, new()
        {
            // 返回值初始化 
            List<T> result = new List<T>();

            DataTable p_Data = dt;

            for (int j = 0; j < p_Data.Rows.Count; j++)
            {
                T _t = (T)Activator.CreateInstance(typeof(T));
                System.Reflection.PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo pi in propertys)
                {
                    for (int i = 0; i < p_Data.Columns.Count; i++)
                    {
                        // 属性与字段名称一致的进行赋值 
                        string columnName = p_Data.Columns[i].ColumnName.ToUpper();
                        if (pi.Name.ToUpper().Equals(columnName) || pi.Name.ToUpper().Equals(columnName.Replace("_", string.Empty)))
                        {
                            // 数据库NULL值单独处理 
                            if (p_Data.Rows[j][i] != DBNull.Value && p_Data.Rows[j][i] != null)
                            {
                                if (pi.PropertyType == typeof(Boolean) || pi.PropertyType == typeof(Boolean?))
                                {
                                    pi.SetValue(_t, Convert.ToBoolean(p_Data.Rows[j][i]), null);
                                }
                                else if (pi.PropertyType == typeof(Int16) || pi.PropertyType == typeof(Int16?))
                                {
                                    pi.SetValue(_t, Convert.ToInt16(p_Data.Rows[j][i]), null);
                                }
                                else if (pi.PropertyType == typeof(Int32) || pi.PropertyType == typeof(Int32?))
                                {
                                    pi.SetValue(_t, Convert.ToInt32(p_Data.Rows[j][i]), null);
                                }
                                else if (pi.PropertyType == typeof(Decimal) || pi.PropertyType == typeof(Decimal?))
                                {
                                    pi.SetValue(_t, Convert.ToDecimal(p_Data.Rows[j][i]), null);
                                }
                                else if (pi.PropertyType == typeof(DateTime) || pi.PropertyType == typeof(DateTime?))
                                {
                                    pi.SetValue(_t, Convert.ToDateTime(p_Data.Rows[j][i]), null);
                                }
                                else if (pi.PropertyType == typeof(Guid) || pi.PropertyType == typeof(Guid?))
                                {
                                    pi.SetValue(_t, Guid.Parse(p_Data.Rows[j][i].ToString()), null);
                                }
                                else
                                {
                                    pi.SetValue(_t, p_Data.Rows[j][i], null);
                                }
                            }
                            else
                                pi.SetValue(_t, null, null);
                            break;
                        }
                    }
                }
                result.Add(_t);
            }

            return result;
        }


        /// <summary>
        /// 绑定笔数
        /// </summary>
        /// <param name="count">总笔数</param>
        /// <param name="page">第几页</param>
        /// <param name="size">每页笔数</param>
        /// <returns>笔数字符串</returns>
        public static string BindTotal(int count, int page, int size)
        {
            return String.Format("第{0}-{1}条  /  共{2}条数据", count == 0 ? 0 : size * (page - 1) + 1, size * page > count ? count : size * page, count);
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        /// <param name="count">总笔数</param>
        /// <param name="page">第几页</param>
        /// <param name="size">每页笔数</param>
        /// <param name="key">关键字</param>
        /// <param name="file">文件名称</param>
        /// <returns>分页字符串</returns>
        public static string BindPager(int count, int page, int size, string file)
        {
            int total = count % size == 0 ? (count - 1) / size + 1 : count / size + 1;
            StringBuilder sb = new StringBuilder();
            if (page == 1)
            {
                sb.AppendFormat("<a href='javascript:void;' class='past_btn' ></a>");
                sb.AppendFormat("<a href='#' class='last_btn' onclick=show('{0}','{1}','{2}')></a>", file, page + 1, size);
            }
            else if (page > 1 && page < total)
            {
                sb.AppendFormat("<a href='#' class='first_btn' onclick=show('{0}','{1}','{2}')></a>", file, page - 1, size);
                sb.AppendFormat("<a href='#' class='last_btn' onclick=show('{0}','{1}','{2}')></a>", file, page + 1, size);

            }
            else if (page == total)
            {
                sb.AppendFormat("<a href='#' class='first_btn' onclick=show('{0}','{1}','{2}')></a>", file, page - 1, size);
                sb.AppendFormat("<a href='javascript:void;' class='next_btn'></a>");
            }
            return sb.ToString();
        }
    }
}
