using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LoginFunctionality.Utility
{
    public static class GraphQLQueryBuilder
    {
        public static string QueryBuilder(object obj)
        {
            string objName = obj.GetType().Name.ToString().ToLower();
            
            var props = obj.GetType().GetProperties().ToList();
            
            string strQuery = "{\"query\": \"{" + objName + "{";
            foreach (PropertyInfo prop in props)
            {
                strQuery = strQuery + prop.Name.ToString().ToLower() + ",";
            }
            strQuery=strQuery.TrimEnd(',');
            strQuery = strQuery + "}}\"}";
            return strQuery;
        }
    }
}
