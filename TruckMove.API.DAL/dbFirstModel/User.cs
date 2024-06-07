using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirstModel
{
    public partial class User
    {
        public User()
        {
            CompanyCreatedBies = new HashSet<Company>();
            CompanyUpdatedBies = new HashSet<Company>();
            ContactCreatedBies = new HashSet<Contact>();
            ContactUpdatedBies = new HashSet<Contact>();
            InverseCreatedBy = new HashSet<User>();
            InverseUpdatedBy = new HashSet<User>();
            JobControllerNavigations = new HashSet<Job>();
            JobCreatedBies = new HashSet<Job>();
            JobUpdatedBies = new HashSet<Job>();
            UserRoleCreatedBies = new HashSet<UserRole>();
            UserRoleUpdatedBies = new HashSet<UserRole>();
            UserRoleUsers = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool? IsActive { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? CreatedById { get; set; }

        public virtual User? CreatedBy { get; set; }
        public virtual User? UpdatedBy { get; set; }
        public virtual ICollection<Company> CompanyCreatedBies { get; set; }
        public virtual ICollection<Company> CompanyUpdatedBies { get; set; }
        public virtual ICollection<Contact> ContactCreatedBies { get; set; }
        public virtual ICollection<Contact> ContactUpdatedBies { get; set; }
        public virtual ICollection<User> InverseCreatedBy { get; set; }
        public virtual ICollection<User> InverseUpdatedBy { get; set; }
        public virtual ICollection<Job> JobControllerNavigations { get; set; }
        public virtual ICollection<Job> JobCreatedBies { get; set; }
        public virtual ICollection<Job> JobUpdatedBies { get; set; }
        public virtual ICollection<UserRole> UserRoleCreatedBies { get; set; }
        public virtual ICollection<UserRole> UserRoleUpdatedBies { get; set; }
        public virtual ICollection<UserRole> UserRoleUsers { get; set; }
    }
}
