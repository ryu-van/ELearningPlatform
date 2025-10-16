using E_learning_platform.Models;

namespace E_learning_platform.Data.SeedData
{
    public class RoleSeedData
    {
        public static List<Role> DefaultRoles => new ()
        {
             new Role { Name = "Admin", Description = "Quản trị hệ thống", IsActive = true },
            new Role { Name = "Giảng viên", Description = "Quản lý khóa học và bài giảng", IsActive = true },
            new Role { Name = "Học viên", Description = "Người tham gia học tập", IsActive = true },
            new Role { Name = "Nhân viên", Description = "Hỗ trợ và vận hành hệ thống", IsActive = true }
        };
    }
}
