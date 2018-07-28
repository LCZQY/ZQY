using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralSurvey.Models
{

    
    public class MySqlTest
    {

        [Key]
        public Guid Id { get; set; }

        [MaxLength(32)]
        public string Title { get; set; }

        public string Content { get; set; }

        public JsonObject<string[]> Tags { get; set; } // Json存储
    }
}
