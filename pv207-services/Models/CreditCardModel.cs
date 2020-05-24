using System;
using System.Collections.Generic;
using System.Text;

namespace pv207_services.Models
{
    public class CreditCardModel
    {
        public string Number { get; set; }
        public string Expiration { get; set; }
        public int Cvc { get; set; }
    }
}
