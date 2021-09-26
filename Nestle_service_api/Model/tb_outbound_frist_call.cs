using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nestle_service_api.Model
{
    public class tb_outbound_frist_call : BaseEntity
    {
        public DateTime ob_date { get; set; }
        public string ob_time { get; set; }
        public string contact_status { get; set; }
        public string consurmer_name { get; set; }
        public string consurmer_surmer { get; set; }
        public string owner_mobile_number { get; set; }
        public string callback_customer_date { get; set; }
        public string use_discount_code { get; set; }
        public string discount_code_for { get; set; }
        public string discount_code_exp_date { get; set; }
        public string interested_brand_ambassador { get; set; }
        public string tellscore_registration_status { get; set; }
        public string case_id { get; set; }
    }
}
