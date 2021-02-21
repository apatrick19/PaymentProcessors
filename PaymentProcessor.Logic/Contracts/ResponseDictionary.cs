using PaymentProcessor.Logic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PaymentProcessor.Logic.Contracts
{
    public class ResponseDictionary
    {
       public static ResponseModel ProvideResponse(string code)
        {
            try
            {
                switch (code)
                {
                    case "00":
                        return new ResponseModel() { ResponseCode = code, ResponseMessage = "Successful" };
                    case "01":
                        return new ResponseModel() { ResponseCode = code, ResponseMessage = "Validation Failed" };
                    case "06":
                        return new ResponseModel() { ResponseCode = code, ResponseMessage = "Internal Server Error" };
                    default:
                        return new ResponseModel() { ResponseCode = code, ResponseMessage = "Uknown Error" };
                        
                }
            }
            catch (Exception ex)
            {
                Trace.TraceInformation($"An error occurred {ex.Message}");
                return new ResponseModel() { ResponseCode = "96", ResponseMessage = "System Error" };
            }
        }
    }
}
