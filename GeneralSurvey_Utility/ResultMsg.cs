using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GeneralSurvey_Utility
{
    public static class ResultMsg
    {
        public static Hashtable FormatResult(int code = 200, string message = "处理完成", string description = "处理完成")
        {
            var ht = new Hashtable();
            ht.Add("code", code);
            ht.Add("message", message);
            ht.Add("description", description);
            return ht;
        }

        public static Hashtable FormatResult(Exception ex)
        {
            var ht = new Hashtable();
            ht.Add("code", ex.GetHashCode());
            ht.Add("message", ex.Message);
            ht.Add("description", ex.Message);
            return ht;
        }
    }
}
