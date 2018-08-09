
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
    public class Formsettings
    {
        /// <summary>
        /// 表ID
        /// </summary>
        public string FormID { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string FormNote { get; set; }


        /// <summary>
        /// 标题
        /// </summary>
        public string FormTitle { get; set; }

        /// <summary>
        /// 版权
        /// </summary>
        public string FormCopyright { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime FormCreateDate { get; set; }


        /// <summary>
        /// 问卷状态
        /// </summary>
        public int FormStatus { get; set; }


        /// <summary>
        /// 创建人
        /// </summary>
        public string FormCreater { get; set; }


    }
}
