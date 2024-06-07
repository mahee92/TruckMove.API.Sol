using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.Models
{
    public partial class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool? IsActive { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? CreatedById { get; set; }

        public virtual User? CreatedBy { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual User? UpdatedBy { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
