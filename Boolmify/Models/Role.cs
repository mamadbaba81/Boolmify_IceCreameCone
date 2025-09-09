    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Models;

    public class Role
    {
        public int RoleId { get; set; }
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; } = default!;

        public List<UserRole> UserRoles { get; set; } = new();
    }  