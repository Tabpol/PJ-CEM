using Microsoft.AspNetCore.Mvc;
using Nestle_service_api.Context;
using Nestle_service_api.Model;
using Nestle_service_api.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nestle_service_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Survey_Link : Controller
    {
       

        private readonly Nestle_Connect _Nestle_Connect;
        private readonly Fcc_Connect _fcc_Connect;
        public Survey_Link(Nestle_Connect Nestle_Connect, Fcc_Connect fcc_Connect)
        {
            _Nestle_Connect = Nestle_Connect;
            _fcc_Connect = fcc_Connect;
        }

        [HttpPost]
        public ActionResult<string> Create_link(master_links m)
        {
            //http://localhost:4200/RegisterCode/:admin
            //string Cus = Convert.ToString(m.customer);
            var data = _Nestle_Connect.tb_master_link.Where(x => x.customer == m.customer).ToList();
            if (data.Count <= 0)
            {
                string source = m.customer;
                var replacement = source.Replace(' ', '_');
                tb_master_link mo = new tb_master_link();
                Random _random = new Random();
                DateTime datenew = DateTime.Now;
                
                    int rundoms = _random.Next(1000, 9999);
                    int rundomsnew = _random.Next(1000, 9999);
                    string msgs = "";
                    mo.id = 0;
                    mo.random_numbe = "CES" + Convert.ToString(rundoms) + "_" + rundomsnew;
                    mo.customer = m.customer;
                    mo.sender = m.sender;
                    mo.create_send = m.create_send;
                    mo.create_link_date = datenew;
                    mo.Url = "https://dev.fcc.co.th:2022/#/RegisterCode/" + mo.random_numbe + "/" + replacement;
                    mo.status = "Send";
                    msgs = "ยินดีด้วยค่ะ คุณได้รับโค้ดส่วนลดมูลค่า 25 บาท คือ NESTV1C25 สำหรับการซื้อขั้นต่ำ 99 บาทขึ้นไป คุณสามารถนำโค้ดส่วนลดนี้ไปใช้กับสินค้าใดก็ได้ที่ Nestlé official store ผ่านทาง";
                    msgs += "Shopee เท่านั้นค่ะ นอกจากนี้คุณได้รับสิทธิ์ในการลงทะเบียนเข้าร่วมกิจกรรม Nestlé Big Fan & Brand influencer สามารถลงทะเบียนได้ที่ ";
                    msgs += "'" + mo.Url + "'";
                    msgs += " แล้วมาเป็นครอบครัว Nestlé ด้วยกันนะคะ!";
                    _Nestle_Connect.tb_master_link.Add(mo);
                    _Nestle_Connect.SaveChanges();

                    var datas = _Nestle_Connect.tb_master_link.Where(x => x.customer == mo.customer).ToList();
                    return Ok(datas);               

            }
            else
            {
                return Ok("มีการลงทะเบียนไปแล้ว");
            }
        }
        [HttpGet]
        public ActionResult<tb_master_link> Resend_Sms(string phone)
        {
            try
            {
                var data = _Nestle_Connect.tb_master_link.Where(x => x.customer == phone).ToList();
                if (data.Count !=0)
                {
                    tb_master_link m = new tb_master_link();
                    _Nestle_Connect.tb_master_link.Where(x => x.customer == phone).SingleOrDefault();
                    m.status = "Resend";
                    _Nestle_Connect.SaveChanges();
                    return Ok(data);
                }
                else
                {
                    return Ok();
                }
               
            }
            catch { return NotFound(); }

        }
        [HttpPost]
        public ActionResult<string> Update_Status_master(Update_Status s)
        {
            try
            {
                tb_master_link u = _Nestle_Connect.tb_master_link.SingleOrDefault(x => x.id == s.id);                
                if (s.casea == "Update_status_Resend")
                {
                    u.status = "Resend";
                }
                _Nestle_Connect.SaveChanges();
                return Ok("update_Status_success");
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost]
        public ActionResult<string> Register_Code(_RegisterHeading r)
        {
            DateTime datenew = DateTime.Now;
            Random _random = new Random();
            int rundoms = _random.Next(1000, 9999);
            int rundoms_ref1 = _random.Next(0, 10);
            int rundoms_ref2 = _random.Next(0, 9);

            tb_RegisterHeading rh = new tb_RegisterHeading();
            rh.id = 0;
            rh.id_master = r.id_master;
            rh.title = r.title;
            rh.name = r.name;
            rh.lastname = r.lastname;
            rh.sex = r.sex;
            rh.age = r.age;
            rh.phone = r.phone;
            rh.facebook = r.facebook;
            rh.email = r.email;
            rh.house_number = r.house_number;
            rh.village = r.village;
            rh.swine = r.swine;
            rh.road = r.road;
            rh.sub_district_name = r.sub_district_name;
            rh.district_name = r.district_name;
            rh.province_name = r.province_name;
            rh.zip_code = r.zip_code;
            rh.OTP = Convert.ToString(rundoms);
            rh.isstatus = "wait_confirmed";
            rh.datesave = datenew;
            rh.RefID = rundoms_ref1 + "C" + rundoms_ref2 +"S";
            //try
            //{
            string otp_ref = rh.OTP + ' ' + "<Ref. " + rh.RefID +">";
            string[] array = new string[] { rh.OTP, rh.RefID };
            try
            {
                _Nestle_Connect.tb_RegisterHeading.Add(rh);
                _Nestle_Connect.SaveChanges();
            }
            catch (Exception)
            {

                return NotFound("Save Eror");
            }

            update_satatus_master(rh.id_master, "wait_confirmed");
            return Ok(array.ToList());
            //}
            // catch
            //{
            //return NotFound();
            // }

        }
        [HttpPost]
        public ActionResult<string> Resend_OTP(Resend_OTP o)
        {
            Random _random = new Random();
            int rundoms = _random.Next(1000, 9999);
            int rundoms_ref1 = _random.Next(0, 10);
            int rundoms_ref2 = _random.Next(0, 9);
            try
            {
                tb_RegisterHeading r = _Nestle_Connect.tb_RegisterHeading.Where(x => x.id_master == o.id_master && x.phone == o.phone).SingleOrDefault();

                r.OTP = Convert.ToString(rundoms);
                r.RefID = rundoms_ref1 + "C" + rundoms_ref2 + "M";
                _Nestle_Connect.SaveChanges();
                string[] array = new string[] { r.OTP, r.RefID };
                update_satatus_master(r.id_master, "wait_confirmed");
                return Ok(array.ToList());
            }
            catch { return NotFound(); }
        }
            
        private bool update_satatus_master(string id_master, string Status)
        {
            tb_master_link m = _Nestle_Connect.tb_master_link.Where(x => x.random_numbe == id_master).SingleOrDefault();
            m.status = Status;
            _Nestle_Connect.SaveChanges();
            return true;
        }

        [HttpPost]
        public ActionResult<tb_master_link> get_master_id(_master m)
        {
            //try
            //{
            var data = _Nestle_Connect.tb_master_link.ToList();
            if (data.Count != 0)
            {
                return Ok(data);
            }
            else
            {
                return NotFound();
            }
            //}
            //catch { return NotFound(); }

        }
        [HttpPost]
        public ActionResult<string> Register_Code_Detail(tb_RegisterDetail d)
        {
            try
            {
                _Nestle_Connect.tb_RegisterDetail.Add(d);
                _Nestle_Connect.SaveChanges();
                return Ok("success");
            }
            catch
            {
                return NotFound();
            }

        }       
        [HttpGet]
        public ActionResult<Array> Getsegment(string idmaster)
        {
           // try {
                var data = _Nestle_Connect.tb_segment.Where(x => x.id_master == idmaster).ToList();

                if (data.Count != 0)
                {
                    string[] arrayurl = new string[] { data[0].bigfan_sms_1, data[0].bigfan_sms_2 };
                    return Ok(arrayurl);
                }
                else
                {
                    return Ok();
                }
            //}
           // catch
            //{
            //    return NotFound();
           // }
            
        }
        [HttpGet]
        public ActionResult CheckOTP(string OTP, string id)
        {
            var data = _Nestle_Connect.tb_RegisterHeading.Where(x => x.OTP == OTP && x.id_master == id).ToList();
            if (data.Count != 0)
            {
                tb_RegisterHeading r = _Nestle_Connect.tb_RegisterHeading.SingleOrDefault(x => x.id_master == id);
                r.isstatus = "confirmed";
                _Nestle_Connect.SaveChanges();
                update_satatus_master(id, "confirmed");
                return Ok();
            }
            else
            {
                return NotFound("ไม่มี");
            }
        }
        [HttpGet]
        public ActionResult CheckPhone(string phone)
        {
            if (phone != "")
            {
                var data = _Nestle_Connect.tb_RegisterHeading.Where(x => x.phone == phone).ToList();
                if (data.Count != 0)
                {
                    return Ok("มีแล้ว");
                }
                else
                {
                    return Ok("ยังไม่มี");
                }
            }
            else { return NotFound(); }
           
        }
        [HttpGet]
        public ActionResult<string> Check_TypeSend(string id_master)
        {
            try
            {
                var data = _Nestle_Connect.tb_master_link.Where(x => x.random_numbe == id_master).ToList();
                if (data.Count !=0)
                {
                    return Ok(data);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return NotFound();
                throw;
            }
        }
        [HttpGet]
        public ActionResult<string> Check_Sms(string phone)
        {
            try
            {
                var data = _Nestle_Connect.tb_master_link.Where(x => x.customer == phone).ToList();
                if (data.Count != 0)
                {
                    return Ok("Yes");
                }
                else
                {
                    return Ok("No");
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
           
        }
        [HttpGet]
        public ActionResult<string> checkRegister(string numberid)
        {
            var data = _Nestle_Connect.tb_RegisterHeading.Where(x => x.id_master == numberid && x.OTP != "").ToList();
            if (data.Count != 0)
            {
                return Ok("Yes");
            }
            else
            {
                return Ok("No");
            }
            
        }
        [HttpGet]
        public ActionResult<tb_province> Get_province()
        {
            try
            {
                var data = _Nestle_Connect.tb_province.OrderBy(x => x.province_name).ToList();
                return Ok(data);
            }
            catch (Exception)
            {

                return NotFound();
            }
           
        }
        [HttpGet]
        public ActionResult<tb_district> Get_district(string province_name)
        {
            try
            {
                var data = _Nestle_Connect.tb_district.Where(x => x.pvnc_name == province_name).OrderBy(x =>x.district_name).ToList();
                return Ok(data);
            }
            catch (Exception)
            {

                return NotFound();
            }

        }
        [HttpGet]
        public ActionResult<tb_sub_district> Get_sub_district(string district_name)
        {
            try
            {
                var data = _Nestle_Connect.tb_sub_district.Where(x => x.district_name == district_name).OrderBy(x => x.sub_district_name).ToList();
                return Ok(data);
            }
            catch (Exception)
            {

                return NotFound();
            }

        }
        [HttpGet]
        public ActionResult<tb_zip_code> Get_zip_code(string sub_district_name)
        {

                var data = _Nestle_Connect.tb_zip_code.Where(x => x.sub_district_name == sub_district_name).OrderBy(x => x.zip_code).ToList();
                return Ok(data);

        }
        [HttpPost]
        public ActionResult<string> login(loig u)
        {
            var data = _Nestle_Connect.tb_user_login.Where(x => x.user_ == u.user_ && x.password_ == u.password_).ToList();
            if (data.Count != 0)
            {
                return Ok(data.ToList());
            }
            else
            {
                return Ok("Not");
            }
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
        static string ConvertStringToHex(string message)
        {
            byte[] bytes = Encoding.BigEndianUnicode.GetBytes(message);
            string hex = BitConverter.ToString(bytes);
            hex = hex.Replace("-", "");
            return hex;
        }

    }
}
