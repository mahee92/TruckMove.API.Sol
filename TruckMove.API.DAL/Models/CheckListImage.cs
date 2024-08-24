using System;
using System.Collections.Generic;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public partial class CheckListImage 
    {
        public int Id { get; set; }
        public int ChecklistId { get; set; }
        public string? Url { get; set; }

        public virtual Checklist Checklist { get; set; } = null!;
       
    }
}
