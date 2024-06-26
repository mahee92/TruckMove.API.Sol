using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class PreDepartureChecklistDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }


        [DisplayName("Water")]
        public string? Water { get; set; }

        [DisplayName("Spare Rim")]
        public string? SpareRim { get; set; }

        [DisplayName("All Lights And Indicators")]
        public string? AllLightsAndIndicators { get; set; }

        [DisplayName("Jack And Tools")]
        public string? JackAndTools { get; set; }

        [DisplayName("Owners Manual")]
        public string? OwnersManual { get; set; }

        [DisplayName("Air And Electrics")]
        public string? AirAndElectrics { get; set; }

        [DisplayName("Tyres Condition")]
        public string? TyresCondition { get; set; }

        [DisplayName("Visually Dip And Check Taps")]
        public string? VisuallyDipAndCheckTaps { get; set; }

        [DisplayName("Windscreen Damage /Wipers")]
        public string? WindscreenDamageWipers { get; set; }

        [DisplayName("Vehicle Clean & Free of Rubbish")]
        public string? VehicleCleanFreeOfRubbish { get; set; }

        [DisplayName("Keys / Fob - Total Keys")]
        public string? KeysFobTotalKeys { get; set; }

        [DisplayName("Check Inside Truck & Trailer for loose or unrestrained loads")]
        public string? CheckInsideTruckTrailer { get; set; }

        [DisplayName("Oil (Used Vehicles)")]
        public string? Oil { get; set; }

        [DisplayName("Check Truck Height (4.3m)")]
        public string? CheckTruckHeight { get; set; }

        [DisplayName("Left Hand Damage")]
        public string? LeftHandDamage { get; set; }

        [DisplayName("Right Hand Damage")]
        public string? RightHandDamage { get; set; }

        [DisplayName("Front Damage")]
        public string? FrontDamage { get; set; }

        [DisplayName("Rear Damage")]
        public string? RearDamage { get; set; }

        [DisplayName("Fuel Level")]
        public decimal? FuelLevel { get; set; }
    }
}
