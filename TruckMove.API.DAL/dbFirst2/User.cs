using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst2
{
    public partial class User
    {
        public User()
        {
            AccommodationAssigneeNavigations = new HashSet<Accommodation>();
            AccommodationCreatedBies = new HashSet<Accommodation>();
            AccommodationDriverNavigations = new HashSet<Accommodation>();
            AccommodationUpdatedBies = new HashSet<Accommodation>();
            AttachmentCreatedBies = new HashSet<Attachment>();
            AttachmentUpdatedBies = new HashSet<Attachment>();
            ChecklistCreatedBies = new HashSet<Checklist>();
            ChecklistUpdatedBies = new HashSet<Checklist>();
            CompanyCreatedBies = new HashSet<Company>();
            CompanyUpdatedBies = new HashSet<Company>();
            ContactCreatedBies = new HashSet<Contact>();
            ContactUpdatedBies = new HashSet<Contact>();
            DelayAssigneeNavigations = new HashSet<Delay>();
            DelayCreatedBies = new HashSet<Delay>();
            DelayDrivers = new HashSet<DelayDriver>();
            DelayUpdatedBies = new HashSet<Delay>();
            ImageCreatedBies = new HashSet<Image>();
            ImageUpdatedBies = new HashSet<Image>();
            InverseCreatedBy = new HashSet<User>();
            InverseUpdatedBy = new HashSet<User>();
            JobContactCreatedBies = new HashSet<JobContact>();
            JobContactUpdatedBies = new HashSet<JobContact>();
            JobControllerNavigations = new HashSet<Job>();
            JobCreatedBies = new HashSet<Job>();
            JobDriverNavigations = new HashSet<Job>();
            JobUpdatedBies = new HashSet<Job>();
            LegCreatedBies = new HashSet<Leg>();
            LegDrivers = new HashSet<Leg>();
            LegUpdatedBies = new HashSet<Leg>();
            NoteCreatedBies = new HashSet<Note>();
            NoteUpdatedBies = new HashSet<Note>();
            PermitsAndPlateAssigneeNavigations = new HashSet<PermitsAndPlate>();
            PermitsAndPlateCreatedBies = new HashSet<PermitsAndPlate>();
            PermitsAndPlateUpdatedBies = new HashSet<PermitsAndPlate>();
            PublicTransportAssigneeNavigations = new HashSet<PublicTransport>();
            PublicTransportCreatedBies = new HashSet<PublicTransport>();
            PublicTransportDriverNavigations = new HashSet<PublicTransport>();
            PublicTransportUpdatedBies = new HashSet<PublicTransport>();
            PurchaseAssigneeNavigations = new HashSet<Purchase>();
            PurchaseCreatedBies = new HashSet<Purchase>();
            PurchaseDriverNavigations = new HashSet<Purchase>();
            PurchaseUpdatedBies = new HashSet<Purchase>();
            RateCreatedBies = new HashSet<Rate>();
            RateUpdatedBies = new HashSet<Rate>();
            TrailerCreatedBies = new HashSet<Trailer>();
            TrailerUpdatedBies = new HashSet<Trailer>();
            UserRoleCreatedBies = new HashSet<UserRole>();
            UserRoleUpdatedBies = new HashSet<UserRole>();
            UserRoleUsers = new HashSet<UserRole>();
            VehicleCreatedBies = new HashSet<Vehicle>();
            VehicleUpdatedBies = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? UpdatedById { get; set; }
        public int? CreatedById { get; set; }

        public virtual User? CreatedBy { get; set; }
        public virtual User? UpdatedBy { get; set; }
        public virtual ICollection<Accommodation> AccommodationAssigneeNavigations { get; set; }
        public virtual ICollection<Accommodation> AccommodationCreatedBies { get; set; }
        public virtual ICollection<Accommodation> AccommodationDriverNavigations { get; set; }
        public virtual ICollection<Accommodation> AccommodationUpdatedBies { get; set; }
        public virtual ICollection<Attachment> AttachmentCreatedBies { get; set; }
        public virtual ICollection<Attachment> AttachmentUpdatedBies { get; set; }
        public virtual ICollection<Checklist> ChecklistCreatedBies { get; set; }
        public virtual ICollection<Checklist> ChecklistUpdatedBies { get; set; }
        public virtual ICollection<Company> CompanyCreatedBies { get; set; }
        public virtual ICollection<Company> CompanyUpdatedBies { get; set; }
        public virtual ICollection<Contact> ContactCreatedBies { get; set; }
        public virtual ICollection<Contact> ContactUpdatedBies { get; set; }
        public virtual ICollection<Delay> DelayAssigneeNavigations { get; set; }
        public virtual ICollection<Delay> DelayCreatedBies { get; set; }
        public virtual ICollection<DelayDriver> DelayDrivers { get; set; }
        public virtual ICollection<Delay> DelayUpdatedBies { get; set; }
        public virtual ICollection<Image> ImageCreatedBies { get; set; }
        public virtual ICollection<Image> ImageUpdatedBies { get; set; }
        public virtual ICollection<User> InverseCreatedBy { get; set; }
        public virtual ICollection<User> InverseUpdatedBy { get; set; }
        public virtual ICollection<JobContact> JobContactCreatedBies { get; set; }
        public virtual ICollection<JobContact> JobContactUpdatedBies { get; set; }
        public virtual ICollection<Job> JobControllerNavigations { get; set; }
        public virtual ICollection<Job> JobCreatedBies { get; set; }
        public virtual ICollection<Job> JobDriverNavigations { get; set; }
        public virtual ICollection<Job> JobUpdatedBies { get; set; }
        public virtual ICollection<Leg> LegCreatedBies { get; set; }
        public virtual ICollection<Leg> LegDrivers { get; set; }
        public virtual ICollection<Leg> LegUpdatedBies { get; set; }
        public virtual ICollection<Note> NoteCreatedBies { get; set; }
        public virtual ICollection<Note> NoteUpdatedBies { get; set; }
        public virtual ICollection<PermitsAndPlate> PermitsAndPlateAssigneeNavigations { get; set; }
        public virtual ICollection<PermitsAndPlate> PermitsAndPlateCreatedBies { get; set; }
        public virtual ICollection<PermitsAndPlate> PermitsAndPlateUpdatedBies { get; set; }
        public virtual ICollection<PublicTransport> PublicTransportAssigneeNavigations { get; set; }
        public virtual ICollection<PublicTransport> PublicTransportCreatedBies { get; set; }
        public virtual ICollection<PublicTransport> PublicTransportDriverNavigations { get; set; }
        public virtual ICollection<PublicTransport> PublicTransportUpdatedBies { get; set; }
        public virtual ICollection<Purchase> PurchaseAssigneeNavigations { get; set; }
        public virtual ICollection<Purchase> PurchaseCreatedBies { get; set; }
        public virtual ICollection<Purchase> PurchaseDriverNavigations { get; set; }
        public virtual ICollection<Purchase> PurchaseUpdatedBies { get; set; }
        public virtual ICollection<Rate> RateCreatedBies { get; set; }
        public virtual ICollection<Rate> RateUpdatedBies { get; set; }
        public virtual ICollection<Trailer> TrailerCreatedBies { get; set; }
        public virtual ICollection<Trailer> TrailerUpdatedBies { get; set; }
        public virtual ICollection<UserRole> UserRoleCreatedBies { get; set; }
        public virtual ICollection<UserRole> UserRoleUpdatedBies { get; set; }
        public virtual ICollection<UserRole> UserRoleUsers { get; set; }
        public virtual ICollection<Vehicle> VehicleCreatedBies { get; set; }
        public virtual ICollection<Vehicle> VehicleUpdatedBies { get; set; }
    }
}
