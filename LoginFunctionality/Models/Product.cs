using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginFunctionality.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public int SafetyStock { get; set; }
        public int StandardCost { get; set; }
        public int ListPrice { get; set; }
        public int DaysToMake { get; set; }
        public string MedicineFamily { get; set; }
    }
}
