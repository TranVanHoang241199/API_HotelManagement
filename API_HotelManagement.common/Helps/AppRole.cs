using System.Reflection;

namespace API_HotelManagement.common.Helps
{
    public static class AppRole
    {
        public const string Admin = "Adminstrator";
        public const string Customer = "Customer";
        public const string Manager = "Manager";
        public const string Accountant = "Accountant";
        public const string HR = "Human Resource";
        public const string Warehouse = "Warehouse staff";

        // Phương thức để lấy danh sách các role
        public static List<string> GetAllRoles()
        {
            // Sử dụng reflection để lấy tất cả các hằng số public string từ lớp AppRole
            var fields = typeof(AppRole).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                                         .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                                         .Select(fi => (string)fi.GetRawConstantValue());

            return fields.ToList();
        }
    }
}
