using IBalance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.Abstract
{
    public interface IConsignmentRepository
    {
        /// <summary>
        /// gets all consignments from database
        /// </summary>
        List<Consignment> GetConsignments();

        /// <summary>
        /// gets consignments where counterparty name equals to counterpartyName
        /// </summary>
        /// <param name="counterpartyName"></param>
        /// <returns></returns>
        List<Consignment> GetConsignmentsByCounterpartyName(string counterpartyName);

        /// <summary>
        /// gets consignments where consignment date equals to date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        List<Consignment> GetConsignmentsByDate(DateTime date);

        /// <summary>
        /// gets consignments where consignment number equals to consignmentNumber
        /// </summary>
        /// <param name="consignmentNumber"></param>
        /// <returns></returns>
        List<Consignment> GetConsignmentsByConsignmentNumber(string consignmentNumber);

        /// <summary>
        /// gets all consignments with id, which belongs to idList
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        List<Consignment> GetConsignmentsWithId(List<int> idList);

        /// <summary>
        /// gets all consignments numbers
        /// </summary>
        /// <returns></returns>
        List<string> GetConsignmentNumbers();

        /// <summary>
        /// gets consignment by serial key
        /// </summary>
        /// <returns></returns>
        Consignment GetConsignmentBySerialKey(string serialKey);

        /// <summary>
        /// saves consignment in database
        /// </summary>
        /// <param name="consignment"></param>
        void SaveConsignment(Consignment consignment);

        /// <summary>
        /// delete consignment from database where consignment.Id == consignmentId
        /// </summary>
        /// <param name="consignmentId"></param>
        void DeleteConsignment(int consignmentId);

        /// <summary>
        /// delete all consignments which id belongs to idList
        /// </summary>
        /// <param name="idList"></param>
        void DeleteConsignments(List<int> idList);

        /// <summary>
        /// delete all consignments from database
        /// </summary>
        void DeleteAllConsignments();

        /// <summary>
        /// gets all consignments by consignmentNumber
        /// </summary>
        /// <param name="consignmentNumber"></param>
        /// <returns></returns>
        List<Consignment> GetConsignmentByConsignmentNumber(string consignmentNumber);
    }
}
