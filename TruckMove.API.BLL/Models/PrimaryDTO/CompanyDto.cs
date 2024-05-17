using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Models.Primary
{
    public class CompanyDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }

       
        [MaxLength(20)]
        public string PhoneNumber { get; set; }


        [Column(TypeName = "nvarchar(max)")]
        public string CompanyAddress { get; set; }

        
        [MaxLength(100)]
        [EmailAddress]
        public string PrimaryEmail { get; set; }

        
        [MaxLength(100)]
        [EmailAddress]
        public string AccountsEmail { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string CompanyStreetAddress { get; set; }

        
        [StringLength(11)]
        public string CompanyABN { get; set; }

        public ICollection<ContactModel> Contacts { get; set; }
    }
}