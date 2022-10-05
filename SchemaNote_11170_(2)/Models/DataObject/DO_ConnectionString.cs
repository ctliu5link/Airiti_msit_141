using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNote_11170__2_.Models.DataObject
{
    public class DO_ConnectionString
    {
        public string uid { get; set; }
        public int pwd { get; set; }
        public string database { get; set; }
        public string server { get; set; }
        public string MixConnectionString()
        {
            return $"uid={uid};pwd={pwd};database={database};server={server}";
        }
    }
}