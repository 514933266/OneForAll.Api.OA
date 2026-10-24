# OneForAll.Api.OA

> ### ⚠️ 生产 IIS 部署特殊说明（PUT/DELETE 405 与 500.19）
> - **现象**：发布到生产 IIS 后 `DELETE`/`PUT` 返回 `405`（`Allow` 仅 `GET,HEAD,OPTIONS,TRACE`），本机正常。根因：服务器装了 IIS `WebDAVModule`，在请求进入 ASP.NET Core 前拦截 PUT/DELETE。
> - **修复（已入库）**：`OA.Host/web.config`（源码级模板，`dotnet publish` 以它为基合并 `aspNetCore` 节点并保留以下节点）已在 `system.webServer` 下加 `<handlers><remove name="WebDAV" /></handlers>` 与 `<modules><remove name="WebDAVModule" /></modules>`，随每次发布自动生效。
> - **新服务器踩坑**：若服务器把 `system.webServer/modules` 节父级锁定，应用级 `<modules>` 块会触发 `HTTP 500.19`（`0x80070021` 不能在此路径中使用此配置节）。解法（每台新服务器一次性执行、之后长期有效）：管理员运行 `C:\Windows\System32\inetsrv\appcmd.exe unlock config /section:system.webServer/modules`，或 IIS「功能委派」把 Modules 设为可读/可写。应急：临时删除站点 web.config 的 `<modules>` 块即可恢复上线。
> - 详见仓库根 `README.md`。

基于 ASP.NET Core 构建的智能人事管理系统(OA),提供员工管理、团队管理、入职离职流程、个人日程等核心人事功能。

## 技术栈

- **.NET 8.0** / ASP.NET Core
- **Entity Framework Core 8.0** - ORM(SQL Server)
- **Autofac 9.0** - 依赖注入
- **AutoMapper 16.0** - 对象映射
- **JWT Bearer** - 身份认证
- **Quartz.NET 3.15** - 定时任务调度
- **钉钉开放平台 SDK** - 钉钉集成

## 项目结构

```
OneForAll.Api.OA/
├── OA.Host/                 # Web 主机层(API 入口、中间件配置、控制器)
├── OA.Application/          # 应用服务层(业务用例、DTO、Profiles)
├── OA.Domain/               # 领域层(实体、枚举、值对象、领域服务)
├── OA.Repository/           # 数据访问层(EF Core 仓储实现)
├── OA.HttpService/          # 外部 HTTP 服务客户端
└── OA.Public/               # 公共模型和工具类
```

## 核心功能

### 员工管理

- **员工档案管理** - 维护员工基本信息、联系方式、教育背景等
- **入职管理** - 新员工入职流程,包括资料上传、信息录入
- **转正管理** - 试用期转正流程和评估
- **离职管理** - 员工离职申请、审批和交接
- **关怀管理** - 生日、纪念日等员工关怀提醒

### 团队管理

- **组织架构** - 多层级团队结构管理
- **团队成员** - 团队成员分配、调岗、历史记录追踪
- **团队类型** - 自定义团队分类(部门、项目组等)
- **无团队人员** - 管理未分配团队的待分配人员

### 岗位体系

- **岗位管理** - 定义公司岗位体系
- **职级管理** - 职级体系和晋升规则
- **职务管理** - 具体职务定义
- **岗位职责** - 岗位职责描述和管理

### 个人日程

- **日程管理** - 个人工作日程安排
- **日程提醒** - 基于微信公众消息的日程提醒
- **监控任务** - 定时监控即将到期的日程并发送通知

### 系统配置

- **字段配置** - 自定义人员信息展示字段
- **表格列配置** - 个性化表格列显示设置
- **钉钉集成配置** - 钉钉同步和通知配置
- **补卡管理** - 考勤补卡申请和审批

### 定时任务

| 任务 | 周期 | 说明 |
|------|------|------|
| MonitorPersonlScheduleJob | 每 10 分钟 | 监控个人日程并发送微信消息提醒 |

## API 接口

### 员工相关

| 方法 | 路径 | 说明 |
|------|------|------|
| GET | `/api/OAPersons/{pageIndex}/{pageSize}` | 分页查询员工列表 |
| GET | `/api/OAPersons/{id}` | 获取员工详情 |
| POST | `/api/OAPersons` | 创建员工档案 |
| PUT | `/api/OAPersons/{id}` | 更新员工信息 |
| DELETE | `/api/OAPersons/{id}` | 删除员工 |
| PATCH | `/api/OAPersons/{id}/Status` | 变更员工在职状态 |

