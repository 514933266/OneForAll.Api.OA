using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using OneForAll.Core;
using OneForAll.File;
using OneForAll.EFCore;
using OneForAll.Core.Upload;
using OneForAll.Core.Utility;
using OneForAll.Core.Extension;
using OA.Domain.Enums;
using OA.Domain.Models;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;
using OA.Domain.ValueObjects;
using System.Data;
using OA.Domain.ExcelModels;
using NPOI.SS.UserModel;
using NPOI.POIFS.FileSystem;
using OneForAll.Core.DDD;

namespace OA.Domain
{
    /// <summary>
    /// 人员资料
    /// </summary>
    public class OAPersonManager : OABaseManager, IOAPersonManager
    {
        private readonly string UPLOAD_PATH = "upload/tenants/{0}/persons";// 文件存储路径
        private readonly string VIRTUAL_PATH = "/resources/tenants/{0}/persons";// 虚拟路径：根据Startup配置设置,返回给前端访问资源

        private readonly IUploader _uploader;
        private readonly IOAPersonRepository _repository;
        private readonly IOATeamRepository _teamRepository;
        private readonly IOATeamPersonContactRepository _contactRepository;
        private readonly IOAPersonSettingFieldRepository _fieldSettingRepository;

        public OAPersonManager(
            IMapper mapper,
            IUploader uploader,
            IHttpContextAccessor httpContextAccessor,
            IOAPersonRepository repository,
            IOATeamRepository teamRepository,
            IOATeamPersonContactRepository contactRepository,
            IOAPersonSettingFieldRepository fieldSettingRepository) : base(mapper, httpContextAccessor)
        {
            _uploader = uploader;
            _repository = repository;
            _teamRepository = teamRepository;
            _contactRepository = contactRepository;
            _fieldSettingRepository = fieldSettingRepository;
        }

        /// <summary>
        /// 获取人员分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<OAPerson>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            OAPersonOnJobStatusEnum onJobStatus)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var data = await _repository.GetPageAsync(pageIndex, pageSize, key, onJobStatus);

            data.Items.ForEach(e =>
            {
                // 没有保存过档案的，根据默认信息赋值到档案扩展信息
                if (e.ExtendInformationJson.IsNullOrWhiteSpace())
                {
                    e.InitExtendInfomation();
                }
            });
            return data;
        }

        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPerson>> GetListAsync(string key, OAPersonOnJobStatusEnum onJobStatus)
        {
            return await _repository.GetListAsync(key, onJobStatus);
        }

