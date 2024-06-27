using System;
using System.Collections.Generic;

namespace ProductsMinimalAPI_1165395.Models;

public partial class ProductColor
{
    public int ColourId { get; set; }

    public string ColourName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
