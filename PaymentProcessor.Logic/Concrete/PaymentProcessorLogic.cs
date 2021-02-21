using Microsoft.Extensions.Options;
using PaymentProcessor.Logic.Contracts;
using PaymentProcessor.Logic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PaymentProcessor.Logic.Concrete
{
    public class PaymentProcessorLogic : IPaymentProcessorLogic
    {

        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly IPremiumPaymentService  _premiumPaymentService;

        private readonly IOptions<ExternalSettings> appSettings;

        public PaymentProcessorLogic(ICheapPaymentGateway cheap, IExpensivePaymentGateway expensive, IPremiumPaymentService premium, IOptions<ExternalSettings> app)
        {
            _cheapPaymentGateway = cheap;
            _expensivePaymentGateway = expensive;
            _premiumPaymentService = premium;
            appSettings = app;
        }
        public ResponseModel ProcessPayment(PaymentModel request)
        {            
            try
            {
                request.CreditCardNumber=  request.CreditCardNumber.Replace(" ","").Trim();
                if (request.Amount < appSettings.Value.CheapAmount)
                {
                    //call cheap service
                    return _cheapPaymentGateway.ProcessPayment(request);
                }
                if (request.Amount>=appSettings.Value.ExpensiveAmountMin && request.Amount<=appSettings.Value.ExpensiveAmountMax)
                {
                    //call expensive service 
                    return _expensivePaymentGateway.ProcessExpensivePayment(request);
                }

                if (request.Amount>appSettings.Value.PremiumAmount)
                {
                    //premium service. 
                    return _premiumPaymentService.ProcessPremiumPayment(request);
                }

                return ResponseDictionary.ProvideResponse("04");
            }
            catch (Exception ex) 
            {
                Trace.TraceInformation($"An error occurred, {ex.Message}");
               return ResponseDictionary.ProvideResponse("06");
            }
        }
    }
}