### 入职管理

| 方法 | 路径 | 说明 |
|------|------|------|
| GET | `/api/OAPersonEntrys/{pageIndex}/{pageSize}` | 分页查询入职记录 |
| POST | `/api/OAPersonEntrys` | 创建入职记录 |
| PUT | `/api/OAPersonEntrys/{id}` | 更新入职信息 |
| DELETE | `/api/OAPersonEntrys/{id}` | 删除入职记录 |

### 离职管理

| 方法 | 路径 | 说明 |
|------|------|------|
| GET | `/api/OAPersonLeaves/{pageIndex}/{pageSize}` | 分页查询离职记录 |
| POST | `/api/OAPersonLeaves` | 创建离职申请 |
| PUT | `/api/OAPersonLeaves/{id}` | 更新离职信息 |
| DELETE | `/api/OAPersonLeaves/{id}` | 删除离职记录 |

### 转正管理

| 方法 | 路径 | 说明 |
|------|------|------|
| GET | `/api/OAPersonFormals/{pageIndex}/{pageSize}` | 分页查询转正记录 |
| POST | `/api/OAPersonFormals` | 创建转正申请 |
| PUT | `/api/OAPersonFormals/{id}` | 更新转正信息 |

### 团队管理

| 方法 | 路径 | 说明 |
|------|------|------|
| GET | `/api/OATeams` | 查询团队列表 |
| GET | `/api/OATeams/{id}` | 获取团队详情 |
| POST | `/api/OATeams` | 创建团队 |
| PUT | `/api/OATeams/{id}` | 更新团队信息 |
| DELETE | `/api/OATeams/{id}` | 删除团队 |
| POST | `/api/OATeamMembers` | 添加团队成员 |
| DELETE | `/api/OATeamMembers/{id}` | 移除团队成员 |
| GET | `/api/OATeamMemberHistorys` | 查询团队成员变动历史 |

### 岗位管理

| 方法 | 路径 | 说明 |
|------|------|------|
| GET/POST/PUT/DELETE | `/api/OAJobs` | 岗位 CRUD |
| GET/POST/PUT/DELETE | `/api/OAJobLevels` | 职级 CRUD |
| GET/POST/PUT/DELETE | `/api/OAJobTypes` | 职务 CRUD |
| GET/POST/PUT/DELETE | `/api/OAJobDutys` | 岗位职责 CRUD |

### 个人日程

| 方法 | 路径 | 说明 |
|------|------|------|
| GET | `/api/OAPersonalSchedules/{pageIndex}/{pageSize}` | 分页查询日程 |
| POST | `/api/OAPersonalSchedules` | 创建日程 |
| PUT | `/api/OAPersonalSchedules/{id}` | 更新日程 |
| DELETE | `/api/OAPersonalSchedules/{id}` | 删除日程 |

### 系统配置

| 方法 | 路径 | 说明 |
|------|------|------|
| GET/POST/PUT | `/api/OAPersonSettings` | 人员字段配置 |
| GET/POST/PUT | `/api/OATableColumnSettings` | 表格列配置 |
| GET/POST/PUT | `/api/OADingDingSettings` | 钉钉集成配置 |
| POST | `/api/OARepair` | 提交补卡申请 |

### 其他接口

| 方法 | 路径 | 说明 |
|------|------|------|
| GET | `/api/OADashboard` | 获取仪表盘统计数据 |
| GET | `/api/OANoTeamPersons` | 查询无团队人员 |
| GET | `/api/OAPersonTeamInfos` | 查询人员团队关联信息 |
| GET/POST/PUT/DELETE | `/api/OAPersonalFiles` | 个人文件管理 |
| GET/POST/PUT/DELETE | `/api/OAPersonalEntryFiles` | 入职文件管理 |
| GET | `/api/OAPersonCares` | 查询员工关怀记录 |
| GET | `/api/OAPersonContracts` | 查询劳动合同记录 |

## 数据库

使用 SQL Server,数据库名 `OneForAll.OA`,主要表:

| 表名 | 说明 |
|------|------|
| oa_person | 员工档案(基本信息、联系方式、教育背景等) |
| oa_person_entry | 入职记录 |
| oa_person_leave | 离职记录 |
| oa_person_formal | 转正记录 |
| oa_team | 团队信息 |
| oa_mid_team_person | 团队成员关联表 |
| oa_team_member_history | 团队成员变动历史 |
| oa_job | 岗位定义 |
| oa_job_level | 职级定义 |
| oa_job_type | 职务定义 |
| oa_job_duty | 岗位职责 |
| oa_personal_schedule | 个人日程 |
| oa_person_setting | 人员字段配置 |
| oa_person_setting_field | 字段配置明细 |
| oa_table_column_setting | 表格列配置 |
| oa_dingding_setting | 钉钉集成配置 |
| oa_repair | 补卡记录 |
| oa_person_care | 员工关怀记录 |
| oa_person_contract | 劳动合同记录 |

