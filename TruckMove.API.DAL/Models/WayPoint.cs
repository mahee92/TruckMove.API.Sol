using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public  class WayPoint 
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string? Location { get; set; }
        public string? Coordinates { get; set; }

        public virtual Job Job { get; set; } = null!;
      
    }
}
