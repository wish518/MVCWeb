using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tool_Web.Models
{
    /// <summary>
    /// 基底資料模型
    /// </summary>
    [Serializable]
    public class BaseModelX
    {
        /// <summary>
        /// 目標Model透過來源Model轉換數值(需同屬性名稱及型別)
        /// </summary>
        /// <param name="source"></param>
        public void Parse<TSource>(TSource source)
            where TSource : class
        {
            if (source == null) return;

            var sourceProps = source.GetType().GetProperties();
            foreach (var item in sourceProps)
            {
                var prop = this.GetType().GetProperty(item.Name);
                if (prop != null && item.PropertyType == prop.PropertyType && prop.CanWrite)
                {
                    prop.SetValue(this, item.GetValue(source));
                }
            }

            return;
        }
    }
}