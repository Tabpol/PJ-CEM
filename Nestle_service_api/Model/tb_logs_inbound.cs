using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nestle_service_api.Model
{
    public class tb_logs_inbound : BaseEntity
    {
        public Int32 Id { get; set; }
        public string case_id { get; set; }
        public string aqent_name { get; set; }
        public DateTime create_date { get; set; }
        public Int32 number_of_repeat { get; set; }
        public string status_of_case { get; set; }
        public string status_of_contact { get; set; }
    }
}
