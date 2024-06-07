using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class JobDto
    {
        [Key]
        public int Id { get; set; }

        public int JobId { get; set; }

        [AllowNull]
        public int? Controller { get; set; }


        [Required]
        public int CompanyId { get; set; }


        [Required]       
        public string Pickuplocation { get; set; }

        [Required]        
        public string Dropofflocation { get; set; }

        public int? CreatedById { get; set; }
    }
}
