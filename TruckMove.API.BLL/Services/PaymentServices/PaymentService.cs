using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Services.JobServices;
using TruckMove.API.DAL.Repositories.JobRepositories;
using TruckMove.API.DAL.Repositories;
using TruckMove.API.DAL.dbFirst;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.DAL.Repositories.PaymentRepositories;
using AutoMapper.QueryableExtensions;

namespace TruckMove.API.BLL.Services.PaymentServices
{
    public class PaymentService :IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
       

        private readonly ILogger<JobService> _logger;
        public PaymentService(IMapper mapper, IPaymentRepository paymentRepository, ILogger<JobService> logger)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;           
            _logger = logger;

        }

        public IQueryable<LegOutPutDto> GetAllUnpaidLegsByDrivers()
        {
            var jobs = _paymentRepository.GetAllUnpaidLegsByDrivers();
            return jobs.ProjectTo<LegOutPutDto>(_mapper.ConfigurationProvider);
        }

    }
}
