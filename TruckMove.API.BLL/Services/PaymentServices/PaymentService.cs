using AutoMapper;
using Microsoft.Extensions.Logging;
using TruckMove.API.BLL.Services.JobServices;
using TruckMove.API.DAL.Repositories;
using TruckMove.API.DAL.Repositories.PaymentRepositories;
using TruckMove.API.DAL.VMmodels;
using TruckMove.API.BLL.Helper;


namespace TruckMove.API.BLL.Services.PaymentServices
{
    public class PaymentService :IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMasterDataRepository _repository;
        private readonly IJobService _jobService;


        private readonly ILogger<JobService> _logger;
        public PaymentService(IMapper mapper, IPaymentRepository paymentRepository, ILogger<JobService> logger, IMasterDataRepository repository)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;           
            _logger = logger;
            _repository = repository;

        }

       

        public IQueryable<DriverJobPaymentVM> GetAllUnpaidPaymentsForDrivers()
        {
            return _paymentRepository.GetAllUnpaidPaymentsForDrivers();
            
        }

       
        public async Task<object> GetCalculatedLegPaymentsAsync(int jobId, int driverId)
        {
          
            var rates = await _repository.GetAllRates();
            var paymentRates = new PaymentRates
            {
               
                PerKmRate = rates.FirstOrDefault(x => x.Name.Contains("Per_KM_Rate"))?.Value ?? 0,
                Saturday_KM_rate = rates.FirstOrDefault(x => x.Name.Contains("Saturday_KM_rate"))?.Value ?? 0,
                Sunday_KM_rate = rates.FirstOrDefault(x => x.Name.Contains("Sunday_KM_rate"))?.Value ?? 0,
                Public_holiday_KM_rate = rates.FirstOrDefault(x => x.Name.Contains("Public_holiday_KM_rate"))?.Value ?? 0,


                Max_fixed_job_KMs = rates.FirstOrDefault(x => x.Name.Contains("Max_fixed_job_KMs"))?.Value ?? 0,
                Fixed_job_rate = rates.FirstOrDefault(x => x.Name.Contains("Fixed_job_rate"))?.Value ?? 0,
                Saturday_Fixed_rate = rates.FirstOrDefault(x => x.Name.Contains("Saturday_fixed_rate"))?.Value ?? 0,
                Sunday_Fixed_rate = rates.FirstOrDefault(x => x.Name.Contains("Sunday_fixed_rate"))?.Value ?? 0,
                Public_holiday_Fixed_rate = rates.FirstOrDefault(x => x.Name.Contains("Public_holiday_fixed_rate"))?.Value ?? 0,

                Commercial_load_KM_rate = rates.FirstOrDefault(x => x.Name.Contains("Commercial_load_KM_rate"))?.Value ?? 0,
               
                Dangerous_Goods_day_Rate = rates.FirstOrDefault(x => x.Name.Contains("Dangerous_Goods_day_Rate"))?.Value ?? 0,

                hookUp_Single = rates.FirstOrDefault(x => x.Name.Contains("Hookup_Single"))?.Value ?? 0,
                hookUp_Double = rates.FirstOrDefault(x => x.Name.Contains("Hookup_Double"))?.Value ?? 0,
                hookUp_4RA = rates.FirstOrDefault(x => x.Name.Contains("Hookup_4RA"))?.Value ?? 0,
            };
           




            var Legs = await _paymentRepository.GetUnpaidLegDataForDriver(jobId, driverId);

           

            // Get the day of the week
            

            double leggrandTotal = 0;
            var response = Legs.Select(leg =>
            {

                var payment = new LegPaymentTransact(leg, paymentRates);
                leggrandTotal += payment.Total;

                return new
                {
                    leg.Id,
                    leg.LegNumber,                   
                    leg.CreatedDate,
                    leg.StartLocation,
                    leg.EndLocation,
                    leg.StartTime,
                    leg.EndTime,

                   
                    payment.LegDay,
                    payment.TotalKm,

                    payment.LegType,
                    payment.PerKmTotal,
                    payment.FixedJobTotal,

                    payment.IsCommercialLoad,
                    payment.CommercialLoadTotal,
                  
                    
                    leg.Job.IsDangerousGoods,
                    payment.DangerousGoodsTotal,
                 

                   
                   // payment.CalculationBreakdown

                    payment.HookupSingleCount,
                    payment.HookupSingleTotal,

                    payment.HookupDoubleCount,
                    payment.HookupDoubleTotal,

                    payment.Hookup4RACount,
                    payment.Hookup4RATotal,

                    payment.Total


                };
            }).ToList();

            return new { LegGroups = response, leggrandTotal= leggrandTotal };
        }


        public Task ChangeVerification(bool verify, int id, bool isLeg, bool isDelay, bool purchase, bool isPublicTransport)
        {
            throw new NotImplementedException();
        }


    }
}
