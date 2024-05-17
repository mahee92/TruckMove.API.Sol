﻿using System;
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
    public class ContactModel : IActiveEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ContactName { get; set; }

        
        public string? ContactMobileNumber { get; set; }

        public string? ContactLandline { get; set; }

        [EmailAddress]
        public string? ContactsEmail { get; set; }

        
        public string? ContactStreetAddress { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public CompanyModel Company { get; set; }
       
        [Required]
        public bool IsActive { get; set; }


        public DateTime? CreatedDate { get; set; }

    }
}
