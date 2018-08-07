using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralSurvey_Data.Model
{

    /// <summary>
    ///  问题
    /// </summary>
    public class Topicgroups
    {
        public string id { get; set; }
        /// <summary>
        /// 题干
        /// </summary>
        public string TopicName { get; set; }

        /// <summary>
        /// 长度限制
        /// </summary>
        public string CharactersSize { get; set; }
        /// <summary>
        /// 题目类型
        /// </summary>
        public string SetsettingId { get; set; }
        /// <summary>
        /// 选项名称
        /// </summary>
        public string OptionText { get; set; }

        /// <summary>
        /// 题号
        /// </summary>
        public int Stide { get; set; }

        /// <summary>
        ///  表单ID        
        /// </summary>
        public string FromID { get; set; }

        /// <summary>
        ///  选项关键字用于提交数据
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        ///  是否为空
        /// </summary>
        public string Isnull { get; set; }

    }
}
