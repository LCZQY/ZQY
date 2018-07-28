
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;

namespace GeneralSurvey_Data.Model
{
    /// <summary>
    ///  储存答案
    /// </summary>
    public class AnswerGroup
    {
        /// <summary>
        /// 表ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        ///  Json数据类型
        /// </summary>
         public string Answer { get; set; }


         /// <summary>
         /// 不同表的答案
         /// </summary>
         public string  FromID { get; set; }

    }
}
