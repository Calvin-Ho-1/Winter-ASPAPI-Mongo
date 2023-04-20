using System;
using System.Collections.Generic;

namespace ASPAPI_mongo.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public int PhoneNumber { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
