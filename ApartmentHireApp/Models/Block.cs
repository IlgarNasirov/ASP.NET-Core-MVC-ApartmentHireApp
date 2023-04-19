using System;
using System.Collections.Generic;

namespace ApartmentHireApp.Models;

public partial class Block
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
}
