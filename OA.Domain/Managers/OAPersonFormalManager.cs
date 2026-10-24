using AutoMapper;
using OA.Domain.Aggregates;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OA.Domain.Repositorys;
using OA.Domain.ValueObjects;
using OneForAll.Core;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Managers
{
    /// <summary>
    /// 员工转正管理
    /// </summary>
    public class OAPersonFormalManager : OADomainManager, IOAPersonFormalManager
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonRepository _personRepository;
        public OAPersonFormalManager(
            IMapper mapper,
            ICurrentUser currentUser,
            IOAPersonRepository personRepository) : base(currentUser)
        {
            _mapper = mapper;
            _personRepository = personRepository;
        }

        /// <summary>
        /// 办理转正
        /// </summary>
        /// <param name="id">数据id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> ConfirmAsync(Guid id, OAPersonFormalConfirmForm form)
        {
            var data = await _personRepository.FindAsync(id);
            if (data == null) return BaseErrType.DataNotFound;

            var statusVal = OAPersonEmployeeStatusVo.GetDefaultSelection().Value;
            data.EmployeeStatus = statusVal;
            var infos = data.ExtendInformationJson.FromJson<List<OAPersonExtenInformationFieldVo>>();
            if (infos.Any())
            {
                var status = infos.FirstOrDefault(w => w.Name == new OAPersonEmployeeStatusVo().Name);
                var date = infos.FirstOrDefault(w => w.Name == new OAPersonActualEntryDateVo().Name);
                if (status != null)
                    status.Value = statusVal;
                if (date != null)
                    date.Value = form.ActualEntryDate.ToString();

                data.ExtendInformationJson = infos.ToJson();
            }
            return await ResultAsync(() => _personRepository.UpdateAsync(data));
        }
    }
}
