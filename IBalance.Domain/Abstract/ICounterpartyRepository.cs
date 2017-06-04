using IBalance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.Abstract
{
    public interface ICounterpartyRepository
    {
        /// <summary>
        /// gets all counterparties from database
        /// </summary>
        List<Counterparty> GetCounterparties();

        /// <summary>
        /// gets all names of counterparties
        /// </summary>
        /// <returns></returns>
        List<string> GetCounterpartiesNames();

        /// <summary>
        /// saves counterparty in database
        /// </summary>
        /// <param name="counterparty"></param>
        void SaveCounterparty(Counterparty counterparty);

        /// <summary>
        /// deletes counterparty which id = counterpartyId from database
        /// </summary>
        /// <param name="counterpartyId"></param>
        void DeleteCounterparty(int counterpartyId);

        /// <summary>
        /// deletes all phones of counterparty which id = counterpartyId from database
        /// </summary>
        /// <param name="counterpartyId"></param>
        void DeletePhones(int counterpartyId);
    }
}
