using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public class CompanyModel : IActiveEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)] 
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(20)] 
        public string PhoneNumber { get; set; }

       
        [Column(TypeName = "nvarchar(max)")]
        public string CompanyAddress { get; set; }

        [Required]
        [MaxLength(100)] 
        public string PrimaryEmail { get; set; }

        [Required]
        [MaxLength(100)]
        public string AccountsEmail { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string CompanyStreetAddress { get; set; }

        [Required]
        [StringLength(11)] 
        public string CompanyABN { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public ICollection<ContactModel> Contacts { get; set; }

    }
}
