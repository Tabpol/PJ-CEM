using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nestle_service_api.Model
{
    public class tbMessageLog
    {
        [Key]
        public Int64 LogID { get; set; }
        public string Channel { get; set; }
        public string RefID { get; set; }
        public string ProjectID { get; set; }
        public string TelNo { get; set; }
        public string SendMessage { get; set; }
        public string ApiMessage { get; set; }
        public DateTime SendDateTime { get; set; }
    }
}
