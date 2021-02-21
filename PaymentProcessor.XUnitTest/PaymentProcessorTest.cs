using PaymentProcessor.API.Controllers;
using PaymentProcessor.Logic.Contracts;
using System;
using Xunit;
using System.Threading.Tasks;
using PaymentProcessor.API;
using PaymentProcessor.Logic.Concrete;
using Microsoft.Extensions.Options;
using PaymentProcessor.Logic.Models;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using PaymentProcessor.Logic.Repository;
using PaymentProcessor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace PaymentProcessor.XUnitTest
{
    public class PaymentProcessorTest
    {
        PaymentsController _controller;
        IPaymentProcessorLogic _paymentProcessor;
        public PaymentProcessorTest()
        {
            
            _controller = new PaymentsController(_paymentProcessor);

            var services = new ServiceCollection();

            services.AddTransient<IRestIntegration, RestIntegration>();

            services.AddTransient<IPaymentRepository, PaymentRepository>();

            services.AddTransient<IPaymentStateRepository, PaymentStateRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(Startup));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PaymentProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IPaymentProfile, PaymentProfile>();
            services.AddTransient<IBaseRepo, BaseRepo>();

            services.AddTransient<ICheapPaymentGateway, CheapPaymentGateway>();
            services.AddTransient<IExpensivePaymentGateway, ExpensivePaymentGateway>();
            services.AddTransient<IPremiumPaymentService, PremiumPaymentService>();
            services.AddTransient<IPaymentProcessorLogic, PaymentProcessorLogic>();
            services.AddTransient<IPaymentProcessorLogic, PaymentProcessorLogic>();

            var serviceProvider = services.BuildServiceProvider();
           
        }


        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var Request = new PaymentModel()
            {
                Amount = 1,
                 CardHolder="46576879",
                  CreditCardNumber="yfughjhlk"
                  
            };
            _controller.ModelState.AddModelError("ExpirationDate", "Required");
            // Act
            var badRequest = _controller.ProcessPayment(Request);

            var badResult = badRequest.Result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
            Assert.Equal(400, badResult.StatusCode);
            // Assert
            //  Assert.IsType<Task<IActionResult>>(badResponse);
        }



        [Fact]
        public void InvalidCreditCardPassed_ReturnsBadRequest()
        {
            // Arrange
            var Request = new PaymentModel()
            {
                Amount = 200,
                CardHolder = "test",
                CreditCardNumber = "Test",
                ExpirationDate = DateTime.Now,
                SecurityCode="123"
               
            };

            _controller.ModelState.AddModelError("CreditCardNumber", "CreditCard");
            // Act
            var badRequest = _controller.ProcessPayment(Request);

            var badResult = badRequest.Result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);           
            Assert.IsType<string>(badResult.Value);
            Assert.Equal(400, badResult.StatusCode);
            
        }


        [Fact]
        public void NegativeAmountPassed_ReturnsBadRequest()
        {
            // Arrange
            var Request = new PaymentModel()
            {
                Amount = -1,
                CardHolder = "test",
                CreditCardNumber = "5500000000000004",
                ExpirationDate = DateTime.Now,
                SecurityCode = "123"

            };

            _controller.ModelState.AddModelError("Amount", "GreaterThanZero");
            // Act
            var badRequest = _controller.ProcessPayment(Request);

            var badResult = badRequest.Result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
            Assert.IsType<string>(badResult.Value);
            Assert.Equal(400, badResult.StatusCode);

        }


        [Fact]
        public void LowerExpirationDatePassed_ReturnsBadRequest()
        {
            // Arrange
            var Request = new PaymentModel()
            {
                Amount = 12,
                CardHolder = "test",
                CreditCardNumber = "5500000000000004",
                ExpirationDate = DateTime.Now.AddDays(-1),
                SecurityCode = "123"

            };

            _controller.ModelState.AddModelError("ExpirationDate", "FutureDate");
            // Act
            var badRequest = _controller.ProcessPayment(Request);

            var badResult = badRequest.Result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
            Assert.IsType<string>(badResult.Value);
            Assert.Equal(400, badResult.StatusCode);

        }


        [Fact]
        public void Higher_SecurityCodeLenght_Passed_ReturnsBadRequest()
        {
            // Arrange
            var Request = new PaymentModel()
            {
                Amount = 15,
                CardHolder = "test",
                CreditCardNumber = "5500000000000004",
                ExpirationDate = DateTime.Now,
                SecurityCode = "1224223"

            };

            _controller.ModelState.AddModelError("SecurityCode", "StringLength");
            // Act
            var badRequest = _controller.ProcessPayment(Request);

            var badResult = badRequest.Result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
            Assert.IsType<string>(badResult.Value);
            Assert.Equal(400, badResult.StatusCode);

        }


        

    }
}
