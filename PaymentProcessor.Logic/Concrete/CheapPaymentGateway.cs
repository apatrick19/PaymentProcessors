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
    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        IRestIntegration _restIntegration;
        private readonly IOptions<ExternalSettings> appSettings;       
        IPaymentProfile _paymentProfile;      
        private readonly IBaseRepo _baseRepo;



        public CheapPaymentGateway(IRestIntegration rest, IOptions<ExternalSettings> app, IPaymentProfile profile, IBaseRepo baseRepo)
        {
            _baseRepo = baseRepo;
            _restIntegration = rest;
            appSettings = app;           
            _paymentProfile = profile;
         


        }
        public ResponseModel ProcessPayment(PaymentModel request)
        {
            try
            {
                //call integration. 
                var result = _restIntegration.UrlPost<ResponseModel>(appSettings.Value.CheapUrl, request);

                //map payment model to db object
                var dbModel= _paymentProfile.MapPayment(request);
                if (dbModel==null)
                {
                    return ResponseDictionary.ProvideResponse("06");
                }

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
