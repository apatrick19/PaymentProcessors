using PaymentProcessor.Core.Concrete;
using PaymentProcessor.Logic.Contracts;
using PaymentProcessor.Logic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PaymentProcessor.Logic.Repository
{
    public class BaseRepo:IBaseRepo
    {
        private IUnitOfWork _unitOfWork;
        public BaseRepo(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool SavePayment(Payment request, ResponseModel result)
        {
            try
            {
                request.PaymentState = result?.ResponseCode == "00" ? new PaymentState { Name = Enum.GetName(typeof(State), State.Processed) } : new PaymentState { Name = Enum.GetName(typeof(State), State.Failed) };
                _unitOfWork._paymentRepository.Add(request);
                _unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceInformation($"An error occurred, {ex.Message}");
                return false;
            }
            //save to database.               
          
        }
    }
}
