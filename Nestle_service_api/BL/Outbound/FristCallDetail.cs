
using Microsoft.EntityFrameworkCore;
using Nestle_service_api.Context;
using Nestle_service_api.Model;
using Nestle_service_api.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nestle_service_api.BL.Outbound
{
    public class FristCallDetail : BaseBLL, IFristCallDetail
    {
        private readonly IEfRepository<tb_outbound_frist_call> fristcallRepository;
        private readonly IEfRepository<tb_outbound_second_call> secondcallRepository;
        private readonly IEfRepository<tb_logs_outbound> outboundlogsRepository;
        private readonly Nestle_Connect nestle_Connect;
        private readonly SPContext context;
        public FristCallDetail(IEfRepository<tb_outbound_frist_call> _fristcallRepository,
                         IEfRepository<tb_outbound_second_call> _secondcallRepository,
                         IEfRepository<tb_logs_outbound> _outboundlogsRepository,
                         Nestle_Connect _nestle_Connect,
                         SPContext _context)
        {
            fristcallRepository = _fristcallRepository;
            secondcallRepository = _secondcallRepository;
            outboundlogsRepository = _outboundlogsRepository;
            nestle_Connect = _nestle_Connect;
            context = _context;
        }

        public async Task<bool> AddOrUpdate(FristCallModel fristCallModel)
        {

            var fristcall = fristcallRepository.Table.Where(x => x.IsActive && x.Id == fristCallModel.Id).FirstOrDefault();
            if (fristcall != null)
            {
                fristcall.ob_date = fristCallModel.ob_date;
                fristcall.ob_time = fristCallModel.ob_time;
                fristcall.contact_status = fristcall.contact_status;
                fristcall.consurmer_name = fristcall.consurmer_name;
                fristcall.consurmer_surmer = fristcall.consurmer_surmer;
                fristcall.owner_mobile_number = fristcall.owner_mobile_number;
                fristcall.use_discount_code = fristcall.use_discount_code;
                fristcall.discount_code_for = fristcall.discount_code_for;
                fristcall.discount_code_exp_date = fristcall.discount_code_exp_date;
                fristcall.interested_brand_ambassador = fristcall.interested_brand_ambassador;
                fristcall.tellscore_registration_status = fristcall.tellscore_registration_status;
                fristcall.UpdatedBy = UserName;
                fristcall.UpdatedDate = DateTime.Now;
                await fristcallRepository.UpdateAsync(fristcall);
            }
            else
            {
                var _fristcall = new tb_outbound_frist_call
                {
                    ob_date = DateTime.Now,
                    ob_time = DateTime.Now.ToString("HH:mm:ss tt"),
                    contact_status = fristcall.contact_status,
                    consurmer_name = fristcall.consurmer_name,
                    consurmer_surmer = fristcall.consurmer_surmer,
                    owner_mobile_number = fristcall.owner_mobile_number,
                    use_discount_code = fristcall.use_discount_code,
                    discount_code_for = fristcall.discount_code_for,
                    discount_code_exp_date = fristcall.discount_code_exp_date,
                    interested_brand_ambassador = fristcall.interested_brand_ambassador,
                    tellscore_registration_status = fristcall.tellscore_registration_status,
                    UpdatedBy = UserName,
                    UpdatedDate = DateTime.Now,
                    CreatedBy = UserName,
                    CreatedDate = DateTime.Now
                };

                await fristcallRepository.AddAsync(_fristcall);
            }

            return true;
        }

        public async Task<bool> AddOrUpdate(SecondCallModel secondCallModel)
        {

            var secondcall = secondcallRepository.Table.Where(x => x.IsActive && x.Id == secondCallModel.Id).FirstOrDefault();
            if (secondcall != null)
            {
                secondcall.ob_date = DateTime.Now;
                secondcall.ob_time = DateTime.Now.ToString("HH:mm:ss tt");
                secondcall.contact_status = secondCallModel.contact_status;
                secondcall.consurmer_name = secondCallModel.consurmer_name;
                secondcall.consurmer_surmer = secondCallModel.consurmer_surmer;
                secondcall.owner_mobile_number = secondCallModel.owner_mobile_number;
                secondcall.regietered_yet = secondCallModel.regietered_yet;
                secondcall.interested_brand_ambassador = secondCallModel.interested_brand_ambassador;
                secondcall.tellscore_registration_status = secondCallModel.tellscore_registration_status;
                secondcall.reasons_register = secondCallModel.reasons_register;
                secondcall.UpdatedBy = UserName;
                secondcall.UpdatedDate = DateTime.Now;
                await secondcallRepository.UpdateAsync(secondcall);
            }
            else
            {
                var _secondcall = new tb_outbound_second_call
                {
                    ob_date = secondCallModel.ob_date,
                    ob_time = secondCallModel.ob_time,
                    contact_status = secondCallModel.contact_status,
                    consurmer_name = secondCallModel.consurmer_name,
                    consurmer_surmer = secondCallModel.consurmer_surmer,
                    owner_mobile_number = secondCallModel.owner_mobile_number,
                    regietered_yet = secondCallModel.regietered_yet,
                    interested_brand_ambassador = secondCallModel.interested_brand_ambassador,
                    tellscore_registration_status = secondCallModel.tellscore_registration_status,
                    reasons_register = secondCallModel.reasons_register,
                    UpdatedBy = UserName,
                    UpdatedDate = DateTime.Now,
                    CreatedBy = UserName,
                    CreatedDate = DateTime.Now
                };

                await secondcallRepository.AddAsync(_secondcall);
            }

            return true;
        }
        public async Task<bool> Delete(int id)
        {
            var inboundCase = await fristcallRepository.FindByIdAsync(id);
            if (inboundCase == null)
                throw new Exception("Not found inbound Case");

            inboundCase.IsActive = false;
            inboundCase.UpdatedDate = DateTime.Now;
            inboundCase.UpdatedBy = UserName;
            return await fristcallRepository.UpdateAsync(inboundCase);

        }
        public async Task<FristCallModel> GetFristCall(int id)
        {
            var fristcall = await fristcallRepository.Table.Where(x => x.Id == id)
                                          .Select(s => new FristCallModel
                                          {
                                              Id = s.Id,
                                              ob_date = s.ob_date,
                                              ob_time = s.ob_time,
                                              contact_status = s.contact_status,
                                              consurmer_name = s.consurmer_name,
                                              consurmer_surmer = s.consurmer_surmer,
                                              owner_mobile_number = s.owner_mobile_number,
                                              use_discount_code = s.use_discount_code,
                                              discount_code_for = s.discount_code_for,
                                              discount_code_exp_date = s.discount_code_exp_date,
                                              interested_brand_ambassador = s.interested_brand_ambassador,
                                              tellscore_registration_status = s.tellscore_registration_status
                                          }).FirstOrDefaultAsync();
            if (fristcall == null)
                throw new Exception("Not found frist call");

            return fristcall;
        }
        public async Task<SecondCallModel> GetSecondCall(int id)
        {
            var secondcall = await secondcallRepository.Table.Where(x => x.Id == id)
                                          .Select(s => new SecondCallModel
                                          {
                                              Id = s.Id,
                                              ob_date = s.ob_date,
                                              ob_time = s.ob_time,
                                              contact_status = s.contact_status,
                                              consurmer_name = s.consurmer_name,
                                              consurmer_surmer = s.consurmer_surmer,
                                              owner_mobile_number = s.owner_mobile_number,
                                              regietered_yet = s.regietered_yet,
                                              interested_brand_ambassador = s.interested_brand_ambassador,
                                              reasons_register = s.reasons_register,
                                              tellscore_registration_status = s.tellscore_registration_status
                                          }).FirstOrDefaultAsync();
            if (secondcall == null)
                throw new Exception("Not found second call");

            return secondcall;
        }
        public async Task<ResponseViewModel<FristCallModel>> GetFristCallAll(string key, int skip, int take)
        {
            var query = fristcallRepository.Table.Where(x => x.IsActive);

            if (!string.IsNullOrEmpty(key))
                query = query.Where(x => x.consurmer_name.Contains(key) || x.consurmer_surmer.Contains(key));

            int total = 0;
            var fristcall = query.OrderBy(x => x.CreatedDate).Get(out total, skip, take)
                                .Select(s => new FristCallModel
                                {
                                    Id = s.Id,
                                    ob_date = s.ob_date,
                                    ob_time = s.ob_time,
                                    contact_status = s.contact_status,
                                    consurmer_name = s.consurmer_name,
                                    consurmer_surmer = s.consurmer_surmer,
                                    owner_mobile_number = s.owner_mobile_number,
                                    use_discount_code = s.use_discount_code,
                                    discount_code_for = s.discount_code_for,
                                    discount_code_exp_date = s.discount_code_exp_date,
                                    interested_brand_ambassador = s.interested_brand_ambassador,
                                    tellscore_registration_status = s.tellscore_registration_status
                                });

            return new ResponseViewModel<FristCallModel> { data = fristcall.ToList(), totalCount = total };
        }

        public async Task<ResponseViewModel<SecondCallModel>> GetSecondCallAll(string key, int skip, int take)
        {
            var query = secondcallRepository.Table.Where(x => x.IsActive);

            if (!string.IsNullOrEmpty(key))
                query = query.Where(x => x.consurmer_name.Contains(key) || x.consurmer_surmer.Contains(key));

            int total = 0;
            var secondcall = query.OrderBy(x => x.CreatedDate).Get(out total, skip, take)
                                .Select(s => new SecondCallModel
                                {
                                    Id = s.Id,
                                    ob_date = s.ob_date,
                                    ob_time = s.ob_time,
                                    contact_status = s.contact_status,
                                    consurmer_name = s.consurmer_name,
                                    consurmer_surmer = s.consurmer_surmer,
                                    owner_mobile_number = s.owner_mobile_number,
                                    regietered_yet = s.regietered_yet,
                                    interested_brand_ambassador = s.interested_brand_ambassador,
                                    reasons_register = s.reasons_register,
                                    tellscore_registration_status = s.tellscore_registration_status
                                });

            return new ResponseViewModel<SecondCallModel> { data = secondcall.ToList(), totalCount = total };
        }

        public async Task<bool> AddLog(tb_logs_outbound logsInbound)
        {
            var registerHeading = nestle_Connect.tb_RegisterHeading.Where(x => x.id_master == logsInbound.case_id).FirstOrDefault();

            if (registerHeading != null)
            {
                registerHeading.number_of_calls += 1;
                nestle_Connect.tb_RegisterHeading.Update(registerHeading);
                nestle_Connect.SaveChanges();
            }

            var outboundlogs = outboundlogsRepository.Table.Where(x => x.case_id == logsInbound.case_id).OrderByDescending(x => x.number_of_repeat).FirstOrDefault();

            var inbound = new tb_logs_outbound
            {
                case_id = logsInbound.case_id,
                aqent_name = logsInbound.aqent_name,
                create_date = DateTime.Now,
                number_of_repeat = outboundlogs == null ? 1 : outboundlogs.number_of_repeat + 1,
                status_of_case = logsInbound.status_of_case,
                status_of_contact = logsInbound.status_of_contact,
                CreatedBy = UserName,
                CreatedDate = DateTime.Now
            };

            await outboundlogsRepository.AddAsync(inbound);

            return true;
        }

        public async Task<ResponseViewModel<OutboundCallViewModel>> GetOutboundCallDetailAsync(string KeywordSearch, int PageNumber)
        {
            try
            {
                int total = 0;
                var Results = context.Set<OutboundCallViewModel>().FromSqlRaw("EXEC dbo.sp_GetAllOutboundCall @KeywordSearch={0}, @PageNumber={1}", KeywordSearch, PageNumber).ToList();

                return new ResponseViewModel<OutboundCallViewModel> { data = Results.ToList(), totalCount = total }; ;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> ExecuteConsumerSegment(string id_master)
        {
            try
            {
                var Result = await context.Database.ExecuteSqlRawAsync("EXEC dbo.sp_i_consumer_segment @id_master={0}", id_master);

                return Result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
