using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using OneForAll.Core;
using OneForAll.Core.Upload;
using OA.Domain.Enums;
using OA.Domain.Models;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Application.Dtos;
using OA.Domain.AggregateRoots;
using OA.Application.Interfaces;
using OneForAll.Core.Extension;
using System.Security.Policy;

namespace OA.Application
{
    /// <summary>
    /// 人员资料
    /// </summary>
    public class OAPersonService : IOAPersonService
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonManager _manager;
        public OAPersonService(
            IMapper mapper,
            IOAPersonManager personManager)
        {
            _mapper = mapper;
            _manager = personManager;
        }

        /// <summary>
        /// 获取人员分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<OAPersonDto>> GetPageAsync(int pageIndex, int pageSize, string key, OAPersonOnJobStatusEnum onJobStatus)
        {
            var data = await _manager.GetPageAsync(pageIndex, pageSize, key, onJobStatus);
            var items = _mapper.Map<IEnumerable<OAPerson>, IEnumerable<OAPersonDto>>(data.Items);
            return new PageList<OAPersonDto>(data.Total, data.PageIndex, data.PageSize, items);
        }

        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>分页列表</returns>
        public async Task<IEnumerable<OAPersonDto>> GetListAsync(string key, OAPersonOnJobStatusEnum onJobStatus)
        {
            var data = await _manager.GetListAsync(key, onJobStatus);
            return _mapper.Map<IEnumerable<OAPerson>, IEnumerable<OAPersonDto>>(data);
        }

        /// <summary>
        /// 获取人员（基础信息）
        /// </summary>
        /// <param name="id">人员id</param>
        /// <returns>基础信息</returns>
        public async Task<OAPersonDto> GetAsync(Guid id)
        {
            var data = await _manager.GetAsync(id);
            return _mapper.Map<OAPerson, OAPersonDto>(data);
        }

        /// <summary>
        /// 获取人员统计数据
        /// </summary>
        /// <returns></returns>
        public async Task<OAPersonStatisticDto> GetStatisticsAsync()
        {
            var data = await _manager.GetListAsync(string.Empty, OAPersonOnJobStatusEnum.OnJob);
            return new OAPersonStatisticDto()
            {
                TotalOnJobCount = data.Count(),
                NormalCount = data.Where(w => w.EmployeeType == "全职").Count(),
                InternCount = data.Where(w => w.EmployeeType == "兼职").Count(),
                TrialCount = data.Where(w => w.EmployeeStatus == "实习生").Count(),
            };
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAPersonForm entity)
        {
            return await _manager.AddAsync(entity);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OAPersonForm entity)
        {
            return await _manager.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">人员id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            return await _manager.DeleteAsync(ids);
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="id">人员id</param>
        /// <param name="url">头像地址</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateHeaderAsync(Guid id, string url)
        {
            return await _manager.UpdateHeaderAsync(id, url);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        public async Task<IUploadResult> UploadFileAsync(Guid id, string filename, Stream file)
        {
            return await _manager.UploadFileAsync(id, filename, file);
        }

        /// <summary>
        /// 人员离职（批量）
        /// </summary>
        /// <param name="ids">人员id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> LeaveAsync(IEnumerable<Guid> ids)
        {
            return await _manager.LeaveAsync(ids);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <param name="employeeStatus">员工状态</param>
        /// <param name="employeeType">员工类型</param>
        /// <param name="fields">选择导出字段</param>
        /// <param name="startEntryDate">开始入职时间</param>
        /// <param name="endEntryDate">结束入职时间</param>
        /// <returns>文件流</returns>
        public async Task<byte[]> ExportExcelAsync(OAPersonOnJobStatusEnum onJobStatus, string employeeType, string employeeStatus, IEnumerable<string> fields, DateTime? startEntryDate, DateTime? endEntryDate)
        {
            return await _manager.ExportExcelAsync(onJobStatus, employeeType, employeeStatus, fields, startEntryDate, endEntryDate);
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public async Task<BaseErrType> ImportExcelAsync(string fileName, Stream fileStream)
        {
            return await _manager.ImportExcelAsync(fileName, fileStream);
        }
    }
}
