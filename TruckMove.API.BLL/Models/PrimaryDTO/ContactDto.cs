using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Models.PrimaryDTO
{
    public class ContactDto
    {
        [Key]     
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
       
        //public string CompanyName { get; set; }

    }
}
