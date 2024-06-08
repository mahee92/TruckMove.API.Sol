﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.JobDTOs;

namespace TruckMove.API.BLL.Services.JobServices
{
    public interface IJobService
    {
        Task<Response<JobDto>> PostPutAsync(JobDto job,int userId);
        Task<Response> GetNextJobId();
        Task<Response<JobDto>> GetAsync(int id);
        Task<Response<JobDto>> GetAllAsync();
    }
}
