using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.PrimaryDTOs;

namespace TruckMove.API.BLL.Services.PrimaryServices
{
    public interface IRateService
    {
        Task<Response<UpdateRateValueDto>> UpdateRateValueAsync(UpdateRateValueDto updatedRate, int userId);

    }
}
