using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nestle_service_api.Model
{
    public class tb_segment
    {
        //[key]
        public int tb_id { get; set; }
        public string id_master { get; set; }
        public string g1_1_segment { get; set; }
        public string g1_2_segment { get; set; }
        public string g1_segment { get; set; }
        public string g2_1_segment { get; set; }
        public string g2_2_segment { get; set; }
        public string g2_segment { get; set; }
        public string g3_1_segment { get; set; }
        public string g3_2_segment { get; set; }
        public string g3_3_segment { get; set; }
        public string g3_segment { get; set; }
        public string g4_1_segment { get; set; }
        public string g4_2_segment { get; set; }
        public string g4_segment { get; set; }
        public string g5_segment { get; set; }
        public string consumer_segment { get; set; }
        public string Tellscore_registerURL { get; set; }
        public string brand_segment { get; set; }
        public string bigfan_sms_1 { get; set; }
        public string bigfan_sms_2 { get; set; }
    }
}
