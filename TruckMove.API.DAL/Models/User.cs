using System;
using System.Collections.Generic;
using TruckMove.API.DAL.dbFirst;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class User : AuditableEntity, IActiveEntity
    {
        public User()
        {
            AccommodationAssigneeNavigations = new HashSet<Accommodation>();
            AccommodationDriverNavigations = new HashSet<Accommodation>();
            CompanyCreatedBies = new HashSet<Company>();
            CompanyUpdatedBies = new HashSet<Company>();
            ContactCreatedBies = new HashSet<Contact>();
            ContactUpdatedBies = new HashSet<Contact>();
            InverseCreatedBy = new HashSet<User>();
            InverseUpdatedBy = new HashSet<User>();
            JobControllerNavigations = new HashSet<Job>();
            JobCreatedBies = new HashSet<Job>();
            JobUpdatedBies = new HashSet<Job>();
            UserRoleCreatedBies = new HashSet<UserRole>();
            UserRoleUpdatedBies = new HashSet<UserRole>();
            UserRoleUsers = new HashSet<UserRole>();
            JobDriverNavigations = new HashSet<Job>();
            PermitsAndPlates = new HashSet<PermitsAndPlate>();
            PermitsAndPlatesUpdatedBies = new HashSet<PermitsAndPlate>();
            PermitsAndPlatesCreatedBies = new HashSet<PermitsAndPlate>();
            PublicTransportAssigneeNavigations = new HashSet<PublicTransport>();
            PublicTransportDriverNavigations = new HashSet<PublicTransport>();
            PublicTransportUpdatedBies = new HashSet<PublicTransport>();
            PublicTransportCreatedBies = new HashSet<PublicTransport>();
            PurchaseAssigneeNavigations = new HashSet<Purchase>();
            PurchaseDriverNavigations = new HashSet<Purchase>();
            Legs = new HashSet<Leg>();
            LegCreatedBies = new HashSet<Leg>();

            LegUpdatedBies = new HashSet<Leg>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsActive { get; set; }
     

     
        public virtual ICollection<Company> CompanyCreatedBies { get; set; }
        public virtual ICollection<Company> CompanyUpdatedBies { get; set; }
        public virtual ICollection<Contact> ContactCreatedBies { get; set; }
        public virtual ICollection<Contact> ContactUpdatedBies { get; set; }
        public virtual ICollection<User> InverseCreatedBy { get; set; }
        public virtual ICollection<User> InverseUpdatedBy { get; set; }
        public virtual ICollection<Job> JobControllerNavigations { get; set; }
        public virtual ICollection<Job> JobCreatedBies { get; set; }
        public virtual ICollection<Job> JobUpdatedBies { get; set; }
        public virtual ICollection<UserRole> UserRoleCreatedBies { get; set; }
        public virtual ICollection<UserRole> UserRoleUpdatedBies { get; set; }
        public virtual ICollection<UserRole> UserRoleUsers { get; set; }

        public virtual ICollection<Job> JobDriverNavigations { get; set; }
        public virtual ICollection<PermitsAndPlate> PermitsAndPlates { get; set; }

        public virtual ICollection<PermitsAndPlate> PermitsAndPlatesUpdatedBies { get; set; }
        public virtual ICollection<PermitsAndPlate> PermitsAndPlatesCreatedBies { get; set; }
        public virtual ICollection<Accommodation> AccommodationAssigneeNavigations { get; set; }
        public virtual ICollection<Accommodation> AccommodationDriverNavigations { get; set; }

        public virtual ICollection<Accommodation> AccommodationUpdatedBies { get; set; }
        public virtual ICollection<Accommodation> AccommodationCreatedBies { get; set; }

        public virtual ICollection<PublicTransport> PublicTransportAssigneeNavigations { get; set; }
        public virtual ICollection<PublicTransport> PublicTransportDriverNavigations { get; set; }

        public virtual ICollection<PublicTransport> PublicTransportUpdatedBies { get; set; }
        public virtual ICollection<PublicTransport> PublicTransportCreatedBies { get; set; }

        public virtual ICollection<Purchase> PurchaseAssigneeNavigations { get; set; }
        public virtual ICollection<Purchase> PurchaseDriverNavigations { get; set; }

        public virtual ICollection<Purchase> PurchaseUpdatedBies { get; set; }
        public virtual ICollection<Purchase> PurchaseCreatedBies { get; set; }

        public virtual ICollection<Leg> Legs { get; set; }
        public virtual ICollection<Leg> LegCreatedBies { get; internal set; }
        public virtual ICollection<Leg> LegUpdatedBies { get; internal set; }
    }
}
