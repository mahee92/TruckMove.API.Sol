﻿using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class UserRole : AuditableEntity, IActiveEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
       
        public virtual Role Role { get; set; } = null!;
     
        public virtual User User { get; set; } = null!;
    }
}