## 配置说明

`appsettings.json` 关键配置项:

```json
{
  "ConnectionStrings": {
    "Default": "SQL Server 连接字符串"
  },
  "Auth": {
    "JwtKey": "JWT 签名密钥",
    "Issuer": "令牌签发地址",
    "ClientId": "客户端ID",
    "ClientSecret": "客户端密钥",
    "ClientName": "智能人事",
    "ClientCode": "OneForAll.OA"
  },
  "Cors": {
    "Origins": ["*"]
  },
  "HttpService": {
    "SysBase": "http://localhost:5082",
    "SysLog": "http://localhost:5084",
    "SysUms": "http://localhost:5085",
    "SysJob": "http://localhost:5087"
  },
  "Quartz": {
    "IsEnabled": false,
    "AppId": "OneForAll.OA",
    "AppSecret": "OneForAll",
    "GroupName": "智能人事",
    "NodeName": "http://localhost:5086",
    "ScheduleJobs": [
      {
        "TypeName": "MonitorPersonlScheduleJob",
        "Cron": "0 0/10 * * * ?",
        "Remark": "监控个人日程并发送对应消息提醒,每10分钟执行一次"
      }
    ]
  },
  "Wxgzh": {
    "ClientId": "OneForAllWxgzh",
    "ScheduleMsgTemplateId": "微信消息模板ID"
  }
}
```

| 配置项 | 说明 |
|--------|------|
| `ConnectionStrings.Default` | SQL Server 数据库连接字符串 |
| `Auth.JwtKey` | JWT Token 签名密钥 |
| `Auth.Issuer` | Token 签发者地址 |
| `HttpService.*` | 微服务体系中其他服务的地址 |
| `Quartz.IsEnabled` | 是否启用 Quartz 定时任务 |
| `Quartz.ScheduleJobs` | 定时任务配置列表 |
| `Wxgzh.ClientId` | 微信公众号客户端ID |
| `Wxgzh.ScheduleMsgTemplateId` | 日程提醒消息模板ID |

## 外部服务依赖

本服务作为 OneForAll 微服务体系的一部分,依赖以下服务:

| 服务 | 配置键 | 说明 |
|------|--------|------|
| SysBase | `HttpService.SysBase` | 基础系统服务(用户认证、权限等) |
| SysLog | `HttpService.SysLog` | 日志服务 |
| SysUms | `HttpService.SysUms` | 消息服务(发送微信通知等) |
| SysJob | `HttpService.SysJob` | 定时任务调度服务 |

## 钉钉集成

系统支持与钉钉平台集成,可实现:

- 从钉钉同步组织架构和人员信息
- 通过钉钉发送工作通知
- 钉钉免登集成

需在 `OADingDingSettings` 中配置钉钉应用的 AppKey 和 AppSecret。

## 启动运行

1. 确保 SQL Server 已就绪,并更新 `appsettings.json` 中的连接字符串
2. 配置 JWT 认证参数(与统一认证中心一致)
3. 如需使用定时任务,设置 `Quartz.IsEnabled` 为 `true`
4. 如需钉钉集成,配置钉钉应用信息

```bash
dotnet restore
dotnet run --project OA.Host
```

服务默认监听端口在 `launchSettings.json` 中配置。

## 开发说明

### 分层架构

- **Domain 层** - 包含实体、值对象、枚举、领域服务和仓储接口
- **Repository 层** - EF Core 数据访问实现
- **Application 层** - 应用服务、DTO、业务逻辑编排
- **Host 层** - Web API 控制器、中间件、配置文件

### AutoMapper 配置

所有 DTO 与实体的映射配置位于 `OA.Host/Profiles/` 目录下,采用 Profile 类进行集中管理。

### 数据库迁移

使用 EF Core Migration 管理数据库架构变更:

```bash
dotnet ef migrations add MigrationName -p OA.Host -s OA.Host
dotnet ef database update -p OA.Host -s OA.Host
```

### 文件上传

上传文件存储在项目根目录的 `upload/` 文件夹下,支持员工照片、入职资料等文件管理。