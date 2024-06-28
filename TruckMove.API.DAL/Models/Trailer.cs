using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public  class Trailer : AuditableEntity, IActiveEntity
    {
        public int Id { get; set; }
        public int HookupType { get; set; }
        public int JobId { get; set; }
        public string? HookupLocation { get; set; }
        public string? DropOffLocation { get; set; }
        public string? Rego { get; set; }
        public string? Type { get; set; }

        public virtual HookupType HookupTypeNavigation { get; set; } = null!;
        public virtual Job Job { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
