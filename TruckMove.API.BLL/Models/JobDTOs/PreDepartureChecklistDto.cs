using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class PreDepartureChecklistDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }


        [JsonProperty("Water")]
        public string Water { get; set; }

        [JsonProperty("Spare Rim")]
        public string SpareRim { get; set; }

        [JsonProperty("All Lights And Indicators")]
        public string AllLightsAndIndicators { get; set; }

        [JsonProperty("Jack And Tools")]
        public string JackAndTools { get; set; }

        [JsonProperty("Owners Manual")]
        public string OwnersManual { get; set; }

        [JsonProperty("Air And Electrics")]
        public string AirAndElectrics { get; set; }

        [JsonProperty("Tyres Condition")]
        public string TyresCondition { get; set; }

        [JsonProperty("Visually Dip And Check Taps")]
        public string VisuallyDipAndCheckTaps { get; set; }

        [JsonProperty("Windscreen Damage /Wipers")]
        public string WindscreenDamageWipers { get; set; }

        [JsonProperty("Vehicle Clean & Free of Rubbish")]
        public string VehicleCleanFreeOfRubbish { get; set; }

        [JsonProperty("Keys / Fob - Total Keys")]
        public string KeysFobTotalKeys { get; set; }

        [JsonProperty("Check Inside Truck & Trailer for loose or unrestrained loads")]
        public string CheckInsideTruckTrailer { get; set; }

        [JsonProperty("Oil (Used Vehicles)")]
        public string Oil { get; set; }

        [JsonProperty("Check Truck Height (4.3m)")]
        public string CheckTruckHeight { get; set; }

        [JsonProperty("Left Hand Damage")]
        public string LeftHandDamage { get; set; }

        [JsonProperty("Right Hand Damage")]
        public string RightHandDamage { get; set; }

        [JsonProperty("Front Damage")]
        public string FrontDamage { get; set; }

        [JsonProperty("Rear Damage")]
        public string RearDamage { get; set; }

        public int? NotesId { get; set; }
        public int? PhotosId { get; set; }

        [JsonProperty("Fuel Level")]
        public decimal FuelLevel { get; set; }
    }
}
