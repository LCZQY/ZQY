using QP.Prospectus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QP.Prospectus.Utility;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using static System.Net.WebRequestMethods;

namespace QP.Prospectus.Utility
{
    public static class Communal
    {
        public static string StringBuil(string filed)
        {
            List<string> builder = new List<string>() { };
            if (filed.IndexOf(',') > -1)
            {
                var arr = filed.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] != "false")
                    {
                        builder.Add(arr[i]);
                    }
                }
            }
            return String.Join("*", builder).ToString();
        }


        public static List<string> ReturnList(string field)
        {
            List<string> arry = new List<string>();

            try
            {
                if (field.IndexOf('*') > -1)
                {
                    var arr = field.Split('*');
                    for (int i = 0; i < arr.Length; i++)
                    {                                               
                            arry.Add(arr[i]);                        
                    }
                }
            }
            catch { }

            return arry;
        }
    }
}