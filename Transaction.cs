using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Komis
{
    public class Transaction
    {
        public int ID { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public Car Car { get; set; }
    }
}
