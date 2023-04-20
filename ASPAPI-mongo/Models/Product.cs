using System;
using System.Collections.Generic;

namespace ASPAPI_mongo.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public int ProductRating { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
