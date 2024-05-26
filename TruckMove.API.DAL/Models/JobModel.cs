using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public class JobModel :AuditableEntity , IActiveEntity
    {


        [Key]
        public int Id { get; set; }

        public int JobId { get; set; }



        [AllowNull]
        public int? Controller { get; set; }

        public UserModel ControlledUser { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public CompanyModel Company { get; set; }


        [AllowNull]
        public int? ContactId { get; set; }
        public ContactModel Contact { get; set; }


        public bool IsActive { get; set; }



    }
}
