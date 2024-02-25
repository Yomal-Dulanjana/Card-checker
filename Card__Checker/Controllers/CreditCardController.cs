using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Card_Checker.Models;

namespace Card_Checker.Controllers
{
    public class CreditCardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CreditCard creditCard)
        {
            if (IsValidCard(creditCard.CardNumber))
            {
                if (IsExpiryDateValid(creditCard.ExpiryMonth, creditCard.ExpiryYear))
                {
                    if (IsCVVValid(creditCard.CVV))
                    {
                        string cardType = GetCardType(creditCard.CardNumber);
                        return View("Success", new { CardType = cardType });
                    }
                    else
                    {
                        ViewData["Error"] = "Invalid CVV";
                        return View();
                    }
                }
                else
                {
                    ViewData["Error"] = "Invalid expiry date";
                    return View();
                }
            }
            else
            {
                ViewData["Error"] = "Invalid card number";
                return View();
            }
        }

        private bool IsValidCard(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(cardNumber[i].ToString());

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                alternate = !alternate;
            }

            return sum % 10 == 0;
        }

        private bool IsExpiryDateValid(int month, int year)
        {
            if (year < DateTime.Now.Year)
            {
                return false;
            }
            else if (year == DateTime.Now.Year && month < DateTime.Now.Month)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsCVVValid(string cvv)
        {
            // You can add your own CVV validation logic here.
            // For this example, we will assume that any CVV with 3 or 4 digits is valid.
            return cvv.Length >= 3 && cvv.Length <= 4;
        }

        private string GetCardType(string cardNumber)
        {
            if (cardNumber.StartsWith("34") || cardNumber.StartsWith("37"))
            {
                return "AMEX";
            }
            else if (cardNumber.StartsWith("5"))
            {
                return "Mastercard";
            }
            else if (cardNumber.StartsWith("6"))
            {
                return "Discover";
            }
            else if (cardNumber.StartsWith("4"))
            {
                return "Visa";
            }
            else
            {
                return "Unknown";
            }
        }

    }
}
   