using System;
using System.Collections.Generic;

namespace ProductsMinimalAPI_1165395.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string? SupplierName { get; set; }

    public string? SupplierAddress { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
