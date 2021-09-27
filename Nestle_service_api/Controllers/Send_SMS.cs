using Microsoft.AspNetCore.Mvc;
using Nestle_service_api.Context;
using Nestle_service_api.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
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

            if (_sendSMS_dtac(S.Senders,S.RefID,S.ProjectID,S.phone_no,S.Msgs) == true)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }
        private bool _sendSMS_dtac(string _sender, string _refID, string _projectID, string _telno, string _msg)
        {

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api05.fcc.co.th/api/Send_SMS/SendMsg");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"phone_no\":\"" + _telno.Trim() + "\"," +
                                   "\"msgs\":\" " + _msg.Trim() + "\"," +
                                   "\"senders\":\"" + _sender.Trim() + "\"," +
                                  "\"refID\":\"" + _refID + "\"," +
                                  "\"projectID\":\"" + _projectID + "\"}";

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd().ToLower();
                    if (result.IndexOf("success", 0) > -1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception e)
            {
                return false;
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
