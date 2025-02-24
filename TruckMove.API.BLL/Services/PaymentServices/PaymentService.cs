using AutoMapper;
using Microsoft.Extensions.Logging;
using TruckMove.API.BLL.Services.JobServices;
using TruckMove.API.DAL.Repositories;
using TruckMove.API.DAL.Repositories.PaymentRepositories;
using TruckMove.API.DAL.VMmodels;
using TruckMove.API.BLL.Helper;
using static TruckMove.API.DAL.MasterData.MasterData;
using TruckMove.API.DAL.Models;
using TruckMove.API.BLL.Models.PaymentDto;
using Microsoft.EntityFrameworkCore;



namespace TruckMove.API.BLL.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMasterDataRepository _repository;
        private readonly IJobService _jobService;


        private readonly ILogger<JobService> _logger;
        private readonly IRepository<Leg> _repositoryLeg;
        private readonly IRepository<PublicTransport> _repositoryPublicTransport;
        private readonly IRepository<Purchase> _repositoryPurchase;
        //private readonly IRepository<DelayDriver> _repositoryDelayDrivers;

        public PaymentService(IMapper mapper, IPaymentRepository paymentRepository, ILogger<JobService> logger, IMasterDataRepository repository, IRepository<Leg> repositoryLeg,IRepository<PublicTransport> repositoryPublicTransport, IRepository<Purchase> repositoryPurchase/*, IRepository<DelayDriver> delayDrivers*/)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;           
            _logger = logger;
            _repository = repository;
            _repositoryLeg = repositoryLeg;
            _repositoryPublicTransport = repositoryPublicTransport;
            _repositoryPurchase = repositoryPurchase;
            //_repositoryDelayDrivers = delayDrivers;


        }


        #region Lists
        public IQueryable<DriverJobPaymentVM> GetQAPendingList(int controller)
        {
            return _paymentRepository.GetQAPendingList(controller);

        }
        public IQueryable<DriverJobPaymentVM> GetPayemntList(int status,int controller)
        {
            if(status == (int)PaymentStatusEnum.QAPending)
            {
                return _paymentRepository.GetQAPendingList(controller);
            }
            else
            {
                return _paymentRepository.GetPayemntList(status);
            }

           
        }
        #endregion




        #region Details
        public async Task<object> GetDetails(int jobId, int driverId)
        {
             var rates = await GetPaymentRates();
             var Legs = await GetCalculatedLegPayments(jobId, driverId,rates);
             var delays = await GetCalculatedDelayPayments(jobId, driverId, rates);
             var publicTransports = await GetCalculatedPublicTransportsPayments(jobId, driverId, rates);
            var paymentAjustments = await GetPaymentAjustments(jobId, driverId);

            dynamic legsResult = Legs;
            dynamic delaysResult = delays;
            dynamic publicTransportsResult = publicTransports;
            dynamic paymentAjustmentsResult = paymentAjustments;

            var Total = (double)legsResult.leggrandTotal +
                             (double)delaysResult.delaygrandTotal +
                             (double)publicTransportsResult.publicTrasportgrandTotal+
                             (double)paymentAjustmentsResult.paymentAdjustmentsgrandTotal;

            return new { Legs = Legs, delays = delays, publicTransports= publicTransports, paymentAjustments= paymentAjustments,  total = Math.Round(Total,2) };


        }

        public async Task<PaymentRates> GetPaymentRates()
        {
            var rates = await _repository.GetAllRates();
            var paymentRates = new PaymentRates
            {

                PerKmRate = rates.FirstOrDefault(x => x.Name.Contains("Per_KM_Rate"))?.Value ?? 0,
                Grade_4_Hourly_rate = rates.FirstOrDefault(x => x.Name.Contains("Grade_4_Hourly_rate"))?.Value ?? 0,

                Commercial_load_KM_rate = rates.FirstOrDefault(x => x.Name.Contains("Commercial_load_KM_rate"))?.Value ?? 0,

                Dangerous_Goods_day_Rate = rates.FirstOrDefault(x => x.Name.Contains("Dangerous_Goods_day_Rate"))?.Value ?? 0,

                hookUp_Single = rates.FirstOrDefault(x => x.Name.Contains("Hookup_Single"))?.Value ?? 0,
                hookUp_Double = rates.FirstOrDefault(x => x.Name.Contains("Hookup_Double"))?.Value ?? 0,
                hookUp_4RA = rates.FirstOrDefault(x => x.Name.Contains("Hookup_4RA"))?.Value ?? 0,
                Delay_hourly_rate = rates.FirstOrDefault(x => x.Name.Contains("Delay_hourly_rate"))?.Value ?? 0,
                Public_transport_hourly_Rate = rates.FirstOrDefault(x => x.Name.Contains("Public_transport_hourly_Rate"))?.Value ?? 0

            };
            return paymentRates;
        }
        public async Task<object> GetCalculatedDelayPayments(int jobId, int driverId, PaymentRates rates)
        {
            var delays = await _paymentRepository.GetUnpaidDelaysForDriver(jobId, driverId);

            var response = delays.Select(d => new
            {
                d.Id,
                d.Delay.StartTime,
                d.Delay.EndTime,
                DurationHours = d.Delay.StartTime.HasValue && d.Delay.EndTime.HasValue
                                ? Math.Round( (d.Delay.EndTime.Value - d.Delay.StartTime.Value).TotalHours,2)
                                : 0, // Handle null values
                rates.Delay_hourly_rate,
                Total = d.Delay.StartTime.HasValue && d.Delay.EndTime.HasValue
                         ? Math.Round((d.Delay.EndTime.Value - d.Delay.StartTime.Value).TotalHours * rates.Delay_hourly_rate, 2)
                        : 0,
                PaymentStatusText = ((PaymentStatusEnum)d.PaymentStatus).ToString(),
            }).ToList();

            double delayTotal = response.Sum(r => r.Total);

            return new { DelayGroups = response, delaygrandTotal = Math.Round(delayTotal,2) };
        }

        public async Task<object> GetCalculatedLegPayments(int jobId, int driverId, PaymentRates rates)
        {

            var Legs = await _paymentRepository.GetUnpaidLegDataForDriver(jobId, driverId);

           

            // Get the day of the week
            

            double leggrandTotal = 0;
            var response = Legs.Select(leg =>
            {

                var payment = new LegPaymentTransact(leg, rates);
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
                    PaymentStatusText = ((PaymentStatusEnum)leg.PaymentStatus).ToString(),



                   // payment.LegDay,
                    

                    payment.LegType,

                    payment.TotalKm,
                    payment.PerKmTotal,
                    rates.PerKmRate,


                    
                    payment.HourlyTotal,
                    payment.RoundedHoursSring,
                    rates.Grade_4_Hourly_rate,

                    payment.IsCommercialLoad,
                    payment.CommercialLoadTotal,
                    rates.Commercial_load_KM_rate,



                    leg.Job.IsDangerousGoods,
                    payment.DangerousGoodsTotal,
                    rates.Dangerous_Goods_day_Rate,



                   // payment.CalculationBreakdown

                    payment.HookupSingleCount,
                    payment.HookupSingleTotal,
                    rates.hookUp_Single,

                    payment.HookupDoubleCount,
                    payment.HookupDoubleTotal,
                    rates.hookUp_Double,

                    payment.Hookup4RACount,
                    payment.Hookup4RATotal,
                    rates.hookUp_4RA,

                    payment.Total




                };
            }).ToList();

            return new { LegGroups = response, leggrandTotal= Math.Round(leggrandTotal,2) };
        }

        public async Task<object> GetCalculatedPublicTransportsPayments(int jobId, int driverId, PaymentRates rates)
        {

            var PublicTrasports = await _paymentRepository.GetUnpaidPublicTransportsDataForDriver(jobId, driverId);

            var response = PublicTrasports.Select(d => new
            {
                d.Id,
                d.Name,
                type = (d.TransportType == null) ? "-" : ((PublicTransportTypeEnum)d.TransportType).ToString(),
                d.DepartureDateTime,
                d.ArrivalDateTime,
                d.DepartureAddress,
                d.ArrivalAddress,
                DurationHours = d.ArrivalDateTime.HasValue && d.DepartureDateTime.HasValue
                                ?Math.Round( (d.ArrivalDateTime.Value - d.DepartureDateTime.Value).TotalHours,2)
                                : 0, // Handle null values
                rates.Public_transport_hourly_Rate,
                Total = d.ArrivalDateTime.HasValue && d.DepartureDateTime.HasValue
                                ? Math.Round((d.ArrivalDateTime.Value - d.DepartureDateTime.Value).TotalHours * rates.Public_transport_hourly_Rate,2)
                                : 0,
                PaymentStatusText = ((PaymentStatusEnum)d.PaymentStatus).ToString()
            }).ToList();

            double total = response.Sum(r => r.Total);

            return new { publicTrasportGroups = response, publicTrasportgrandTotal = Math.Round(total,2) };
        }

        public async Task<object> GetPaymentAjustments(int jobId, int driverId)
        {
            var paymentAdjustments = await _paymentRepository.GetUnpaidPaymentAjustments(jobId, driverId);

            var response = paymentAdjustments.Select(d => new
            {
                d.Id,
                d.JobId,
                d.DriverId,
                d.Description,
                Total= Math.Round(d.Amount,2),
                PaymentStatusText = ((PaymentStatusEnum)d.Status).ToString(),
            }).ToList();

            decimal total = response.Sum(r => r.Total);

            return new { paymentAdjustments = response, paymentAdjustmentsgrandTotal = Math.Round(total, 2) };
        }

        #endregion 

        #region Status Change
        public async Task<Response> ChangePaymentStatus(PaymentStatusDTO PaymentStatusDTO, int userId)
        {
            Response response = new Response();

            try
            {
                object entity = null;

                if (PaymentStatusDTO.isLeg)
                {
                    entity = await _repositoryLeg.GetAsync(PaymentStatusDTO.Id);
                    if (entity is Leg leg)
                    {
                        leg.PaymentStatus = (int)PaymentStatusDTO.StatusId;
                        leg.LastModifiedDate = DateTime.Now;
                        leg.UpdatedById = userId;
                        var res = await _repositoryLeg.UpdateAsync(leg);
                        response.data = res.PaymentStatus.ToString();
                        response.Success = true;
                    }
                }
                else if(PaymentStatusDTO.isPublicTransport)
                {
                    entity = await _repositoryPublicTransport.GetAsync(PaymentStatusDTO.Id);
                    if (entity is PublicTransport publicTransport)
                    {
                        publicTransport.PaymentStatus = (int)PaymentStatusDTO.StatusId;
                        publicTransport.LastModifiedDate = DateTime.Now;
                        publicTransport.UpdatedById = userId;
                        var res = await _repositoryPublicTransport.UpdateAsync(publicTransport);
                        response.data = res.PaymentStatus.ToString();
                        response.Success = true;
                    }
                }
                else if (PaymentStatusDTO.isDelay)
                {
                    entity = await _paymentRepository.GetDelayDriverAsync(PaymentStatusDTO.Id);
                    if (entity is DelayDriver delay)
                    {
                        delay.PaymentStatus = (int)PaymentStatusDTO.StatusId;
                        //delay.LastModifiedDate = DateTime.Now;
                        //delay.UpdatedById = userId;
                        var res = await _paymentRepository.DelayDriverUpdateAsync(delay);
                        response.data = res.PaymentStatus.ToString();
                        response.Success = true;
                    }
                }
                // If entity was not found, return a not found response.
                if (entity == null)
                {
                    return new Response
                    {
                        Success = false,
                        ErrorType = ErrorCode.NotFound,
                        ErrorMessage = ErrorMessages.NotFound
                    };
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
            }
            
            return response;
        }

        public async Task<Response> VerifyOrPayPayment(int jobId, int driverId, int userId, int status)
        {
            Response response = new Response();

            try
            {
               if(jobId == -1 || driverId == -1)
                {
                    response.Success = false;
                    response.ErrorType = ErrorCode.NotFound;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    return response;
                }

                await _paymentRepository.ExecuteInTransactionAsync(async () =>
                {
                    var legs = await _paymentRepository.GetUnpaidLegDataForDriver(jobId, driverId);
                    var publicTransports = await _paymentRepository.GetUnpaidPublicTransportsDataForDriver(jobId, driverId);
                    var delays = await _paymentRepository.GetUnpaidDelaysForDriver(jobId, driverId);

                    foreach (var leg in legs)
                    {
                        leg.PaymentStatus = status;
                        leg.LastModifiedDate = DateTime.Now;
                        leg.UpdatedById = userId;
                    }
                    await _paymentRepository.UpdateListAsync(legs);

                    foreach (var publicTransport in publicTransports)
                    {
                        publicTransport.PaymentStatus = status;
                        publicTransport.LastModifiedDate = DateTime.Now;
                        publicTransport.UpdatedById = userId;
                    }
                    await _paymentRepository.UpdateListAsync(publicTransports);

                    foreach (var delay in delays)
                    {
                        delay.PaymentStatus = status;
                    }
                    await _paymentRepository.UpdateListAsync(delays);
                });

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<Response> AddPaymentAjustments(PaymentAdjustmentDTO paymentAdjustment, int userId)
        {
            Response response = new Response();
            try
            {

                PaymentAdjustment newPaymentAdjustment = _mapper.Map<PaymentAdjustment>(paymentAdjustment);
                newPaymentAdjustment.CreatedDate = DateTime.Now;
                newPaymentAdjustment.CreatedById = userId;
                newPaymentAdjustment.Status = (int)PaymentStatusEnum.QADone;
                var res = await _paymentRepository.AddPaymentAdjustmentAsync(newPaymentAdjustment);
                response.data = res.Id.ToString();
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
                return response;
            }

            return response;
        }

        public async Task<Response> DeletePaymentAjustment(int id)
        {
            Response response = new Response();
            try
            {
                await _paymentRepository.DeletePaymentAdjustmentAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
                return response;
            }

            return response;
        }



        #endregion


    }
}
