using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Customer:IEntitiy
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CompanyName { get; set; }
        public string? City { get; set; }

    }
}
