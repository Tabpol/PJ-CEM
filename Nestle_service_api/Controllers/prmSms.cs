using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nestle_service_api.Controllers
{
    public class prmSms
    {
        public string phone_no { get; set; }
        public string Msgs { get; set; }
        public string Senders { get; set; }
        public string RefID { get; set; }
        public string ProjectID { get; set; }
    }
}
