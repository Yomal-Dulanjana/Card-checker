﻿namespace Card_Checker.Models
{
    public class CreditCard
    {
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CVV { get; set; }
    }
}
