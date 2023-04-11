

namespace Jh.Core.Enums
{
    public enum EventType
    {
        Get,
        Post,
        Put,
        Delete
    }
    /// <summary>
    /// 
    /// </summary>
    public enum UserLevels
    {
        Custom = 0,
        #region Root
        RootAdmin,
        RootDirector,
        RootHeadManagement,
        RootHeadDepartment,
        RootEmployee,
        #endregion

        #region Ministry
        MainAdmin = 11,
        MainDirector,
        MainHeadManagement,
        MainHeadDepartment,
        MainEmployee,
        #endregion

        #region Agent
        AgentAdmin = 101,
        AgentDirector,
        AgentHeadManagement,
        AgentHeadDepartment,
        AgentEmployee,
        #endregion

        #region District
        DistrictAdmin = 1001,
        DistrictDirector,
        DistrictHeadManagement,
        DistrictHeadDepartment,
        DistrictEmployee,
        #endregion
    }
}
