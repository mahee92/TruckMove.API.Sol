using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TruckMove.API.DAL.Models
{
    public class UserModel : IActiveEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedById { get; set; }

        public int? CreatedById { get; set; }

        public virtual ICollection<CompanyModel> CreatedCompanies { get; set; }
        public virtual ICollection<CompanyModel> UpdatedCompanies { get; set; }

        public virtual ICollection<ContactModel> CreatedContacts { get; set; }
        public virtual ICollection<ContactModel> UpdatedContacts { get; set; }

        public virtual ICollection<UserRoleModel> CreatedRoles { get; set; }
        public virtual ICollection<UserRoleModel> UpdatedRoles { get; set; }

        public virtual ICollection<UserRoleModel> UserRoles { get; set; }

        public virtual ICollection<JobModel> CreatedJobs { get; set; }
        public virtual ICollection<JobModel> UpdatedJobs { get; set; }

        public virtual ICollection<JobModel> ControlledJobs { get; set; }
    }
}
