using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst
{
    public partial class PreDepartureChecklist
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string? Water { get; set; }
        public string? SpareRim { get; set; }
        public string? AllLightsAndIndicators { get; set; }
        public string? JackAndTools { get; set; }
        public string? OwnersManual { get; set; }
        public string? AirAndElectrics { get; set; }
        public string? TyresCondition { get; set; }
        public string? VisuallyDipAndCheckTaps { get; set; }
        public string? WindscreenDamageWipers { get; set; }
        public string? VehicleCleanFreeOfRubbish { get; set; }
        public string? KeysFobTotalKeys { get; set; }
        public string? CheckInsideTruckTrailer { get; set; }
        public string? Oil { get; set; }
        public string? CheckTruckHeight { get; set; }
        public string? LeftHandDamage { get; set; }
        public string? RightHandDamage { get; set; }
        public string? FrontDamage { get; set; }
        public string? RearDamage { get; set; }
        public int? NotesId { get; set; }
        public int? PhotosId { get; set; }
        public decimal? FuelLevel { get; set; }

        public virtual Job Job { get; set; } = null!;
    }
}
