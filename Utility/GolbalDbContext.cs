using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QP.Prospectus.Models;
using System.Data.Entity.Core;

namespace QP.Prospectus.Utility
{
    public  class GolbalDbContext
    {
        public GolbalDbContext() { }

        public static zqyEntities db = null;
        public static zqyEntities Instace()
        {
            if(db == null)
            {
                db = new zqyEntities();    
            }
            return db;
        }
    }
}