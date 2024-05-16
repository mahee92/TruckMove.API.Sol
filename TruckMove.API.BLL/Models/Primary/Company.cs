using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TruckMove.API.BLL.Models.Primary
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

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
    }
}