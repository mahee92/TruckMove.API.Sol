using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Models.PrimaryDTO
{
    public class ContactUpdateDto
    {
        [Key]
        public int Id { get; set; }

        public string ContactName { get; set; }


        public string? ContactMobileNumber { get; set; }

        public string? ContactLandline { get; set; }

        [EmailAddress]
        public string? ContactsEmail { get; set; }


        public string? ContactStreetAddress { get; set; }


        public int CompanyId { get; set; }

        public string? Image { get; set; }

    }
}
