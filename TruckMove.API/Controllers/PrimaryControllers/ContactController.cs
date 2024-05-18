using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL;
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.BLL.Services.Primary;
using TruckMove.API.BLL.Services.PrimaryServices;
using TruckMove.API.Controllers.Primary;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.Controllers.PrimaryControllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {


        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactservice;

        public ContactController(ILogger<ContactController> logger, IContactService companService)
        {
            _logger = logger;
            _contactservice = companService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {

            Response<ContactDto> response = await _contactservice.GetAsync(id);


            if (response.Success)
            {
                return Ok(response.Object);
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);

            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ContactDto contact)
        {

            Response<ContactDto> response = await _contactservice.AddAsync(contact);
            if (response.Success)
            {
                //  return CreatedAtRoute("Contact", new { id = response.Object.Id }, response.Object);
                return Ok(response.Object);
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Response<ContactDto> response = await _contactservice.DeleteAsync(id);
            if (response.Success)
            {
                return NoContent();
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _contactservice.GetAllAsync();
            if (response.Success)
            {
                return Ok(response.Objects);
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

       
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] ContactUpdateDto contact)
        {

            Response<ContactUpdateDto> response = await _contactservice.UpdateAsync(contact);

            if (response.Success)
            {
                return NoContent();
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        //
        //[HttpPut]
        //public async Task<IActionResult> PutAsync(int id, [FromBody] ContactUpdateDto updateCompany)
        //{

        //    Response<ContactUpdateDto> response = await _contactservice.UpdateAsync(contact);

        //    if (response.Success)
        //    {
        //        return NoContent();
        //    }
        //    else
        //    {
        //        _logger.BeginScope(response.ErrorMessage);
        //        return StatusCode((int)response.ErrorType, response.ErrorMessage);
        //    }
        //}





    }
}
