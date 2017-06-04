using IBalance.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace IBalance.Web.Infrastructure
{
    public class FileFormatter
    {
        public static void FormatFile(List<ConsignmentRequestVM> consignments, string path)
        {
            string text = "";
            foreach (var consignment in consignments)
            {
                text += "Дата производства: " + consignment.ProductionDate + Environment.NewLine;
                text += "Модель: " + consignment.Model + Environment.NewLine;
                text += "Серийный номер: " + consignment.SerialKey + Environment.NewLine;
                text += Environment.NewLine;
            }
            File.WriteAllText(path, text);
        }
    }
}