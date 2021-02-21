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
    public class PremiumPaymentService : IPremiumPaymentService
    {
        IRestIntegration _restIntegration;
        private readonly IOptions<ExternalSettings> appSettings;
        private IUnitOfWork _unitOfWork;
        IPaymentProfile _paymentProfile;
        private readonly IBaseRepo _baseRepo;


        public PremiumPaymentService(IRestIntegration rest, IOptions<ExternalSettings> app,  IPaymentProfile profile, IBaseRepo baseRepo)
        {
            _baseRepo = baseRepo;
            _restIntegration = rest;
            appSettings = app;
          
            _paymentProfile = profile;

        }
        public ResponseModel ProcessPremiumPayment(PaymentModel request)
        {
            int count = 1;
            var result = new ResponseModel();
            try
            {
                 //iterate for 3 times if service is not responding ..
                 //the loop breaks if the response is successful
                for (int i = 1; i <= 3; i++)
                {
                    //call integration.
                    result = _restIntegration.UrlPost<ResponseModel>(appSettings.Value.PremiumUrl, request);
                    if (result?.ResponseCode=="00")
                    {
                        break;
                    }
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
