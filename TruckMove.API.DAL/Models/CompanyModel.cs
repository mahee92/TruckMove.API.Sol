using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.DAL.Models
{
    public class CompanyModel
    {
        [Key]
        public int Id { get; set; }
       
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }   
    }
}
