
using Microsoft.EntityFrameworkCore;
using Nestle_service_api.Context;
using Nestle_service_api.Model;
using Nestle_service_api.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nestle_service_api.BL.Inbound
{
    public class CallDetail : BaseBLL, ICallDetail
    {
        private readonly IEfRepository<tb_inbound_case> inboundCaseRepository;
        private readonly IEfRepository<tb_logs_inbound> inboundlogsRepository;
        public CallDetail(IEfRepository<tb_inbound_case> _inboundCaseRepository,
                         IEfRepository<tb_logs_inbound> _inboundlogsRepository)
        {
            inboundCaseRepository = _inboundCaseRepository;
            inboundlogsRepository = _inboundlogsRepository;
        }

        public async Task<bool> AddOrUpdate(InboundCaseModel inboundCase)
        {

            var inboundCasep = inboundCaseRepository.Table.Where(x => x.IsActive && x.case_id == inboundCase.case_id).FirstOrDefault();
            var inb = inboundCaseRepository.Table.Where(x => x.IsActive).OrderByDescending(x => x.case_id).FirstOrDefault();

            if (inboundCasep != null)
            {
                inboundCasep.inbound_call_date = inboundCase.inbound_call_date;
                inboundCasep.case_open_time = inboundCase.case_open_time;
                inboundCasep.case_id = inboundCase.case_id;
                inboundCasep.contact_channel = inboundCase.contact_channel;
                inboundCasep.name = inboundCase.name;
                inboundCasep.surname = inboundCase.surname;
                inboundCasep.contact_number = inboundCase.contact_number;
                inboundCasep.service_group = inboundCase.service_group;
                inboundCasep.service_type = inboundCase.service_type;
                inboundCasep.service_requst_verbatim = inboundCase.service_requst_verbatim;
                inboundCasep.solution = inboundCase.solution;
                inboundCasep.sratus_case = inboundCase.sratus_case;
                inboundCasep.UpdatedBy = UserName;
                inboundCasep.UpdatedDate = DateTime.Now;
                await inboundCaseRepository.UpdateAsync(inboundCasep);
            }
            else
            {
                string CaseId = string.Empty;
                if (inb != null)
                {
                    if (!string.IsNullOrWhiteSpace(inb.case_id))
                    {
                        CaseId = GenerateCaseId(Convert.ToInt32(inb.case_id));
                    }
                    else
                    {
                        CaseId = "001";
                    }
                }
                else
                {
                    CaseId = "001";
                }

                var inbound = new tb_inbound_case
                {
                    inbound_call_date = DateTime.Now,
                    case_open_time = DateTime.Now.ToString("HH:mm:ss tt"),
                    case_id = CaseId,
                    contact_channel = inboundCase.contact_channel,
                    name = inboundCase.name,
                    surname = inboundCase.surname,
                    contact_number = inboundCase.contact_number,
                    service_group = inboundCase.service_group,
                    service_type = inboundCase.service_type,
                    service_requst_verbatim = inboundCase.service_requst_verbatim,
                    solution = inboundCase.solution,
                    sratus_case = inboundCase.sratus_case,
                    CreatedBy = UserName,
                    CreatedDate = DateTime.Now
                };
                await inboundCaseRepository.AddAsync(inbound);
            }


            return true;
        }
        private string GenerateCaseId(int lastAddedId)
        {
            string caseId = Convert.ToString(lastAddedId + 1).PadLeft(3, '0');
            return caseId;
        }
        public async Task<bool> Delete(int id)
        {
            var inboundCase = await inboundCaseRepository.FindByIdAsync(id);
            if (inboundCase == null)
                throw new Exception("Not found inbound Case");

            inboundCase.IsActive = false;
            inboundCase.UpdatedDate = DateTime.Now;
            inboundCase.UpdatedBy = UserName;
            return await inboundCaseRepository.UpdateAsync(inboundCase);

        }
        public async Task<InboundCaseModel> Get(int id)
        {
            var inboundCase = await inboundCaseRepository.Table.Where(x => x.Id == id)
                                          .Select(s => new InboundCaseModel
                                          {
                                              inbound_call_date = s.inbound_call_date,
                                              case_open_time = s.case_open_time,
                                              case_id = s.case_id,
                                              contact_channel = s.contact_channel,
                                              name = s.name,
                                              surname = s.surname,
                                              contact_number = s.contact_number,
                                              service_group = s.service_group,
                                              service_type = s.service_type,
                                              service_requst_verbatim = s.service_requst_verbatim,
                                              solution = s.solution,
                                              sratus_case = s.sratus_case
                                          }).FirstOrDefaultAsync();
            if (inboundCase == null)
                throw new Exception("Not found inbound Case");

            return inboundCase;
        }
        public async Task<ResponseViewModel<InboundCaseModel>> Get(string key, int skip, int take)
        {
            var query = inboundCaseRepository.Table.Where(x => x.IsActive && x.sratus_case != "Close");

            if (!string.IsNullOrEmpty(key))
                query = query.Where(x => x.name.Contains(key) || x.contact_number.Contains(key));

            int total = 0;
            var inboundCases = query.OrderBy(x => x.CreatedDate).Get(out total, skip, take)
                                .Select(s => new InboundCaseModel
                                {
                                    Id = s.Id,
                                    inbound_call_date = s.inbound_call_date,
                                    case_open_time = s.case_open_time,
                                    case_id = s.case_id,
                                    contact_channel = s.contact_channel,
                                    name = s.name,
                                    surname = s.surname,
                                    contact_number = s.contact_number,
                                    service_group = s.service_group,
                                    service_type = s.service_type,
                                    service_requst_verbatim = s.service_requst_verbatim,
                                    solution = s.solution,
                                    sratus_case = s.sratus_case,
                                    number_of_calls = s.number_of_calls
                                });

            return new ResponseViewModel<InboundCaseModel> { data = inboundCases.ToList(), totalCount = total };
        }

        public async Task<bool> AddLog(tb_logs_inbound logsInbound)
        {

            var inboundCase = inboundCaseRepository.Table.Where(x => x.case_id == logsInbound.case_id).FirstOrDefault();

            if (inboundCase != null)
            {
                inboundCase.number_of_calls += 1;
                await inboundCaseRepository.UpdateAsync(inboundCase);
            }

            var inboundlog = inboundlogsRepository.Table.Where(x => x.case_id == logsInbound.case_id).OrderByDescending(x => x.number_of_repeat).FirstOrDefault();
            var inbound = new tb_logs_inbound
            {
                case_id = logsInbound.case_id,
                aqent_name = logsInbound.aqent_name,
                create_date = DateTime.Now,
                number_of_repeat = inboundlog == null ? 1 : inboundlog.number_of_repeat + 1,
                status_of_case = logsInbound.status_of_case,
                status_of_contact = logsInbound.status_of_contact,
                CreatedBy = UserName,
                CreatedDate = DateTime.Now
            };

            await inboundlogsRepository.AddAsync(inbound);

            return true;
        }
    }
}
