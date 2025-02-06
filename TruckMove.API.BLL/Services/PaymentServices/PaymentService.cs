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
using TruckMove.API.DAL.VMmodels;
using TruckMove.API.DAL.Models;
using TruckMove.API.BLL.Helper;

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

        public IQueryable<DriverJobPaymentVM> GetAllUnpaidPaymentsForDrivers()
        {
            return _paymentRepository.GetAllUnpaidPaymentsForDrivers();
            
        }

       
        public async Task<object> GetCalculatedLegPaymentsAsync(int jobId, int driverId)
        {
            // Define payment rates (Later, you can move this to DB or config)
            var paymentRates = new PaymentRates
            {
                Commercial_load_KM_rate = 5,
                PerKmRate = 10,
                Saturday_Fixed_rate = 12,
                Sunday_Fixed_rate = 15,
                Public_holiday_Fixed_rate = 20,
                Saturday_KM_rate = 8,
                Sunday_KM_rate = 12,
                Public_holiday_KM_rate = 15,
                Max_fixed_job_KMs = 100,
                Fixed_job_rate = 100,
                Dangerous_Goods_day_Rate = 20

            };

       

        var Legs = await _paymentRepository.GetUnpaidLegDataForDriver(jobId, driverId);


            // Perform calculations in BA layer
            // Perform calculations in a single loop and prepare the response
            double leggrandTotal = 0;
            var response = Legs.Select(leg =>
            {
                var payment = new LegPaymentTransact(leg.LegNumber, "Saturday", leg.TotalDistance??0,leg.Job.IsCommercialLoad,leg.Job.IsDangerousGoods, paymentRates);
                leggrandTotal += payment.Total;

                return new
                {
                    payment.LegNumber,                   
                    leg.CreatedDate,
                    leg.StartLocation,
                    leg.EndLocation,
                    leg.StartTime,
                    leg.EndTime,

                    payment.LegType,
                    payment.LegDay,
                    payment.TotalKm,
                    payment.IsCommercialLoad,
                    payment.IsDangerousGoods,
                  
                    payment.PerKmTotal,
                    payment.GetPerKmTotalString,
                    payment.GetPerKmCalTotalString,


                    payment.FixedJobTotal,
                    payment.GetFixTotalString, 

                    payment.CommercialLoadTotal,
                    payment.CommercialLoadTotalString,
                    payment.CommercialLoadCalTotalString,

                    payment.DangerousGoodsTotal,
                    payment.DangerousGoodsTotalString,
                    payment.DangerousGoodsCalTotalString,


                    payment.Total,
                    payment.totalString,
                    payment.totalCalString

                };
            }).ToList();

            return new { LegGroups = response, leggrandTotal= leggrandTotal };
        }

      

    }
}
