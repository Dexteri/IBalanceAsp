using IBalance.Domain.Concrete;
using IBalance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Admin
{
    class Program
    {
        static void Main(string[] args)
        {
            CounterpartyRepository repositry = new CounterpartyRepository();


            CounterpartyToPhone phone1 = new CounterpartyToPhone()
            {
                Phone = "+7(097)642-74-77",
              //  CounterpartyId = counterparty.Id
            };
            CounterpartyToPhone phone2 = new CounterpartyToPhone()
            {
                Phone = "+7(093)263-75-23",
               // CounterpartyId = counterparty.Id
            };
            CounterpartyToPhone phone3 = new CounterpartyToPhone()
            {
                Phone = "+7(092)433-56-64",
               // CounterpartyId = counterparty.Id
            };

            var phones = new List<CounterpartyToPhone>();
            phones.Add(phone1);
            phones.Add(phone2);
            phones.Add(phone3);
            var counterparty = new Counterparty()
            {
                Name = "booo",
                Email = "booo@gmail.com",
                City = "boooo",
                CounterpartyType = Domain.Enums.CounterpartyType.LLC,
                Phones = phones
            };
            repositry.SaveCounterparty(counterparty);
            // counterparty.Phones = phones;
            // phonesRepo.SavePhone(phone1);
            //phonesRepo.SavePhone(phone2);
            //phonesRepo.SavePhone(phone3);
        }
    }
}
