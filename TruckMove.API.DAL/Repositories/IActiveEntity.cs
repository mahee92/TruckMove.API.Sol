using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.DAL.Repositories
{
    public interface IActiveEntity
    {
        int Id { get; set; }
        bool IsActive { get; set; }

        DateTime? CreatedDate { get; set; }
    }
}
