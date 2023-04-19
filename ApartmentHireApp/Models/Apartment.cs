using System;
using System.Collections.Generic;

namespace ApartmentHireApp.Models;

public partial class Apartment
{
    public int Id { get; set; }

    public int Blockid { get; set; }

    public string No { get; set; } = null!;

    public int Numberofrooms { get; set; }

    public double Size { get; set; }

    public double Cost { get; set; }

    public bool? Status { get; set; }

    public virtual Block Block { get; set; } = null!;

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
