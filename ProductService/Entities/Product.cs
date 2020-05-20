using System;
using System.Collections.Generic;

namespace ProductService.Entities
{
    public partial class Product
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
