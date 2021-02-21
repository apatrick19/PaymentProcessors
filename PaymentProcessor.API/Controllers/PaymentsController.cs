using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentProcessor.Core.Concrete;
using PaymentProcessor.Data;
using PaymentProcessor.Logic.Contracts;
using PaymentProcessor.Logic.Models;

namespace PaymentProcessor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        
        private readonly IPaymentProcessorLogic _paymentProcessor;


        public PaymentsController(IPaymentProcessorLogic payment)
        {
           
            _paymentProcessor = payment;
        }


        [HttpPost]

        public async Task<IActionResult> ProcessPayment(PaymentModel request)
        {
            //check that model is valid else return internal server error 
            if (!ModelState.IsValid)
            {
                return BadRequest("invalid input requests");
            }

            //prevalidaiton is done by data anotation on the Payment Model (credit card, amount , 3 digit security code etc)
            var result = _paymentProcessor.ProcessPayment(request);

            switch (result?.ResponseCode)
            {
                case "00":
                    return Ok(result?.ResponseMessage);
                case "01":
                    return BadRequest(result?.ResponseMessage);
                case "06":
                    return StatusCode(StatusCodes.Status500InternalServerError,result?.ResponseMessage);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError, result?.ResponseMessage);
            }
        }
    }
}
