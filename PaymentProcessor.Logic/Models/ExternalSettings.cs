using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessor.Logic.Models
{
   public  class ExternalSettings
    {
        public int CheapAmount { get; set; }
        public int ExpensiveAmountMin { get; set; }
        public int ExpensiveAmountMax { get; set; }
        public int PremiumAmount { get; set; }
        public string CheapUrl { get; set; }
        public string ExpensiveUrl { get; set; }
        public string PremiumUrl { get; set; }
    }
}
