using AutoMapper;
using Microsoft.Extensions.Options;
using PaymentProcessor.Core.Concrete;
using PaymentProcessor.Logic.Contracts;
using PaymentProcessor.Logic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PaymentProcessor.Logic.Concrete
{
    public class ExpensivePaymentGateway : IExpensivePaymentGateway
    {

        IRestIntegration _restIntegration;
        private readonly IOptions<ExternalSettings> appSettings;
       
        IPaymentProfile _paymentProfile;
        private readonly IBaseRepo _baseRepo;
        public ExpensivePaymentGateway(IRestIntegration rest, IOptions<ExternalSettings> app, IPaymentProfile profile, IBaseRepo baseRepo)
        {
         
            _restIntegration = rest;
            appSettings = app;
            _baseRepo = baseRepo;
             _paymentProfile = profile;
           
        }
        public ResponseModel ProcessExpensivePayment(PaymentModel request)
        {
            try
            {               
                //call integration. 
                var result = _restIntegration.UrlPost<ResponseModel>(appSettings.Value.ExpensiveUrl, request);

                //expensive gateway is not avalable use cheap gateway. 
                if (string.IsNullOrEmpty(result?.ResponseCode) || result==null)
                {
                    result = _restIntegration.UrlPost<ResponseModel>(appSettings.Value.CheapUrl, request);
                }

                //map payment model to db object
                var dbModel = _paymentProfile.MapPayment(request);
                                          
                //save to database.               
                _baseRepo.SavePayment(dbModel, result);


                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceInformation($"An error occurred, {ex.Message}");
                return ResponseDictionary.ProvideResponse("06");
            }
        }

       

    }
}
