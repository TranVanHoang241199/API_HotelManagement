namespace API_HotelManagement.common.Helps
{
    /// <summary>
    /// Hỗ trợ cho 
    /// </summary>
    public enum EBusinessAreas
    {
        Male,
        Female,
        Other
    }

    /// <summary>
    /// Quyền {Chưa sử dụng}
    /// </summary>
    public enum ERole
    {
        Adminstrator,
        Customer,
        Manager,
        Accountant,
        HR,
        Warehouse,
    }

    /// <summary>
    /// Hỗ trợ cho trạng thái dịch vụ
    /// </summary>
    public enum EStatusService
    {
        Active,
        End,
        maintenance
    }

    /// <summary>
    /// Hỗ trợ trạng thái phòng
    /// </summary>
    public enum EStatusRoom
    {
        Active,
        Stop,
        End,
        maintenance
    }
}