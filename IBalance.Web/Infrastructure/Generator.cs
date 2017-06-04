using IBalance.Domain.Entities;
using IBalance.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBalance.Web.Infrastructure
{
    public class Generator
    {
        private const string Symbols = "0123456789qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
        private const string Digits = "0123456789";
        private const int RandomKeyLength = 15;
        private const string Prefix = "IB";
        private Random _random;

        public Generator()
        {
            _random = new Random();
        }

        public List<string> GenerateCodes(GenerateRequestVM generateVM)
        {
            List<string> serialKeys = new List<string>();
            for (int i = 0; i < generateVM.CodesNumber; i++)
            {
                serialKeys.Add(GenerateCode(generateVM.ProductId, generateVM.CounterpartyId));
            }
            return serialKeys;
        }

        private string GenerateCode(int productId, int counterpartyId)
        {
            string randomKey = "";
            for (int i = 0; i < RandomKeyLength; i++)
            {
                if(i % 2 == 0)
                {
                    randomKey += Symbols[_random.Next(0, Symbols.Length)];
                }
                else
                {
                    randomKey += Digits[_random.Next(0, Digits.Length)];
                }
            }

            return $"{Prefix}-{randomKey}-{productId}";
        }
    }
}