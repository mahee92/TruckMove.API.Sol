using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirst2
{
    public partial class CheckListImage
    {
        public int Id { get; set; }
        public int ChecklistId { get; set; }
        public string? Url { get; set; }

        public virtual Checklist Checklist { get; set; } = null!;
    }
}
