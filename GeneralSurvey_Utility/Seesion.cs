using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GeneralSurvey_Utility
{
    public static class Seesion
    {
        /// <summary>
        ///  原题号
        /// </summary>
        public static string _stide { get; set; }

        /// <summary>
        /// 表单id
        /// </summary>
        public static string FromIds { get; set; } = "1";

        /// <summary>
        /// 随机数
        /// </summary>
        public static Random sj { get; set; } = new Random();

        /// <summary>
        ///  当前用户名
        /// </summary>
        public static string UserName { get; set; } = "Admin";



        /// <summary>
        ///  组合展示表头
        /// </summary>
        /// <returns></returns>
        public static string StrBuilder()
        {

            StringBuilder theader = new StringBuilder();
            theader.Append("<thead>");
            foreach (var item in HelpTopicgroup.GetList())
            {
                theader.Append("<tr");
                theader.Append("<th>" + item.TopicName + "</th>");
                theader.Append("</tr");
            }
            theader.Append("</thead>");

            return theader.ToString();
        }

    }
}
