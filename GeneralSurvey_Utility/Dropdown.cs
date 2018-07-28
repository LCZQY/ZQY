using GeneralSurvey_Data.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace GeneralSurvey_Utility
{

    public static class Dropdown
    {
        /// <summary>
        /// 组合下拉框后台数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static Dictionary<string,string> createDropdown(List<Setsetting> query)
        {
            Dictionary<string, string> valuePairs = new Dictionary<string, string>() { };
            if (query != null)
            {
                foreach (var item in query)
                {
                    valuePairs.Add(item.id.ToString(), item.TopicClass.ToString());
                }
            }
            else
            {
                valuePairs.Add("0", "常规");
            }

            return valuePairs;
        }


    }
}
