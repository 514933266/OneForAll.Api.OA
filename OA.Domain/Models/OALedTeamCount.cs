namespace OA.Domain.Models
{
    /// <summary>
    /// 当前登录用户管辖的组织人数
    /// </summary>
    public class OALedTeamCount
    {
        /// <summary>
        /// 我管理的部门人数
        /// </summary>
        public int TeamCount { get; set; }

        /// <summary>
        /// 我管理的群组人数
        /// </summary>
        public int GroupCount { get; set; }
    }
}
