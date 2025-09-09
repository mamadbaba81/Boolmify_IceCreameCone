    namespace Boolmify.Models;

    public class UserRole
    {
        //Foreign key user
        public int UserID { get; set; }

        public AppUser  AppUser { get; set; }
        
        //forigen key Role
        public int RoleId { get; set; }

        public Role Role { get; set; } = default!;
        
        

    }