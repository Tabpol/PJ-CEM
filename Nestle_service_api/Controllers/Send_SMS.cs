using Microsoft.AspNetCore.Mvc;
using Nestle_service_api.Context;
using Nestle_service_api.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nestle_service_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Send_SMS : Controller
    {
        private readonly Fcc_Connect _fcc_Connect;

        public Send_SMS(Fcc_Connect fcc_Connect)
        {
            _fcc_Connect = fcc_Connect;
        }

        [HttpPost]
        public ActionResult<string> SendMsg(prmSms S)
        {

            string RefNo = "1";
            string Sender = S.Senders;
            string Msn = S.phone_no;
            string Sno = "00";
            string MsgType = "H";
            string User = "api1614158";
            string Password = "iafg2qp8";
            string Msg = ConvertStringToHex(S.Msgs);
            string Status = "";
            string alert_;
            tbMessageLog lg = new tbMessageLog();
            lg.LogID = 0;
            lg.Channel = "";
            lg.RefID = S.RefID;
            lg.ProjectID = S.ProjectID;
            lg.TelNo = S.phone_no;
            lg.SendMessage = S.Msgs;
            lg.ApiMessage = "";
            lg.SendDateTime = DateTime.Now;

            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["RefNo"] = RefNo;
                    values["Sender"] = Sender;
                    values["Msn"] = Msn;
                    values["Sno"] = Sno;
                    values["MsgType"] = MsgType;
                    values["User"] = User;
                    values["Password"] = Password;
                    values["Msg"] = Msg;
                    values["Encoding"] = "8";
                    string url = "https://apismsplus.dtac.co.th/servlet/com.iess.socket.SmsCorplink";
                    var response = client.UploadValues(url, values);

                    var responseString = Encoding.Default.GetString(response);
                    alert_ = responseString;
                    lg.ApiMessage = alert_;
                    string[] s2 = responseString.Split('\n');
                    foreach (string author in s2)
                    {
                        if (author == "Status=0")
                        {
                            Status = "Success";
                        }
                    }
                }

                if (Status == "Success")
                {
                    lg.Channel = "SMS_" + Status + "";
                    savelog(lg);
                    return Ok(Status.ToString());

                }
                else
                {
                    lg.Channel = "SMS_Failed";
                    savelog(lg);
                    return NotFound(Status.ToString());
                }
            }
            catch
            {
                lg.Channel = "SMS_ServerFailed";
                lg.ApiMessage = "ส่ง SMS ไม่สำเร็จ ";
                savelog(lg);
                return NotFound();
            }

        }
        static string ConvertStringToHex(string message)
        {
            byte[] bytes = Encoding.BigEndianUnicode.GetBytes(message);
            string hex = BitConverter.ToString(bytes);
            hex = hex.Replace("-", "");
            return hex;
        }
        private void savelog(tbMessageLog data)
        {
            try
            {
                _fcc_Connect.tbMessageLog.Add(data);
                _fcc_Connect.SaveChanges();
            }
            catch { }
        }
    }
}
