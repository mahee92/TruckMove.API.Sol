using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.Options;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Services.JobServices;
using TruckMove.API.Helper;
using TruckMove.API.Settings;

namespace TruckMove.API.Controllers.JobControllers
{
    public class TestController : ODataController
    {
        private readonly IAuthUserService _authUserService;
        private readonly IJobService _jobService;
        private readonly MySettings _mySettings;
        private readonly GoogleMapSettings _googleMapSettings;

        public TestController(IAuthUserService authUserService, IJobService jobService, IOptions<MySettings> mySettings, IOptions<GoogleMapSettings> googleMapSettings)
        {

            _authUserService = authUserService;
            _jobService = jobService;
            _mySettings = mySettings.Value;
            _googleMapSettings = googleMapSettings.Value;

        }
        private static Random random = new Random();
        private static List<Customer> customers = new List<Customer>(
            Enumerable.Range(1, 3).Select(idx => new Customer
            {
                Id = idx,
                Name = $"Customer {idx}",
                Orders = new List<Order>(
                    Enumerable.Range(1, 2).Select(dx => new Order
                    {
                        Id = (idx - 1) * 2 + dx,
                        Amount = random.Next(1, 9) * 10
                    }))
            }));

        //[EnableQuery]
        //public ActionResult<IEnumerable<Customer>> Get()
        //{
        //    return Ok(customers);
        //}

        //[EnableQuery]
        //public ActionResult<Customer> Get([FromRoute] int key)
        //{
        //    var item = customers.SingleOrDefault(d => d.Id.Equals(key));

        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(item);
        //}
        [EnableQuery]
        public  ActionResult<IEnumerable<JobOutPutDTO>> Get()
        {
            //string jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiZHJpdmVyQGV4YW1wbGUuY29tIiwibmFtZWlkIjoiMjEiLCJyb2xlIjoiRHJpdmVyIiwibmJmIjoxNzE5MzM0NTIyLCJleHAiOjE3MTkzMzgxMjIsImlhdCI6MTcxOTMzNDUyMiwiaXNzIjoiaHR0cHM6Ly92dG10cnVja21vdmUuYXBpLmRldi5yaXZlcmluYS5kaWdpdGFsLyIsImF1ZCI6Imh0dHBzOi8vdnRtdHJ1Y2ttb3ZlLmFwaS5kZXYucml2ZXJpbmEuZGlnaXRhbC8ifQ.qMI46lgenS0kKwDsYf8HIew_R-IzgSIrT713Dl1m60";
            //await JobApiClient.SetJwtToken();
            //string result = await JobApiClient.ApiCallAsync();

            var query = _jobService.GetAllAsync();
            //var count = query.Count();
            //var data = query.ToList();
            return Ok(query);
        }
    }
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Order> Orders { get; set; }
}


    public class Order
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
    }