        /// <summary>
        /// 获取人员
        /// </summary>
        /// <param name="id">人员id</param>
        /// <returns>基础信息</returns>
        public async Task<OAPerson> GetAsync(Guid id)
        {
            var data = await _repository.FindAsync(id);
            if (data.ExtendInformationJson.IsNullOrEmpty())
            {
                data.InitExtendInfomation();
            }
            return data;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entities">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(IEnumerable<OAPersonForm> entities)
        {
            var data = _mapper.Map<IEnumerable<OAPersonForm>, IEnumerable<OAPerson>>(entities);
            data.ForEach(e =>
            {
                e.CreatorId = LoginUser.Id;
                e.CreatorName = LoginUser.Name;
                e.SysTenantId = LoginUser.SysTenantId;
                if (e.Id == Guid.Empty) e.Id = Guid.NewGuid();
                if (e.EntryDate == null) e.EntryDate = DateTime.Now;

                e.ReInitExtendInfomation();
            });

            return await ResultAsync(() => _repository.AddRangeAsync(data));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAPersonForm entity)
        {
            var data = await _repository.GetAsync(entity.Name, entity.IdCard ?? "");
            if (data != null)
                return BaseErrType.DataExist;

            data = _mapper.Map<OAPersonForm, OAPerson>(entity);
            data.CreatorId = LoginUser.Id;
            data.CreatorName = LoginUser.Name;
            data.SysTenantId = LoginUser.SysTenantId;
            if (data.EntryDate == null)
                data.EntryDate = DateTime.Now;

            data.InitExtendInfomation(entity.ExtendInformations);

            var teams = await _teamRepository.GetListValidAsync();
            var contacts = await _contactRepository.GetListAsync(w => w.OAPersonId == data.Id);
            if (contacts.Any())
            {
                // 团队和直属主管
                var teamIds = contacts.Select(s => s.OATeamId).ToList();
                var thisTeams = teams.Where(w => teamIds.Contains(w.Id)).ToList();
                var teamLeaderIds = thisTeams.Select(s => s.LeaderId).ToList();
                if (teamLeaderIds.Any())
                {
                    var leaders = await _repository.GetListAsync(teamLeaderIds);
                    data.InitExtendInfomation(thisTeams, leaders);
                }
            }

            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OAPersonForm entity)
        {
            var data = await _repository.FindAsync(entity.Id);
            if (data == null) return BaseErrType.DataNotFound;

            _mapper.Map(entity, data);

            data.InitExtendInfomation(entity.ExtendInformations);
            var teams = await _teamRepository.GetListValidAsync();
            var contacts = await _contactRepository.GetListAsync(w => w.OAPersonId == data.Id);
            if (contacts.Any())
            {
                // 团队和直属主管
                var teamIds = contacts.Select(s => s.OATeamId).ToList();
                var thisTeams = teams.Where(w => teamIds.Contains(w.Id)).ToList();
                var teamLeaderIds = thisTeams.Select(s => s.LeaderId).ToList();
                if (teamLeaderIds.Any())
                {
                    var leaders = await _repository.GetListAsync(teamLeaderIds);
                    data.InitExtendInfomation(thisTeams, leaders);
                }
            }
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OATeamMemberForm entity)
        {
            var data = await _repository.FindAsync(entity.Id);
            if (data == null) return BaseErrType.DataNotFound;

            _mapper.Map(entity, data);

            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">项目id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            var data = await _repository.GetListAsync(ids);
            if (data.Count() < 1) return BaseErrType.DataEmpty;
            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="id">人员id</param>
        /// <param name="url">头像地址</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateHeaderAsync(Guid id, string url)
        {
            var data = await _repository.FindAsync(id);
            if (data == null) return BaseErrType.DataNotFound;

            data.IconUrl = url;
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="id">人员id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>结果</returns>
        public async Task<IUploadResult> UploadFileAsync(Guid id, string filename, Stream file)
        {
            IUploadResult result = new UploadResult();

            var maxSize = 5 * 1024 * 1024;
            var floder = id.ToString("N");
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            var extension = Path.GetExtension(filename);
            filename = name.Append(extension);

            var uploadPath = AppContext.BaseDirectory + Path.Combine(UPLOAD_PATH.Fmt(LoginUser.SysTenantId), floder);
            var virtualPath = Path.Combine(VIRTUAL_PATH.Fmt(LoginUser.SysTenantId), floder, filename);
            if (new ValidateImageType().Validate(filename, file))
            {
                result = await _uploader.WriteAsync(file, uploadPath, filename, maxSize);
                // 设置返回虚拟路径
                if (result.State.Equals(UploadEnum.Success))
                {
                    result.Url = virtualPath;
                }
            }
            else
            {
                result.State = UploadEnum.TypeError;
            }
            return result;
        }

        /// <summary>
        /// 人员离职（批量）
        /// </summary>
        /// <param name="ids">人员id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> LeaveAsync(IEnumerable<Guid> ids)
        {
            var data = await _repository.GetListAsync(ids);
            if (data.Any()) return BaseErrType.DataNotFound;

            using (var tran = new UnitOfWork().BeginTransaction())
            {
                data.ForEach(e =>
                {
                    e.LeaveDate = DateTime.Now;
                    if (e.ExtendInformationJson.IsNullOrEmpty())
                    {
                        e.InitExtendInfomation();
                    }
                    else
                    {
                        var infos = e.ExtendInformationJson.FromJson<IEnumerable<OAPersonExtenInformationFieldVo>>();
                        if (infos.Any())
                        {
                            var leaveDate = infos.FirstOrDefault(w => w.Name == new OAPersonLeaveDateVo().Name);
                            if (leaveDate != null)
                            {
                                leaveDate.Value = e.LeaveDate.Value.ToString("yyyy-MM-dd");
                            }
                        }
                        e.ExtendInformationJson = infos.ToJson();
                    }
                });
            }
            return await ResultAsync(() => _repository.UpdateRangeAsync(data));
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
            var data = await _repository.GetListAsync(onJobStatus, employeeType, employeeStatus, startEntryDate, endEntryDate);
            if (data.Any())
            {
                IWorkbook workbook;
                if (fields.Any())
                {
                    // 选择字段导出
                    var dt = new DataTable();
                    var fieldList = new List<OAPersonSettingField>();
                    var settings = await _fieldSettingRepository.GetListAsync();
                    fields.ForEach(e =>
                    {
                        var columnName = e;
                        var field = settings.FirstOrDefault(w => w.Name == e);
                        if (field != null)
                        {
                            columnName = field.Text;
                            fieldList.Add(field);
                        }
                        dt.Columns.Add(columnName);
                    });

                    var count = fields.Count();
                    data.ForEach(e =>
                    {
                        var index = 0;
                        var row = new string[count];
                        var infos = e.ExtendInformationJson.IsNullOrEmpty() ? new List<OAPersonExtenInformationFieldVo>() : e.ExtendInformationJson.FromJson<IEnumerable<OAPersonExtenInformationFieldVo>>();
                        if (infos.Any())
                        {
                            fieldList.ForEach(e =>
                            {
                                var field = infos.FirstOrDefault(w => w.Name == e.Name);
                                var value = field == null ? "" : field.Value;
                                row[index] = value;
                                index++;
                            });
                            dt.Rows.Add(row);
                        }
                    });
                    workbook = NPOIExcelHelper.Export(dt, FileType.xlsx);
                }
                else
                {
                    // 默认导出
                    var dts = _mapper.Map<IEnumerable<OAPerson>, IEnumerable<OAPersonExcel>>(data);
                    workbook = NPOIExcelHelper.EntityExport(dts, FileType.xlsx);
                }
                var ms = new MemoryStream();
                workbook.Write(ms);
                return ms.ToArray();
            }
            return Array.Empty<byte>();
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public async Task<BaseErrType> ImportExcelAsync(string fileName, Stream fileStream)
        {
            var pass = FileHelper.ValidateFileType<ExcelType>(fileStream);
            if (!pass) return BaseErrType.NotAllow;

            var extension = Path.GetExtension(fileName);
            var fileType = extension == ".xls" ? FileType.xls : FileType.xlsx;
            var dts = NPOIExcelHelper.Import(fileStream, fileType, true);
            if (dts.Count() > 0)
            {
                var dt = dts.First();
                var data = new List<OAPerson>();
                var fields = new List<string>();
                var settings = await _fieldSettingRepository.GetListAsync();

                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    var field = settings.FirstOrDefault(w => w.Text == dt.Columns[i].ToString());
                    var value = field != null ? field.Name : string.Empty;
                    fields.Add(value);
                }

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var infos = new HashSet<OAPersonExtenInformationFieldVo>();
                    for (var j = 0; j < fields.Count; j++)
                    {
                        var field = fields[j];
                        if (!field.IsNullOrEmpty())
                        {
                            infos.Add(new OAPersonExtenInformationFieldVo()
                            {
                                Name = field,
                                Value = dt.Rows[i][j].ToString()
                            });
                        }
                    }

                    if (infos.Any())
                    {
                        var person = new OAPerson()
                        {
                            SysTenantId = LoginUser.SysTenantId
                        };
                        person.InitExtendInfomation(infos);
                        if (person.Job.IsNullOrEmpty())
                        {
                            throw new Exception("职级信息不允许为空");
                        }
                        data.Add(person);
                    }
                }

                if (data.Any())
                {
                    return await ResultAsync(() => _repository.AddRangeAsync(data));
                }
            }
            return BaseErrType.DataEmpty;
        }
    }
}
